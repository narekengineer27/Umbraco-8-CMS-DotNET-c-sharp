namespace Umbraco.Plugins.Connector.Services
{
    using System;
    using System.Linq;
    using Umbraco.Core.Scoping;
    using Umbraco.Core.Persistence;
    using System.Collections.Generic;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Content;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.ConnectorServices;
    using Umbraco.Plugins.Connector.Exceptions;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using System.Web;

    public class ControllerService : IApiConnector
    {
        private const string _customSvgHtml = "<svg class=\"land-icon icons-{0}\"><use xlink:href=\"/img/sprite-menu.svg#icon-{0}\"></use></svg>";
        private readonly ApiKeysService apiService;
        private readonly IUmbracoDatabase database;
        private readonly HomeContentNode homeNode;
        private readonly LanguageDictionaryService languageService;
        private readonly NodeHelper nodeHelper;
        private readonly IScopeProvider scopeProvider;
        private readonly UserGroupService userGroupService;
        public ControllerService()
        {
            this.scopeProvider = ConnectorContext.ScopeProvider;
            using (var scope = scopeProvider.CreateScope(autoComplete: true))
            {
                this.database = scope.Database;
                ApiKeyCache.UpdateCache(scope.Database);
            }
            this.userGroupService = new UserGroupService(ConnectorContext.UserService, database, ConnectorContext.Logger);
            this.languageService = new LanguageDictionaryService(ConnectorContext.LocalizationService, ConnectorContext.DomainService, ConnectorContext.Logger);
            this.homeNode = new HomeContentNode(ConnectorContext.ContentService, ConnectorContext.LocalizationService, ConnectorContext.DomainService, ConnectorContext.ContentTypeService, ConnectorContext.Logger);
            this.apiService = new ApiKeysService(database);
            this.nodeHelper = new NodeHelper();

            SaveAndPublish = bool.Parse(TenantGenerationOptions.SaveAndPublish);
            SecureUrls = bool.Parse(TenantGenerationOptions.SecureUrls);
            SetupLocalUrls = bool.Parse(TenantGenerationOptions.SetupLocalUrls);

#if DEBUG
            SetupLocalUrls = true;
#endif
        }

        public bool SaveAndPublish { get; set; }
        public bool SecureUrls { get; set; }
        public bool SetupLocalUrls { get; set; }

        public TenantDomains AddTenantDomain(TenantDomain payload)
        {
            return homeNode.AddTenantDomain(payload, SetupLocalUrls, SecureUrls);
        }

        public string AssignUserGroups(string[] groups, string username)
        {
            string groupsList = string.Empty;
            for (int i = 0; i < groups.Length; i++)
            {
                groupsList += $"{userGroupService.AssignUserToGroup(username, groups[i])},";
            }
            return groupsList;
        }

        public string ChangePassword(TenantUser tenantUser)
        {
            return userGroupService.ChangeUserPassword(tenantUser);
        }

        public string CreateGroup(TenantGroup group)
        {
            return userGroupService.CreateUserGroup(group).Id.ToString();
        }

        public ExtendedTenant CreateTenant(Tenant tenant, string tenantToBeCopied = "")
        {
            int homeId, mediaId, sliderBannersId, myAccountId, editMyAccountId, documentsId, marketingPreferencesId, withdrawId, withdrawBankAccountsAndCardsId;
            int withdrawHistoryId, ticketsId, viewTicketId, depositId, depositBankAccountsAndCardsId, depositHistoryId, termsAndConditionsId, settingsId;
            int transactionHistoryId, helpId, loginProblemsId, depositAndWithdrawId;
            int H_registrationAndLoginId, H_depositId, H_withdrawId, H_myAccountId, H_betHistoryId, H_transactionHistoryId, H_sportsId, H_inplayId, H_casinoId, H_liveCasinoId, H_pokerId, H_faqId;
            int HA_registration, HA_login, HA_forgotUsername, HA_forgotPwd, HA_depositCardsAndBank, HA_depositCartipay, HA_depositCartipal, HA_depositHistory, HA_withdrawCardsAndBank, HA_withdraw, HA_withdrawHistory, HA_myAccount, HA_betHistory, HA_transactionHistory, HA_sportbook, HA_inplay;
            int betHistoryId, bonusHistoryId, sportId, inPlayId, casinoId, liveCasinoId, vegasId, lotteryId, pokerId, liveScheduleId, boardId, liveScoreId, overviewId, eventViewId, resultsId;
            int zeppelinId;
            int affiliateId;
            int bonusHistoryPageId;
            int loginPageId;
            var nodeHelper = new NodeHelper();
            var homeTypeDoc = new HomeDocumentType(ConnectorContext.ContentTypeService, ConnectorContext.DataTypeService, ConnectorContext.FileService, ConnectorContext.Logger);
            var mediaNode = new HomeMediaNode(ConnectorContext.MediaService, ConnectorContext.Logger, ConnectorContext.ContentTypeBaseService);

            homeNode.Validate(tenant);
            mediaNode.Validate(tenant);
            languageService.Validate(tenant);
            userGroupService.Validate(tenant, ignoreUser: true);
            apiService.Validate(tenant);

            if (tenantToBeCopied != "")
            {
                IContent tenantSourceNode = nodeHelper.GetTenantRoot(tenantToBeCopied);

                if (tenantSourceNode == null) throw new TenantException(ExceptionCode.TenantNotFound.CodeToString(), ExceptionCode.TenantNotFound, tenantToBeCopied);

                //copy root node with all subnodes
                IContent newTenant = nodeHelper.CopyTenant(tenantSourceNode, tenantSourceNode.ParentId, tenant.TenantUId);

                homeNode.EditTenant(tenant);

                homeId = newTenant.Id;

                //Create new media folder
                mediaId = mediaNode.CopyTenantMediaFolder(tenantSourceNode.GetPublishName("en"), tenant);
                sliderBannersId = mediaNode.GetMediaSliderFolder(mediaId);

                // My Account
                myAccountId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeAccountPage", "My Account");
                editMyAccountId = nodeHelper.GetNodeId(myAccountId, _09_EditAccountDocumentType.DOCUMENT_TYPE_ALIAS, _09_EditAccountDocumentType.PAGE_NAME);

                documentsId = nodeHelper.GetNodeId(myAccountId, "totalCodeDocumentPage", "Documents");
                marketingPreferencesId = nodeHelper.GetNodeId(myAccountId, "totalCodeMarketingPage", "Marketing Preferences");

                // Withdraw
                withdrawId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeWithdrawPage", "Withdraw");
                withdrawBankAccountsAndCardsId = nodeHelper.GetNodeId(withdrawId, "totalCodeWithdrawBankPage", "Bank Accounts & Cards");
                withdrawHistoryId = nodeHelper.GetNodeId(withdrawId, "totalCodeWithdrawHistoryPage", "History");

                // Tickets
                ticketsId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeTicketsPage", "Contact Us");
                viewTicketId = nodeHelper.GetNodeId(ticketsId, "totalCodeTicketPage", "View Ticket");

                // Deposit
                depositId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeDepositPage", "Deposit");
                depositBankAccountsAndCardsId = nodeHelper.GetNodeId(depositId, "totalCodeDepositBankPage", "Bank Accounts & Cards");
                depositHistoryId = nodeHelper.GetNodeId(depositId, "totalCodeDepositHistoryPage", "History");

                // Terms and Conditions
                termsAndConditionsId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeTermsPage", "Terms and Conditions");

                // Settings
                settingsId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeSettingsPage", "Settings");

                // Transaction History
                transactionHistoryId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeTransactionHistoryPage", "Transaction History");

                // Help
                helpId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeCategoriesPage", "Help");
                loginProblemsId = nodeHelper.GetNodeId(helpId, "totalCodeCategoryPage", "Login Problems");
                depositAndWithdrawId = nodeHelper.GetNodeId(helpId, "totalCodeCategoryPage ", "Deposit & Withdraw");

                // Sports Pages
                betHistoryId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Betting History");
                bonusHistoryId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Bonus History");
                sportId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Sport");
                inPlayId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "In-Play");
                casinoId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Casino");
                liveCasinoId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Live Casino");
                vegasId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Vegas");
                lotteryId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Lottery");
                pokerId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Poker");
                liveScheduleId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Live Schedule");
                boardId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Board Games");
                liveScoreId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Live Score");
                overviewId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Overview");
                eventViewId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Event View");
                resultsId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGenericPage", "Results");
                zeppelinId = nodeHelper.GetNodeId(newTenant.Id, "totalCodeGamePage", "Zeppelin");

                // Affiliate
                affiliateId = nodeHelper.GetNodeId(newTenant.Id, _34_AffiliatePageDocumentType.DOCUMENT_TYPE_ALIAS, "Affiliate");

                // Bonus History Page
                bonusHistoryPageId = nodeHelper.GetNodeId(newTenant.Id, _35_BonusHistoryPageDocumentType.DOCUMENT_TYPE_ALIAS, "Bonus History");

                // Login Page
                loginPageId = nodeHelper.GetNodeId(newTenant.Id, _41_LoginPageDocumentType.DOCUMENT_TYPE_ALIAS, "Login");
            }
            else
            {

                languageService.CreateDictionary(tenant);

                homeId = homeNode.CreateTenant(tenant);
                mediaId = mediaNode.CreateMediaHome(tenant);
                sliderBannersId = mediaNode.CreateMediaSliderFolder(mediaId);

                // Create Sub Pages under tenant Root
                var confirmEmailNode = new ConfirmEmailContentNode(ConnectorContext.ContentService, ConnectorContext.ContentTypeService, ConnectorContext.Logger);
                confirmEmailNode.CreateConfirmEmail(tenant);

                var resetPasswordNode = new ResetPasswordContentNode(ConnectorContext.ContentService, ConnectorContext.ContentTypeService, ConnectorContext.Logger);
                resetPasswordNode.CreateResetPasswordPage(tenant);

                // My Account
                myAccountId = nodeHelper.CreateNode(tenant, homeId, "totalCodeAccountPage", "My Account", "حساب من");
                editMyAccountId = nodeHelper.CreateNode(tenant, myAccountId, "TotalCodeEditAccountPage", "Edit", "ویرایش");

                documentsId = nodeHelper.CreateNode(tenant, myAccountId, "totalCodeDocumentPage", "Documents", "مدارک شناسایی");
                marketingPreferencesId = nodeHelper.CreateNode(tenant, myAccountId, "totalCodeMarketingPage", "Marketing Preferences", "تنظیمات مارکتینگ");

                // Withdraw
                withdrawId = nodeHelper.CreateNode(tenant, homeId, "totalCodeWithdrawPage", "Withdraw", "برداشت از حساب");
                withdrawBankAccountsAndCardsId = nodeHelper.CreateNode(tenant, withdrawId, "totalCodeWithdrawBankPage", "Bank Accounts & Cards", "کارت و حساب های بانکی");
                withdrawHistoryId = nodeHelper.CreateNode(tenant, withdrawId, "totalCodeWithdrawHistoryPage", "History", "سابقه برداشت از حساب");

                // Tickets
                ticketsId = nodeHelper.CreateNode(tenant, homeId, "totalCodeTicketsPage", "Contact Us", "پشتیبانی");
                viewTicketId = nodeHelper.CreateNode(tenant, ticketsId, "totalCodeTicketPage", "View Ticket", "نمایش پیام");

                // Deposit
                depositId = nodeHelper.CreateNode(tenant, homeId, "totalCodeDepositPage", "Deposit", "افزایش موجودی");
                depositBankAccountsAndCardsId = nodeHelper.CreateNode(tenant, depositId, "totalCodeDepositBankPage", "Bank Accounts & Cards", "کارت و حساب های بانکی");
                depositHistoryId = nodeHelper.CreateNode(tenant, depositId, "totalCodeDepositHistoryPage", "History", "سابقه افزایش موجودی");

                // Terms and Conditions
                termsAndConditionsId = nodeHelper.CreateNode(tenant, homeId, "totalCodeTermsPage", "Terms and Conditions", "شرایط و ضوابط");

                // Settings
                settingsId = nodeHelper.CreateNode(tenant, homeId, "totalCodeSettingsPage", "Settings", "تنظیمات");

                // Transaction History
                transactionHistoryId = nodeHelper.CreateNode(tenant, homeId, "totalCodeTransactionHistoryPage", "Transaction History", "سابقه تراکنش ها");

                // Help
                helpId = nodeHelper.CreateNode(tenant, homeId, "totalCodeCategoriesPage", "Help", "راهنما");

                // Help categories
                H_registrationAndLoginId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "Registration and login", "ثبت نام و ورود به سایت");
                H_depositId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "Deposit", "افزایش موجودی");
                H_withdrawId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "Withdraw", "برداشت از حساب");
                H_myAccountId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "My Account", "حساب من");
                H_betHistoryId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "Bet History", "سابقه پیش بینی ورزشی");
                H_transactionHistoryId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "Transaction History", "سابقه تراکنش ها");
                H_sportsId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "Sports", "پیش بینی ورزشی");
                H_inplayId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "In-Play", "پیش بینی ورزشی زنده");
                H_casinoId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "Casino", "کازینو");
                H_liveCasinoId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "Live Casino", "کازینو زنده");
                H_pokerId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "Poker", "پوکر");
                H_faqId = nodeHelper.CreateNode(tenant, helpId, "totalCodeCategoryPage", "FAQ", "سوالات متداول");

                // Help articles
                HA_registration = nodeHelper.CreateNode(tenant, H_registrationAndLoginId, "totalCodeArticlePage", "Registration", "ثبت نام");
                HA_login = nodeHelper.CreateNode(tenant, H_registrationAndLoginId, "totalCodeArticlePage", "Login", "ورود به سایت");
                HA_forgotUsername = nodeHelper.CreateNode(tenant, H_registrationAndLoginId, "totalCodeArticlePage", "Forgot your username?", "فراموش کردن نام کاربری");
                HA_forgotPwd = nodeHelper.CreateNode(tenant, H_registrationAndLoginId, "totalCodeArticlePage", "Forgot your password?", "فراموش کردن رمز");

                HA_depositCardsAndBank = nodeHelper.CreateNode(tenant, H_depositId, "totalCodeArticlePage", "Cards and Bank Accounts", "کارت و حسابهای بانکی");
                HA_depositCartipay = nodeHelper.CreateNode(tenant, H_depositId, "totalCodeArticlePage", "Deposit with Cartipay", "افزایش موجودی با کارتی پی");
                HA_depositCartipal = nodeHelper.CreateNode(tenant, H_depositId, "totalCodeArticlePage", "Deposit with Cartipal", "افزایش موجودی با کارتی پال");
                HA_depositHistory = nodeHelper.CreateNode(tenant, H_depositId, "totalCodeArticlePage", "Deposit History", "سابقه افزایش موجودی");

                HA_withdrawCardsAndBank = nodeHelper.CreateNode(tenant, H_withdrawId, "totalCodeArticlePage", "Cards and Bank Accounts", "کارت و حساب های بانکی");
                HA_withdraw = nodeHelper.CreateNode(tenant, H_withdrawId, "totalCodeArticlePage", "Withdraw", "برداشت از حساب");
                HA_withdrawHistory = nodeHelper.CreateNode(tenant, H_withdrawId, "totalCodeArticlePage", "Withdraw History", "سابقه برداشت از حساب");

                HA_myAccount = nodeHelper.CreateNode(tenant, H_myAccountId, "totalCodeArticlePage", "My account", "حساب من");
                HA_betHistory = nodeHelper.CreateNode(tenant, H_betHistoryId, "totalCodeArticlePage", "Bet History", "سابقه پیش بینی ورزشی");
                HA_transactionHistory = nodeHelper.CreateNode(tenant, H_transactionHistoryId, "totalCodeArticlePage", "Transaction History", "سابقه تراکنش ها");
                HA_sportbook = nodeHelper.CreateNode(tenant, H_sportsId, "totalCodeArticlePage", "Sportbook", "پیش بینی ورزشی");
                HA_inplay = nodeHelper.CreateNode(tenant, H_inplayId, "totalCodeArticlePage", "In-Play", "پیش بینی زنده");


                // Sports Pages
                betHistoryId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Betting History", "سابقه پیش بینی ها");
                bonusHistoryId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Bonus History", "سابقه بونوس");
                sportId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Sport", "پیش بینی ورزشی");
                inPlayId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "In-Play", "پیش بینی زنده");
                casinoId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Casino", "کازینو");
                liveCasinoId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Live Casino", "کازینو زنده");
                vegasId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Vegas", "وگاس");
                lotteryId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "TVBET", "بخت آزمایی");
                pokerId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Poker", "پوکر");
                liveScheduleId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Live Schedule", "تقویم مسابقات");
                boardId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Board Games", "تخته نرد");
                liveScoreId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Live Score", "نتایج زنده");
                overviewId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Overview", "خلاصه");
                eventViewId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Event View", "نمایش رویداد");
                resultsId = nodeHelper.CreateNode(tenant, homeId, "totalCodeGenericPage", "Results", "نتایج");
                zeppelinId = nodeHelper.CreateNode(tenant, casinoId, "totalCodeGamePage", "Zeppelin", "انفجار");

                // Affiliate
                affiliateId = nodeHelper.CreateNode(tenant, homeId, _34_AffiliatePageDocumentType.DOCUMENT_TYPE_ALIAS, "Affiliate", "وابسته");

                // Bonus History Page
                bonusHistoryPageId = nodeHelper.CreateNode(tenant, homeId, _35_BonusHistoryPageDocumentType.DOCUMENT_TYPE_ALIAS, "Bonus History", "تاریخ پاداش");

                // Login Page
                loginPageId = nodeHelper.CreateNode(tenant,homeId, _41_LoginPageDocumentType.DOCUMENT_TYPE_ALIAS, "Login", "وارد شدن");

                UpdateTenantPages(betHistoryId, sportId, casinoId, pokerId, inPlayId, liveCasinoId, vegasId, lotteryId, liveScheduleId, boardId, zeppelinId);
            }

            SetHomeMenu(homeId, betHistoryId, sportId, casinoId, pokerId, inPlayId, liveCasinoId, vegasId, lotteryId, liveScheduleId, boardId, overviewId, eventViewId, ticketsId, liveScoreId, resultsId, helpId, depositId, withdrawId, myAccountId, bonusHistoryId, transactionHistoryId);

            SetHomeSettings(tenant,homeId, betHistoryId, sportId, casinoId, pokerId, inPlayId, liveCasinoId, vegasId, lotteryId, liveScheduleId, boardId, overviewId, eventViewId, ticketsId, liveScoreId, resultsId, helpId, depositId, withdrawId, myAccountId, bonusHistoryId, transactionHistoryId, termsAndConditionsId);


            var extended = new ExtendedTenant
            {
                Tenant = tenant,
                StartContentId = homeId,
                StartMediaId = mediaId,
                AllowedSectionAliases = new List<string>
                {
                    DefaultSections.CONTENT, DefaultSections.MEDIA
                },
                Permissions = ExtendedTenant.DefaultManagerPermissions,
            };

            //userGroupService.CreateUserGroup(extended);
            userGroupService.CreateUser(extended);

            // Save Api Key information
            apiService.Save(extended);
            ApiKeyCache.ForceUpdateCache(database);

            if (SaveAndPublish)
                nodeHelper.TryPublishSite(homeId);

            languageService.SetCulturesAndHostnames(extended, setupLocal: SetupLocalUrls, secureUrl: SecureUrls);

            return extended;
        }


        public string CreateUser(TenantUser tenant)
        {
            var tenantNode = TenantHelper.GetCurrentTenantHome(ConnectorContext.ContentService, tenant.TenantUId.ToString());
            var group = userGroupService.GetUserGroup(tenant.Group);

            userGroupService.CreateUser(tenant);
            return tenant.AssignedUmbracoUserId.ToString();
        }

        public string DisableTenant(SimpleTenant tenant)
        {
            homeNode.DisableTenant(tenant.TenantUId);
            userGroupService.DisableUser(tenant.Username);
            return tenant.TenantUId.ToString();
        }

        public string DisableUser(SimpleTenant tenant)
        {
            //new HomeContentNode(ConnectorContext.ContentService, ConnectorContext.ContentTypeService, ConnectorContext.Logger).ValidateTenant(tenant.TenantUId);
            userGroupService.DisableUser(tenant.Username);
            return tenant.Username;
        }

        public ExtendedTenant EditTenant(TenantData tenant, TenantUser user = null, TenantGroup group = null)
        {
            var mediaService = new HomeMediaNode(ConnectorContext.MediaService, ConnectorContext.Logger, ConnectorContext.ContentTypeBaseService);
            var tenantNode = nodeHelper.GetTenantRoot(tenant.TenantUId);
            languageService.CreateDictionary(tenant);

            if (user != null)
            {
                userGroupService.UpdateUser(user);
            }

            IUserGroup g = null;
            if (group != null)
            {
                if (!string.IsNullOrWhiteSpace(group.RenameGroupTo))
                {
                    g = userGroupService.EditGroup(group.Name, group);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(tenant.ReadOnlyGroupAlias))
                    g = userGroupService.GetUserGroup(tenant.ReadOnlyGroupAlias);
            }

            var mediaId = mediaService.RenameMediaHome(tenant, user);

            tenant.TenantPreferences.PaymentSettings = tenant.PaymentSettings; //little hack to match total coding system submission to umbraco properties
            tenant.PaymentSettings = null;

            var extended = new ExtendedTenant
            {
                Tenant = tenant,
                StartContentId = tenantNode.Id,
                StartMediaId = mediaId,
                AllowedSectionAliases = g?.AllowedSections,
                Permissions = g?.Permissions,
            };

            homeNode.EditTenant(tenant);
            nodeHelper.AddLanguageVersionToNode(extended.Tenant);

            extended.Tenant.Username = user?.Username;
            extended.Tenant.Name = user?.Name;
            extended.Tenant.Email = user?.Email;
            extended.Tenant.Password = user != null ? "*********" : null;
            extended.Tenant.Group = group?.Name;
            extended.UserId = user?.AssignedUmbracoUserId ?? 0;

            if (!string.IsNullOrEmpty(extended.Tenant.AppId) || !string.IsNullOrEmpty(extended.Tenant.ApiKey))
            {
                var apiService = new ApiKeysService(this.database);
                apiService.UpdateApiForTenant(extended);
            }

            if (SaveAndPublish)
                new NodeHelper().TryPublishSite(tenantNode.Id);

            return extended;
        }

        public ExtendedTenant EditTenantWrap(TenantEdit edit)
        {
            return EditTenant(edit.Tenant, edit.User, edit.Group);
        }

        public string EnableTenant(SimpleTenant tenant)
        {
            homeNode.EnableTenant(tenant.TenantUId);
            userGroupService.EnableUser(tenant.Username);
            return tenant.TenantUId.ToString();
        }

        public string EnableUser(SimpleTenant user)
        {
            return userGroupService.EnableUser(user.Username);
        }

        public void LoginTenant(SimpleTenant tenant)
        {
            homeNode.ValidateTenant(tenant.TenantUId);
            new Controllers.ExternalApiConnectorController().LoginTenantRemotely(tenant);
        }

        public PurgeResults PurgeTenant(SimpleTenant tenant)
        {
            var contentService = ConnectorContext.ContentService;
            var userService = ConnectorContext.UserService;
            var mediaService = ConnectorContext.MediaService;

            var tenantNode = nodeHelper.GetTenantRoot(tenant.TenantUId);
            if (tenantNode == null) throw new TenantException(ExceptionCode.TenantNotFound.CodeToString(), ExceptionCode.TenantNotFound, tenant.TenantUId);

            var user = userService.GetByUsername(tenant.Username);
            var group = user?.Groups.Any() == true ? userService.GetUserGroupById(user.Groups.ToList()[0].Id) : userService.GetUserGroupByAlias(tenant.Group.Sanitize());
            var media = group != null ? mediaService.GetById(group.StartMediaId.Value) : mediaService.GetByLevel(1).SingleOrDefault(x => x.Name.Equals(tenantNode.Name));

            if (user != null)
            {
                userGroupService.PurgeUserAfterFirstLogin(user.Id);
                userService.Delete(user, true);
            }

            if (group != null)
            {
                foreach (var u in userService.GetAllInGroup(group.Id))
                {
                    userService.Delete(u, true);
                }
                userService.DeleteUserGroup(group);
            }

            if (media != null)
                mediaService.Delete(media);
            if (tenantNode != null)
                contentService.Delete(tenantNode);

            new ApiKeysService(this.database).PurgeTenant(tenant.TenantUId.ToString()); // remove tenant's Api information
            ApiKeyCache.ForceUpdateCache(this.database);

            return new PurgeResults
            {
                ContentFoundAndDeleted = tenantNode != null,
                GroupFoundAndDeleted = group != null,
                UserFoundAndDeleted = user != null,
                MediaFolderFoundAndDeleted = media != null
            };
        }

        public ExtendedTenant ReassignCulturesAndHostnames(string tenantUid)
        {
            var extended = GetExtendedTenant(tenantUid);
            languageService.ClearCulturesAndHostnames(extended);
            languageService.SetCulturesAndHostnames(extended, setupLocal: SetupLocalUrls, secureUrl: SecureUrls);

            foreach (var alternateDomain in extended.Tenant.AlternateDomains)
            {
                languageService.AddCultureAndHostname(new TenantDomain
                {
                    Domain = alternateDomain,
                    TenantUid = tenantUid
                }, SecureUrls);
            }

            if (SaveAndPublish)
                nodeHelper.TryPublishSite(extended.StartContentId);

            return extended;
        }

        public bool RefreshTenant(string tenantUid, string tenantName, string languageCode = "en")
        {
            int betHistoryId = -1, sportId = -1, casinoId = -1, pokerId = -1, inPlayId = -1, liveCasinoId = -1, vegasId = -1, lotteryId = -1, liveScheduleId = -1;

            var extended = GetExtendedTenant(tenantUid);
            var children = TenantHelper.GetTenantNodes(ConnectorContext.ContentService, tenantUid);
            var tenantNode = TenantHelper.GetCurrentTenantHome(ConnectorContext.ContentService, tenantUid);
            if (tenantNode == null) throw new TenantException(ExceptionCode.TenantNotFound.CodeToString(), ExceptionCode.TenantNotFound, tenantUid);

            string cultureIsoCode = languageCode;
            try
            {
                if (languageService.HasLanguage(cultureIsoCode))
                {
                    if (children.Any())
                    {
                        betHistoryId = children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Betting History")) != null ? children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Betting History")).Id : -1;
                        sportId = children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Sport")) != null ? children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Sport")).Id : -1;
                        inPlayId = children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("In-Play")) != null ? children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("In-Play")).Id : -1;
                        casinoId = children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Casino")) != null ? children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Casino")).Id : -1;
                        liveCasinoId = children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Live Casino")) != null ? children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Live Casino")).Id : -1;
                        vegasId = children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Vegas")) != null ? children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Vegas")).Id : -1;
                        lotteryId = children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Lottery")) != null ? children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Lottery")).Id : -1;
                        pokerId = children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Poker")) != null ? children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Poker")).Id : -1;
                        liveScheduleId = children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Live Schedule")) != null ? children.SingleOrDefault(x => x.GetCultureName(cultureIsoCode).Equals("Live Schedule")).Id : -1;

                        UpdateTenantPages(betHistoryId, sportId, casinoId, pokerId, inPlayId, liveCasinoId, vegasId, lotteryId, liveScheduleId);
                    }

                    nodeHelper.RefreshNodeName(tenantUid, tenantName);
                }
                ReassignCulturesAndHostnames(tenantUid);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int RefreshAllTenants(string language = "en")
        {
            var tenants = nodeHelper.GetAllTenants();
            var count = 0;
            foreach (var tenant in tenants)
            {
                if (RefreshTenant(tenant.GetValue<string>("tenantUid"), tenant.GetCultureName(language) ?? tenant.GetValue<string>("brandName"), language))
                    count++;
            }
            return count;
        }

        public TenantDomains RemoveTenantDomain(TenantDomain payload)
        {
            return homeNode.RemoveTenantDomain(payload, SecureUrls);
        }

        public string ResetPassword(SimpleTenant tenant)
        {
            homeNode.ValidateTenant(tenant.TenantUId);
            return userGroupService.ResetUserPassword(tenant.Username);
        }

        private ExtendedTenant GetExtendedTenant(string tenantUid)
        {
            var tenantNode = nodeHelper.GetTenantRoot(tenantUid);
            var extended = new ExtendedTenant
            {
                Tenant = new Tenant
                {
                    Languages = new Languages
                    {
                        Alternate = tenantNode.GetValue<string>("languages") != null ? tenantNode.GetValue<string>("languages")?.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries) : new string[] { },
                        Default = tenantNode.GetValue<string>("defaultLanguage") ?? string.Empty
                    },
                    Domain = tenantNode.GetValue<string>("domain") ?? string.Empty,
                    SubDomain = tenantNode.GetValue<string>("subDomain") ?? string.Empty,
                    AlternateDomains = tenantNode.GetValue<string>("alternateDomains")?.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries) ?? new string[] { }
                },
                StartContentId = tenantNode.Id
            };
            return extended;
        }

        private void SetHomeMenu(int homeId, int betHistoryId, int sportId, int casinoId, int pokerId, int inPlayId, int liveCasinoId, int vegasId, int lotteryId, int liveScheduleId, int boardId, int overviewId, int eventViewId, int ticketsId, int liveScoreId, int resultsId, int helpId, int depositId, int withdrawId, int myAccountId, int bonusHistoryId, int transactionHistoryId)
        {
            // Set the Menu items in the home page
            List<int> lstTopMenuIds = new List<int>() { sportId, inPlayId, casinoId, liveCasinoId, vegasId, lotteryId, pokerId, boardId };
            List<int> lstMainMenuIds = new List<int>() { overviewId, eventViewId, liveScheduleId, liveScoreId, resultsId, helpId, ticketsId };
            List<int> lstAccountMenuIds = new List<int>() { depositId, withdrawId, myAccountId, betHistoryId, bonusHistoryId, transactionHistoryId };

            var home = nodeHelper.SetNodeContent(homeId, "topMenu", GetUmbValueForNode(lstTopMenuIds));

            nodeHelper.SetNodeContent(home, "mainMenu", GetUmbValueForNode(lstMainMenuIds));

            nodeHelper.SetNodeContent(home, "accountMenu", GetUmbValueForNode(lstAccountMenuIds));

            nodeHelper.SaveNode(home);
        }

        private void SetHomeSettings(Tenant tenant, int homeId, int betHistoryId, int sportId, int casinoId, int pokerId, 
            int inPlayId, int liveCasinoId, int vegasId, int lotteryId, int liveScheduleId, int boardId, int overviewId, 
            int eventViewId, int ticketsId, int liveScoreId, int resultsId, int helpId, int depositId, int withdrawId, 
            int myAccountId, int bonusHistoryId, int transactionHistoryId, int termsAndConditionsId)
        {
            var homeNode = nodeHelper.GetNodeContent(homeId);
            List<int> FirstGroupLinkIds = new List<int>() { sportId, inPlayId, casinoId, liveCasinoId, pokerId, boardId };
            List<int> ThirdGroupLinkIds = new List<int>() { ticketsId, termsAndConditionsId };

            var FirstGroup = new List<Dictionary<string, object>>();
            var SecondGroup = new List<Dictionary<string, object>>();
            var ThirdGroup = new List<Dictionary<string, object>>();

            var FirstGroupLinks = new List<Dictionary<string, object>>();
            var ThirdGroupLinks = new List<Dictionary<string, object>>();

            FirstGroupLinks.Add(new Dictionary<string, object>(){
                {"key",Guid.NewGuid().ToString()},
                {"ncContentTypeAlias","totalCodeFooterLinkSettings"},
                {"internalLink",GetUmbValueForNode(FirstGroupLinkIds)}
            });

            FirstGroup.Add(new Dictionary<string, object>(){
                {"key",Guid.NewGuid().ToString()},
                {"ncContentTypeAlias","totalCodeFooterLinkGroup"},
                {"title", "محصولات"},
                { "footerLinks",FirstGroupLinks}
            });

            SecondGroup.Add(new Dictionary<string, object>(){
                {"key",Guid.NewGuid().ToString()},
                {"ncContentTypeAlias","totalCodeFooterLinkGroup"},
                {"title", "کمک"},
                
            });

            ThirdGroupLinks.Add(new Dictionary<string, object>(){
                {"key",Guid.NewGuid().ToString()},
                {"ncContentTypeAlias","totalCodeFooterLinkSettings"},
                {"internalLink",GetUmbValueForNode(ThirdGroupLinkIds)}
            });

            ThirdGroup.Add(new Dictionary<string, object>(){
                {"key",Guid.NewGuid().ToString()},
                {"ncContentTypeAlias","totalCodeFooterLinkGroup"},
                {"title", "درباره ما"},
                { "footerLinks",ThirdGroupLinks}
            });


            homeNode.SetValue("groupLinksA", Newtonsoft.Json.JsonConvert.SerializeObject(FirstGroup), tenant.Languages.Default);
            homeNode.SetValue("groupLinksB", Newtonsoft.Json.JsonConvert.SerializeObject(SecondGroup), tenant.Languages.Default);
            homeNode.SetValue("groupLinksC", Newtonsoft.Json.JsonConvert.SerializeObject(ThirdGroup), tenant.Languages.Default);

            nodeHelper.SaveNode(homeNode);

        }
        private string GetUmbValueForNode(IList<int> ids)
        {
            string toReturn = "";
            foreach (int id in ids)
            {
                if (id > 0)
                {
                    if (toReturn != string.Empty)
                    {
                        toReturn += ",";
                    }
                    toReturn += $"umb://document/{nodeHelper.GetNodeGuid(id)}";
                }
            }
            return toReturn;
        }

        private void UpdateTenantPages(int betHistoryId = -1, int sportId = -1, int casinoId = -1, int pokerId = -1, int inPlayId = -1, int liveCasinoId = -1, int vegasId = -1, int lotteryId = -1, int liveScheduleId = -1, int boardId = -1, int zeppelinId = -1)
        {

            try
            {
                if (betHistoryId != -1)
                {
                    // SET THE TEMPLATE FOR BETTING HISTORY
                    ContentHelper.SetTemplate(ConnectorContext.ContentService, ConnectorContext.FileService, betHistoryId, _11_BettingHistoryPageIntegration.TEMPLATE_ALIAS);
                }

                if (sportId != -1)
                {
                    // SET THE TEMPLATE FOR SPORTS FEED
                    ContentHelper.SetTemplate(ConnectorContext.ContentService, ConnectorContext.FileService, sportId, _08_SportsPageIntegration.TEMPLATE_ALIAS);
                    // SET THE PAGE SVG ICON
                    var sportPage = nodeHelper.SetNodeContent(sportId, "pageCustomSvg", "icon-sport", false);
                    // Set the Game Type for each page
                    nodeHelper.SetNodeContent(sportPage, "gameType", GamePageType.Sport, false);
                    nodeHelper.SaveNode(sportPage);
                }

                if (casinoId != -1)
                {
                    // SET THE TEMPLATE FOR CASINO PAGE
                    ContentHelper.SetTemplate(ConnectorContext.ContentService, ConnectorContext.FileService, casinoId, _19_GamePages.TEMPLATE_ALIAS);
                    // SET THE PAGE SVG ICON
                    var casinoPage = nodeHelper.SetNodeContent(casinoId, "pageCustomSvg", "icon-casino", false);
                    // Set the Game Type for each page
                    nodeHelper.SetNodeContent(casinoPage, "gameType", GamePageType.Casino, false);
                    nodeHelper.SaveNode(casinoPage);
                }

                if (pokerId != -1)
                {
                    // SET THE TEMPLATE FOR Poker 
                    ContentHelper.SetTemplate(ConnectorContext.ContentService, ConnectorContext.FileService, pokerId, _19_GamePages.TEMPLATE_ALIAS);
                    // SET THE PAGE SVG ICON
                    var pokerPage = nodeHelper.SetNodeContent(pokerId, "pageCustomSvg", "icon-poker", false);
                    // Set the Game Type for each page
                    nodeHelper.SetNodeContent(pokerPage, "gameType", GamePageType.Poker, false);
                    nodeHelper.SaveNode(pokerPage);
                }

                if (inPlayId != -1)
                {
                    // SET THE TEMPLATE FOR In-Play 
                    ContentHelper.SetTemplate(ConnectorContext.ContentService, ConnectorContext.FileService, inPlayId, _26_InPlayGamePageReconfiguration.TEMPLATE_ALIAS);
                    // SET THE PAGE SVG ICON
                    var inPlayPage = nodeHelper.SetNodeContent(inPlayId, "pageCustomSvg", "icon-in-play", false);
                    // Set the Game Type for each page
                    nodeHelper.SetNodeContent(inPlayPage, "gameType", GamePageType.InPlay, false);
                    nodeHelper.SaveNode(inPlayPage);
                }

                if (liveCasinoId != -1)
                {
                    // SET THE TEMPLATE FOR Live Casino 
                    ContentHelper.SetTemplate(ConnectorContext.ContentService, ConnectorContext.FileService, liveCasinoId, _19_GamePages.TEMPLATE_ALIAS);
                    // SET THE PAGE SVG ICON
                    var liveCasinoPage = nodeHelper.SetNodeContent(liveCasinoId, "pageCustomSvg", "icon-live-casino", false);
                    // Set the Game Type for each page
                    nodeHelper.SetNodeContent(liveCasinoPage, "gameType", GamePageType.LiveCasino, false);
                    nodeHelper.SaveNode(liveCasinoPage);
                }

                if (vegasId != -1)
                {
                    // SET THE TEMPLATE FOR In-Play 
                    ContentHelper.SetTemplate(ConnectorContext.ContentService, ConnectorContext.FileService, vegasId, _19_GamePages.TEMPLATE_ALIAS);
                    // SET THE PAGE SVG ICON
                    var vegasPage = nodeHelper.SetNodeContent(vegasId, "pageCustomSvg", "icon-vegas", false);
                    // Set the Game Type for each page
                    nodeHelper.SetNodeContent(vegasPage, "gameType", GamePageType.Vegas, false);
                    nodeHelper.SaveNode(vegasPage);
                }

                if (lotteryId != -1)
                {
                    // SET THE TEMPLATE FOR Lottery 
                    ContentHelper.SetTemplate(ConnectorContext.ContentService, ConnectorContext.FileService, lotteryId, _19_GamePages.TEMPLATE_ALIAS);
                    var lotteryPage = nodeHelper.SetNodeContent(lotteryId, "pageCustomSvg", "icon-lottery", false);
                    // SET THE PAGE SVG ICON
                    nodeHelper.SetNodeContent(lotteryPage, "gameType", GamePageType.Lottery, false);
                    // Set the Game Type for each page
                    nodeHelper.SaveNode(lotteryPage);
                }

                if (liveScheduleId != -1)
                {
                    // SET THE TEMPLATE FOR live Schedule 
                    ContentHelper.SetTemplate(ConnectorContext.ContentService, ConnectorContext.FileService, liveScheduleId, _19_GamePages.TEMPLATE_ALIAS);
                }

                if (boardId != -1)
                {
                    // SET THE TEMPLATE FOR Board Games 
                    ContentHelper.SetTemplate(ConnectorContext.ContentService, ConnectorContext.FileService, boardId, _19_GamePages.TEMPLATE_ALIAS);
                    var boardPage = nodeHelper.SetNodeContent(boardId, "pageCustomSvg", "icon-board", false);
                    // SET THE PAGE SVG ICON
                    nodeHelper.SetNodeContent(boardPage, "gameType", GamePageType.Board, false);
                    // Set the Game Type for each page
                    nodeHelper.SaveNode(boardPage);
                }

                if (zeppelinId != -1)
                {
                    var zeppelinPage = nodeHelper.SetNodeContent(zeppelinId, "gameId", 1533, false);
                    nodeHelper.SetNodeContent(zeppelinPage, "gameName", "Zeppelin", false);
                    nodeHelper.SaveNode(zeppelinPage);
                }
            }
            catch
            {
            }
        }
    }
}
