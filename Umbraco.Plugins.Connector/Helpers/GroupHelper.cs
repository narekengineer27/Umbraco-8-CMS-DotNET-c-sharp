namespace Umbraco.Plugins.Connector.Helpers
{
    using Umbraco.Core.Models.Membership;

    public static class GroupHelper
    {
        public static IReadOnlyUserGroup ToReadOnlyGroup(this IUserGroup group)
        {
            return group as IReadOnlyUserGroup ?? (IReadOnlyUserGroup)new ReadOnlyUserGroup(group.Id, group.Name, group.Icon, group.StartContentId, group.StartMediaId, group.Alias, group.AllowedSections, group.Permissions);
        }
    }
}
