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

    public class _04_RenantePhase2Milestone6DictionaryMigration : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly ILogger logger;
        private readonly bool createDictionaryItems = false;


        public _04_RenantePhase2Milestone6DictionaryMigration(ILocalizationService localizationService, IDomainService domainService, ILogger logger)
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
                    if (!language.CheckExists(typeof(Others_ParentKey)))
                    {
                        // Add Dictionary Items
                        var dictionaryItems = new List<Type>
                    {
                        typeof(Others_ParentKey),
                        typeof(Others_Deposit),
                        typeof(Others_Login),
                        typeof(Others_Register),
                        typeof(Others_Search),
                        typeof(Others_ForgotPassword),
                        typeof(Others_Balance),
                        typeof(Others_Cash),
                        typeof(Others_Withdrawable),
                        typeof(Others_BetCredits),
                        typeof(Others_Change),
                        typeof(Others_Edit),
                        typeof(Others_MarketingPreferences),
                        typeof(Others_ChangePassword),
                        typeof(Others_WhatCanWeHelp),
                        typeof(Others_Back),
                        typeof(Others_CreditCard),
                        typeof(Others_Delete),
                        typeof(Others_AddNewPaymentMethod),
                        typeof(Others_PaymentMethod),
                        typeof(Others_Status),
                        typeof(Others_StartDate),
                        typeof(Others_EndDate),
                        typeof(Others_ApplyFilters),
                        typeof(Others_ResetFilters),
                        typeof(Others_Filter),
                        typeof(Others_Rejected),
                        typeof(Others_IDCertificate),
                        typeof(Others_UploadedDocumentRules),
                        typeof(Others_Verified),
                        typeof(Others_Passport),
                        typeof(Others_Upload),
                        typeof(Others_FrontBack),
                        typeof(Others_Pending),
                        typeof(Others_BankStatement),
                        typeof(Others_UtilityBill),
                        typeof(Others_Receipt),
                        typeof(Others_FirstAndLastName),
                        typeof(Others_Date),
                        typeof(Others_Amount),
                        typeof(Others_TransactionNumber),
                        typeof(Others_WithdrawCancelled),
                        typeof(Others_GotIt),
                        typeof(Others_AreYouSureWant),
                        typeof(Others_Cancel),
                        typeof(Others_This),
                        typeof(Others_Withdraw),
                        typeof(Others_Yes),
                        typeof(Others_No),
                        typeof(Others_PasswordChanged),
                        typeof(Others_GetStarted),
                        typeof(Others_NewPaymentMethodAdded),
                        typeof(Others_PasswordResetLinkSent),
                        typeof(Others_PasswordResetLinkSentTo),
                        typeof(Others_ProceedToLogin),
                        typeof(Others_MessageSent),
                        typeof(Others_ResetPassword),
                        typeof(Others_ConfirmNewPassword),
                        typeof(Others_SaveNewPassword),
                        typeof(Others_ForgotUsernameEmail),
                        typeof(Others_EnterMobileSendUsername),
                        typeof(Others_EnterEmailToSendLink),
                        typeof(Others_AccountHolderName),
                        typeof(Others_CardNumber16Digits),
                        typeof(Others_ChangePhone),
                        typeof(Others_EditUserInfo),
                        typeof(Others_Male),
                        typeof(Others_Female),
                        typeof(Others_UserInfoUpdated),
                        typeof(Others_EnterPasswordToChange),
                        typeof(Others_SuccessChanged),
                        typeof(Others_PleaseEnterPasswordToChange),
                        typeof(Others_ChangeEmailVerifyNewEmail),
                        typeof(Others_WillNotBeReplacedUntil),
                        typeof(Others_NewPassword),
                        typeof(Others_WeWillRespondToEmail),
                        typeof(Others_Within24Hours),
                        typeof(Others_ContactUs),
                        typeof(Others_Subject),
                        typeof(Others_Message),
                        typeof(Others_Uploading),
                        typeof(Others_AttachFile),
                        typeof(Others_Send),
                        typeof(Others_NewTicket),
                        typeof(Others_NewMessage),
                        typeof(Others_Close),
                        typeof(Others_Ticket),
                        typeof(Others_Created),
                        typeof(Others_WeWillRespondTo),
                        typeof(Others_TicketSuccessClose),
                        typeof(Others_Settings),
                        typeof(Others_Fractional),
                        typeof(Others_ChoosePaymentMethod),
                        typeof(Others_Exclusive),
                        typeof(Others_AccessToExclusivePoints),
                        typeof(Others_Rewards),
                        typeof(Others_OtherPartners),
                        typeof(Others_MarketResearch),
                        typeof(Others_Tipping),
                        typeof(Others_Reply),
                        typeof(Others_Type),
                        typeof(Others_AmountToWithdraw),
                        typeof(Others_WithdrawWarning)
                    };
                        language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items

                        if (!language.CheckExists(typeof(Others_DepositBonusCode)))
                        {
                            var latestAddition = new List<Type>
                        {
                            typeof(Others_DepositBonusCode)
                        };
                            language.CreateDictionaryItems(latestAddition); // Create Dictionary Items
                        }

                        ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Dictionary Items", $"Dictionaries Created");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(_04_RenantePhase2Milestone6DictionaryMigration), ex.Message);
                logger.Error(typeof(_04_RenantePhase2Milestone6DictionaryMigration), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            CreateDictionaryItems();
        }

        public void Terminate() { }
    }
}
