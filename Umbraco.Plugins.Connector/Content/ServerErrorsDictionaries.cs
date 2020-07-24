namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Collections.Generic;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Dictionaries;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;
    using System.Linq;

    public class _25_ServerErrorsDictionaries : IComponent
    {
        private readonly ILogger logger;
        private readonly bool createDictionaryItems = true;

        public _25_ServerErrorsDictionaries(ILogger logger)
        {
            this.logger = logger;
        }

        private void InsertDictionaries()
        {
            try
            {
                if (createDictionaryItems)
                {
                    var language = new LanguageDictionaryService(ConnectorContext.LocalizationService, ConnectorContext.DomainService, ConnectorContext.Logger);
                    var dictionaryItems = new List<Type>();
                    // Check if parent Key exists, and skip if true
                    if (!language.CheckExists(typeof(ServerErrors_ParentKey)))
                        dictionaryItems.Add(typeof(ServerErrors_ParentKey));

                    if (!language.CheckExists(typeof(ServerErrors_MissingField)))
                        dictionaryItems.Add(typeof(ServerErrors_MissingField));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidEmailFormat)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidEmailFormat));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidDate)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidDate));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidAge)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidAge));

                    if (!language.CheckExists(typeof(ServerErrors_MobileOrEmailRequired)))
                        dictionaryItems.Add(typeof(ServerErrors_MobileOrEmailRequired));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidCountry)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidCountry));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidCurrency)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidCurrency));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidLanguage)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidLanguage));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidTimeZone)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidTimeZone));

                    if (!language.CheckExists(typeof(ServerErrors_ExistingCustomer)))
                        dictionaryItems.Add(typeof(ServerErrors_ExistingCustomer));

                    if (!language.CheckExists(typeof(ServerErrors_EmailNotFound)))
                        dictionaryItems.Add(typeof(ServerErrors_EmailNotFound));

                    if (!language.CheckExists(typeof(ServerErrors_EmailSendFail)))
                        dictionaryItems.Add(typeof(ServerErrors_EmailSendFail));

                    if (!language.CheckExists(typeof(ServerErrors_ChangePassword)))
                        dictionaryItems.Add(typeof(ServerErrors_ChangePassword));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidCustomer)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidCustomer));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidOldPassword)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidOldPassword));

                    if (!language.CheckExists(typeof(ServerErrors_MatchingOldAndNewPassword)))
                        dictionaryItems.Add(typeof(ServerErrors_MatchingOldAndNewPassword));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidCustomerStatus)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidCustomerStatus));

                    if (!language.CheckExists(typeof(ServerErrors_CustomerNotFound)))
                        dictionaryItems.Add(typeof(ServerErrors_CustomerNotFound));

                    if (!language.CheckExists(typeof(ServerErrors_VerificationRecordNotFound)))
                        dictionaryItems.Add(typeof(ServerErrors_VerificationRecordNotFound));

                    if (!language.CheckExists(typeof(ServerErrors_VerificationEmailExpired)))
                        dictionaryItems.Add(typeof(ServerErrors_VerificationEmailExpired));

                    if (!language.CheckExists(typeof(ServerErrors_ValidationCodeExpired)))
                        dictionaryItems.Add(typeof(ServerErrors_ValidationCodeExpired));

                    if (!language.CheckExists(typeof(ServerErrors_ValidationCodeSendFail)))
                        dictionaryItems.Add(typeof(ServerErrors_ValidationCodeSendFail));

                    if (!language.CheckExists(typeof(ServerErrors_SMSSendFail)))
                        dictionaryItems.Add(typeof(ServerErrors_SMSSendFail));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidMobileNumber)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidMobileNumber));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidVerificationEmail)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidVerificationEmail));

                    if (!language.CheckExists(typeof(ServerErrors_VerificationCodeLimitExceeded)))
                        dictionaryItems.Add(typeof(ServerErrors_VerificationCodeLimitExceeded));

                    if (!language.CheckExists(typeof(ServerErrors_MobileNumberNotFound)))
                        dictionaryItems.Add(typeof(ServerErrors_MobileNumberNotFound));

                    if (!language.CheckExists(typeof(ServerErrors_ValidationCodeInvalid)))
                        dictionaryItems.Add(typeof(ServerErrors_ValidationCodeInvalid));

                    if (!language.CheckExists(typeof(ServerErrors_UnhandledError)))
                        dictionaryItems.Add(typeof(ServerErrors_UnhandledError));

                    if (!language.CheckExists(typeof(ServerErrors_FieldRequired)))
                        dictionaryItems.Add(typeof(ServerErrors_FieldRequired));

                    if (!language.CheckExists(typeof(ServerErrors_BelowMinimumWithdrawalAmount)))
                        dictionaryItems.Add(typeof(ServerErrors_BelowMinimumWithdrawalAmount));

                    if (!language.CheckExists(typeof(ServerErrors_InsufficientBalance)))
                        dictionaryItems.Add(typeof(ServerErrors_InsufficientBalance));

                    if (!language.CheckExists(typeof(ServerErrors_UsernameCannotBeEmailAddress)))
                        dictionaryItems.Add(typeof(ServerErrors_UsernameCannotBeEmailAddress));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidLogin)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidLogin));

                    if (!language.CheckExists(typeof(ServerErrors_Other)))
                        dictionaryItems.Add(typeof(ServerErrors_Other));

                    if (!language.CheckExists(typeof(ServerErrors_AccountLocked)))
                        dictionaryItems.Add(typeof(ServerErrors_AccountLocked));

                    if (!language.CheckExists(typeof(ServerErrors_ExceedsMaximalWithdrawal)))
                        dictionaryItems.Add(typeof(ServerErrors_ExceedsMaximalWithdrawal));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidCard)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidCard));

                    if (!language.CheckExists(typeof(ServerErrors_InvalidIBAN)))
                        dictionaryItems.Add(typeof(ServerErrors_InvalidIBAN));

                    language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items

                    ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Dictionary Item", $"Server Error Dictionary Items have been created/updated");

                }

            }
            catch (Exception ex)
            {
                logger.Error(typeof(_25_ServerErrorsDictionaries), ex.Message);
                logger.Error(typeof(_25_ServerErrorsDictionaries), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            InsertDictionaries();
        }

        public void Terminate() { }
    }
}
