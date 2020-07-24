namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Collections.Generic;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Dictionaries;
    using Umbraco.Plugins.Connector.Services;

    public class _28_ServerErrorsDictionary : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly ILogger logger;
        private readonly bool createDictionaryItems = false;


        public _28_ServerErrorsDictionary(ILocalizationService localizationService, IDomainService domainService, ILogger logger)
        {
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            this.createDictionaryItems = false;
        }

        private void CreateDictionaryItems()
        {
            try
            {
                if (createDictionaryItems)
                {
                    var language = new LanguageDictionaryService(localizationService, domainService, logger);
                    // Check if parent Key exists, and skip if true
                    if (!language.CheckExists(typeof(ServerErrors_ParentKey)))
                    {
                        // Add Dictionary Items
                        var dictionaryItems = new List<Type>
                    {
                        typeof(ServerErrors_MissingField),
                        typeof(ServerErrors_InvalidEmailFormat),
                        typeof(ServerErrors_InvalidDate),
                        typeof(ServerErrors_InvalidAge),
                        typeof(ServerErrors_MobileOrEmailRequired),
                        typeof(ServerErrors_InvalidCountry),
                        typeof(ServerErrors_InvalidCurrency),
                        typeof(ServerErrors_InvalidLanguage),
                        typeof(ServerErrors_InvalidTimeZone),
                        typeof(ServerErrors_ExistingCustomer),
                        typeof(ServerErrors_EmailNotFound),
                        typeof(ServerErrors_EmailSendFail),
                        typeof(ServerErrors_ChangePassword),
                        typeof(ServerErrors_InvalidCustomer),
                        typeof(ServerErrors_InvalidOldPassword),
                        typeof(ServerErrors_MatchingOldAndNewPassword),
                        typeof(ServerErrors_InvalidCustomerStatus),
                        typeof(ServerErrors_CustomerNotFound),
                        typeof(ServerErrors_VerificationRecordNotFound),
                        typeof(ServerErrors_VerificationEmailExpired),
                        typeof(ServerErrors_ValidationCodeExpired),
                        typeof(ServerErrors_ValidationCodeSendFail),
                        typeof(ServerErrors_SMSSendFail),
                        typeof(ServerErrors_InvalidMobileNumber),
                        typeof(ServerErrors_InvalidVerificationEmail),
                        typeof(ServerErrors_VerificationCodeLimitExceeded),
                        typeof(ServerErrors_MobileNumberNotFound),
                        typeof(ServerErrors_ValidationCodeInvalid),
                        typeof(ServerErrors_UnhandledError),
                        typeof(ServerErrors_FieldRequired),
                        typeof(ServerErrors_BelowMinimumWithdrawalAmount),
                        typeof(ServerErrors_InsufficientBalance),
                        typeof(ServerErrors_UsernameCannotBeEmailAddress)


                    };
                        language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items
                        ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Dictionary Items", $"Dictionaries Created");
                    }

                    if (!language.CheckExists(typeof(ServerErrors_ConnectionTimeout)))
                    {
                        // Add Dictionary Items
                        var dictionaryItems1 = new List<Type>
                            {
                                typeof(ServerErrors_ConnectionTimeout)
                            };
                        language.CreateDictionaryItems(dictionaryItems1); // Create Dictionary Items
                        ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Dictionary Items", $"Dictionaries Created");
                    }

                    if (!language.CheckExists(typeof(ServerErrors_InvalidLogin)))
                    {
                        // Add Dictionary Items
                        var dictionaryItems2 = new List<Type>
                            {
                                typeof(ServerErrors_InvalidLogin)
                            };
                        language.CreateDictionaryItems(dictionaryItems2); // Create Dictionary Items
                        ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Dictionary Items", $"Dictionaries Created");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(_28_ServerErrorsDictionary), ex.Message);
                logger.Error(typeof(_28_ServerErrorsDictionary), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            CreateDictionaryItems();
        }

        public void Terminate() { }
    }
}
