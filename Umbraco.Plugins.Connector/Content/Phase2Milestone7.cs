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

    public class _06_Phase2Milestone7 : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly ILogger logger;
        private readonly IContentTypeService contentTypeService;

        private readonly bool createDictionaryItems = false;


        public _06_Phase2Milestone7(ILocalizationService localizationService, IDomainService domainService, ILogger logger, IContentTypeService contentTypeService)
        {
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            this.contentTypeService = contentTypeService;
            this.createDictionaryItems = false;
        }

        private void CreateMilestoneItems()
        {
            try
            {
                if (createDictionaryItems)
                {
                    var language = new LanguageDictionaryService(localizationService, domainService, logger);
                    // Check if parent Key exists, and skip if true
                    if (!language.CheckExists(typeof(Forgot_ParentKey)))
                    {
                        // Add Dictionary Items
                        var dictionaryItems = new List<Type>
                    {
                        typeof(Forgot_ParentKey),
                        typeof(Forgot_EnterCaptcha),
                        typeof(Forgot_Captcha),
                        typeof(Forgot_ForgotUsernameSuccess),
                        typeof(Forgot_ForgotUsernameFailure),
                        typeof(Forgot_ChangePassword),
                        typeof(Forgot_ConfirmNewPassword),
                        typeof(Forgot_NewPassword),
                        typeof(Forgot_ChangePasswordSuccess),
                        typeof(Forgot_ForgotPasswordEmailSentToTitle),
                        typeof(Forgot_ForgotPasswordEmailSentTo),
                        typeof(Forgot_ResendResetPasswordEmail),
                        typeof(Forgot_ResendResetPasswordEmailSent),
                        typeof(Forgot_ForgotPasswordInstructions),
                        typeof(Forgot_ForgotUsernameInstructions),
                        typeof(Forgot_ForgotPassword),
                        typeof(Forgot_UsernameRequestSuccessful),

                        typeof(Others_ConfirmEmail),
                        typeof(Others_Successful)
                    };
                        language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items
                    }

                    if (!language.CheckExists(typeof(Reset_ParentKey)))
                    {
                        var resetPasswordViaEmailDictionary = new List<Type>
                        {
                            typeof(Reset_ParentKey),
                            typeof(Reset_ChangePassword),
                            typeof(Reset_ResetPasswordSuccess),
                            typeof(Reset_ResetPasswordFailure)
                        };
                        language.CreateDictionaryItems(resetPasswordViaEmailDictionary); // Create Dictionary Items
                    }
                    ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Reset Password Dictionary Items", "Dictionaries Created");

                    if (!language.CheckExists(typeof(ServerErrors_ParentKey)))
                    {
                        var serverErrorsDictionary = new List<Type>
                        {
                            typeof(ServerErrors_ParentKey),
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
                            typeof(ServerErrors_UnhandledError)
                        };
                        language.CreateDictionaryItems(serverErrorsDictionary); // Create Dictionary Items
                        ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Server Error Dictionary Items", "Dictionaries Created");
                    }

                    var firstEmbeddedResource = new Milestone7EmbeddedResources().Resources[0];
                    if (!ContentHelper.AssetAlreadyExists(firstEmbeddedResource.FileName, firstEmbeddedResource.OutputDirectory))
                    {
                        ContentHelper.CopyPhysicalAssets(new Milestone7EmbeddedResources());
                        ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Templates", "Templates Created");
                    }

                    // changed Email Confirmation page to remove from Allowed in Root
                    var confirmEmailDocType = contentTypeService.Get(_03_ConfirmEmailDocumentType.DOCUMENT_TYPE_ALIAS);
                    if (!confirmEmailDocType.AllowedAsRoot)
                    {
                        confirmEmailDocType.AllowedAsRoot = false;
                        contentTypeService.Save(confirmEmailDocType);
                    }

                    // delete en-GB
                    var enGB = localizationService.GetLanguageByIsoCode("en-GB");
                    if (enGB != null)
                        localizationService.Delete(enGB);
                }

            }
            catch (Exception ex)
            {
                logger.Error(typeof(_06_Phase2Milestone7), ex.Message);
                logger.Error(typeof(_06_Phase2Milestone7), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            CreateMilestoneItems();
        }

        public void Terminate() { }
    }
}
