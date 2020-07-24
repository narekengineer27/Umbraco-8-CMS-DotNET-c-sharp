namespace Umbraco.Plugins.Connector.Services
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Security;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Exceptions;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Web.Security.Providers;

    public class UserGroupService
    {
        private readonly ILogger logger;
        private readonly IUserService userService;
        private readonly IUmbracoDatabase database;

        public UserGroupService(IUserService userService, IUmbracoDatabase database, ILogger logger)
        {
            this.userService = userService;
            this.logger = logger;
            this.database = database;
        }

        public string ChangeUserPassword(TenantUser tenantUser)
        {
            var user = userService.GetByUsername(tenantUser.Username);
            if (user == null)
            {
                throw new TenantException(ExceptionCode.UsernameDoesNotExist.CodeToString(), ExceptionCode.UsernameDoesNotExist, tenantUser.Username);
            }
            ValidateUserPassword(tenantUser);
            try
            {
                user.RawPasswordValue = (Membership.Providers["UsersMembershipProvider"] as UsersMembershipProvider)?.HashPasswordForStorage(tenantUser.Password);
                userService.Save(user);
                ConnectorContext.AuditService.Add(AuditType.Save, -1, user.Id, "User", $"Password for {user.Id} has been changed");

                return tenantUser.Username;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(UserGroupService), ex.Message);
                logger.Error(typeof(UserGroupService), ex.StackTrace);
                throw;
            }
        }

        public void PurgeUserAfterFirstLogin(int userId)
        {
            database.Execute($"DELETE FROM [dbo].[umbracoUserLogin] WHERE [userId] = {userId}");
        }

        public void CreateUser(TenantUser tenantUser)
        {
            if (!string.IsNullOrEmpty(tenantUser.Password))
            {
                ValidateUserPassword(tenantUser);
            }
            var user = userService.GetByUsername(tenantUser.Username);
            var group = userService.GetUserGroupByAlias("managers");
            if (group == null)
            {
                throw new TenantException(ExceptionCode.GroupDoesNotExist.CodeToString(), ExceptionCode.GroupDoesNotExist, tenantUser.TenantUId, tenantUser.Name);
            }
            try
            {
                if (user == null)
                {
                    user = userService.CreateUserWithIdentity(tenantUser.Username, tenantUser.Email);
                    user.Name = tenantUser.Name;
                }
                var startContentIds = user.StartContentIds;
                Array.Resize(ref startContentIds, startContentIds.Length + 1);
                startContentIds[startContentIds.GetUpperBound(0)] = tenantUser.StartContentIds[0];
                user.StartContentIds = startContentIds;

                var startMediaIds = user.StartMediaIds;
                Array.Resize(ref startMediaIds, startMediaIds.Length + 1);
                startMediaIds[startMediaIds.GetUpperBound(0)] = tenantUser.StartMediaIds[0];
                user.StartMediaIds = startMediaIds;

                user.AddGroup(group.ToReadOnlyGroup());
                if (!string.IsNullOrEmpty(tenantUser.Password))
                {
                    user.RawPasswordValue = (Membership.Providers["UsersMembershipProvider"] as UsersMembershipProvider)?.HashPasswordForStorage(tenantUser.Password);
                }
                userService.Save(user);
                ConnectorContext.AuditService.Add(AuditType.New, -1, user.Id, "User", $"User for {tenantUser.Username} has been created");

                tenantUser.AssignedUmbracoUserId = user.Id;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(UserGroupService), ex.Message);
                logger.Error(typeof(UserGroupService), ex.StackTrace);
                throw;
            }
        }

        public UserGroup CreateUserGroup(TenantGroup tenantGroup)
        {
            ValidateGroup(tenantGroup);
            var groupAlias = tenantGroup.Name.Sanitize();
            UserGroup group = (UserGroup)userService.GetUserGroupByAlias(groupAlias);

            try
            {
                group = new UserGroup(1, groupAlias, tenantGroup.Name, Enumerable.Empty<string>(), "icon-umb-members")
                {
                    Permissions = tenantGroup.Permissions,
                    StartContentId = tenantGroup.StartContentId,
                    StartMediaId = tenantGroup.StartMediaId
                };

                foreach (var section in tenantGroup.AllowedSectionAliases)
                {
                    group.AddAllowedSection(section);
                }
                userService.Save(group);
                ConnectorContext.AuditService.Add(AuditType.New, -1, group.Id, "User Group", $"User Group {group.Name} has been created");
                return group;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(UserGroupService), ex.Message);
                logger.Error(typeof(UserGroupService), ex.StackTrace);
                throw;
            }
        }

        public void DisableUser(string username)
        {
            var user = userService.GetByUsername(username);
            if (user == null)
            {
                throw new TenantException(ExceptionCode.UsernameDoesNotExist.CodeToString(), ExceptionCode.UsernameDoesNotExist, username);
            }
            else
            {
                try
                {
                    userService.Delete(user, false);
                    ConnectorContext.AuditService.Add(AuditType.Delete, -1, user.Id, "User", $"User {user.Id} has been disabled");
                }
                catch (Exception ex)
                {
                    logger.Error(typeof(UserGroupService), ex.Message);
                    logger.Error(typeof(UserGroupService), ex.StackTrace);
                    throw;
                }
            }
        }

        public string EnableUser(string username)
        {
            var user = userService.GetByUsername(username);
            if (user == null)
            {
                throw new TenantException(ExceptionCode.UsernameDoesNotExist.CodeToString(), ExceptionCode.UsernameDoesNotExist, user.Username);
            }
            else
            {
                try
                {
                    user.IsApproved = true;
                    user.IsLockedOut = false;
                    userService.Save(user);
                    ConnectorContext.AuditService.Add(AuditType.Save, -1, user.Id, "User", $"User {user.Id} has been enabled");

                    return username;
                }
                catch (Exception ex)
                {
                    logger.Error(typeof(UserGroupService), ex.Message);
                    logger.Error(typeof(UserGroupService), ex.StackTrace);
                    throw;
                }
            }
        }

        public int AssignUserToGroup(string username, string groupName)
        {
            var user = userService.GetByUsername(username);
            var group = userService.GetUserGroupByAlias(groupName.Sanitize())?.ToReadOnlyGroup();
            if (group == null)
            {
                throw new TenantException(ExceptionCode.GroupDoesNotExist.CodeToString(), ExceptionCode.GroupDoesNotExist, groupName);
            }
            user.AddGroup(group);
            userService.Save(user);
            return group.Id;
        }

        public IUserGroup GetUserGroup(string alias)
        {
            return (UserGroup)userService.GetUserGroupByAlias(alias);
        }

        public IUserGroup EditGroup(string oldGroupName, TenantGroup newGroup)
        {
            var group = userService.GetUserGroupByAlias(oldGroupName.Sanitize());
            if (group == null)
            {
                throw new TenantException(ExceptionCode.GroupDoesNotExist.CodeToString(), ExceptionCode.GroupDoesNotExist, oldGroupName);
            }
            if (!string.IsNullOrEmpty(newGroup.Name))
                ValidateGroup(newGroup.Name);
            try
            {
                group.Name = string.IsNullOrEmpty(newGroup.RenameGroupTo) ? group.Name : newGroup.RenameGroupTo;
                group.Alias = string.IsNullOrEmpty(newGroup.RenameGroupTo) ? group.Alias : newGroup.RenameGroupTo.Sanitize();
                group.Permissions = newGroup.Permissions.Any() ? newGroup.Permissions : group.Permissions;

                if (newGroup.AllowedSectionAliases.Any() && !string.Join(",", newGroup.AllowedSectionAliases.ToArray()).Equals(string.Join(",", group.AllowedSections)))
                {
                    group.ClearAllowedSections();
                    foreach (var section in newGroup.AllowedSectionAliases)
                    {
                        group.AddAllowedSection(section);
                    }
                }
                if (newGroup.StartContentId.HasValue && !newGroup.StartContentId.Value.Equals(group.StartContentId))
                {
                    group.StartContentId = newGroup.StartContentId.Value;
                }
                if (newGroup.StartMediaId.HasValue && !newGroup.StartMediaId.Value.Equals(group.StartMediaId))
                {
                    group.StartMediaId = newGroup.StartMediaId.Value;
                }

                userService.Save(group);
                ConnectorContext.AuditService.Add(AuditType.Save, -1, group.Id, "User Group", $"User Group {group.Id} has been renamed from '{oldGroupName}' to '{newGroup.Name}'");
                return group;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(UserGroupService), ex.Message);
                logger.Error(typeof(UserGroupService), ex.StackTrace);
                throw;
            }
        }

        public string ResetUserPassword(string username)
        {
            var user = userService.GetByUsername(username);
            if (user == null)
            {
                throw new TenantException(ExceptionCode.UsernameDoesNotExist.CodeToString(), ExceptionCode.UsernameDoesNotExist, username);
            }
            else
            {
                try
                {
                    Regex _regex = new Regex("[^a-zA-Z0-9]");
                    var password = Membership.GeneratePassword(10, 1);
                    var formatted = _regex.Replace(password, "9");

                    user.RawPasswordValue = formatted;
                    userService.Save(user);
                    ConnectorContext.AuditService.Add(AuditType.Save, -1, user.Id, "User", $"Password for {user.Id} has been reset");

                    return formatted;
                }
                catch (Exception ex)
                {
                    logger.Error(typeof(UserGroupService), ex.Message);
                    logger.Error(typeof(UserGroupService), ex.StackTrace);
                    throw;
                }
            }
        }

        public void UpdateUser(TenantUser tenantUser)
        {
            var user = userService.GetByUsername(tenantUser.Username);
            if (user == null)
            {
                throw new TenantException(ExceptionCode.UsernameDoesNotExist.CodeToString(), ExceptionCode.UsernameDoesNotExist, tenantUser.Username);
            }

            if (!string.IsNullOrEmpty(tenantUser.Password))
            {
                ValidateUserPassword(tenantUser);
            }
            try
            {
                user.Name = tenantUser.Name ?? user.Name;
                user.Email = tenantUser.Email ?? user.Email;
                if (!string.IsNullOrEmpty(tenantUser.Password))
                    user.RawPasswordValue = (Membership.Providers["UsersMembershipProvider"] as UsersMembershipProvider)?.HashPasswordForStorage(tenantUser.Password);

                userService.Save(user);

                if (!string.IsNullOrEmpty(tenantUser.Group) && !user.Groups.ToArray()[0].Name.Equals(tenantUser.Group))
                {
                    AssignUserToGroup(user.Username, tenantUser.Group);
                }

                ConnectorContext.AuditService.Add(AuditType.Save, -1, user.Id, "User", $"User {user.Username} has been edited");

                tenantUser.AssignedUmbracoUserId = user.Id;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(UserGroupService), ex.Message);
                logger.Error(typeof(UserGroupService), ex.StackTrace);
                throw;
            }
        }

        public void Validate(Tenant tenant, bool ignoreGroup = false, bool ignoreUser = false)
        {
            var user = userService.GetByUsername(tenant.Username);
            var group = userService.GetUserGroupByAlias(tenant.Group);
            if (!ignoreUser && user != null)
            {
                throw new TenantException(ExceptionCode.UsernameAlreadyExists.CodeToString(), ExceptionCode.UsernameAlreadyExists, tenant.TenantUId, user.Username);
            }
            if (!ignoreGroup && group != null)
            {
                throw new TenantException(ExceptionCode.GroupAlreadyExists.CodeToString(), ExceptionCode.GroupAlreadyExists, tenant.TenantUId, group.Name);
            }
        }

        public void ValidateUser(TenantUser user)
        {
            var u = userService.GetByUsername(user.Username);
            if (user.Password.Length < 10)
            {
                throw new TenantException(ExceptionCode.PasswordLenghtIncorrect.CodeToString(), ExceptionCode.PasswordLenghtIncorrect, user.TenantUId, user.Password);
            }
        }

        public void ValidateUserPassword(TenantUser user)
        {
            if (user.Password.Length < 10)
            {
                throw new TenantException(ExceptionCode.PasswordLenghtIncorrect.CodeToString(), ExceptionCode.PasswordLenghtIncorrect, user.TenantUId, user.Password);
            }
        }

        public void ValidateGroup(TenantGroup group)
        {
            var g = userService.GetUserGroupByAlias(group.Name);
            if (g != null)
            {
                throw new TenantException(ExceptionCode.GroupAlreadyExists.CodeToString(), ExceptionCode.GroupAlreadyExists, group.TenantUid, group.Name);
            }
        }
    }
}
