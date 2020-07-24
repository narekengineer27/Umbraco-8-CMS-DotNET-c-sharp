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

    public class _02_HomeDocumentTypePhase2Milestone6 : IComponent
    {
        public static string
                    DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
                    CONTENT_TAB = "Content",
                    CONTAINER = "Total Code Container";


        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        private readonly bool createDictionaryItems = false;

        public _02_HomeDocumentTypePhase2Milestone6(IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.fileService = fileService;
            this.logger = logger;
            this.createDictionaryItems = false;
        }

        private void UpdateHomeDocumentType()
        {
            try
            {
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);

                var container = contentTypeService.GetContainers(CONTAINER, 1).FirstOrDefault();

                if (contentType != null)
                {
                    if (contentType.PropertyTypes.ToList().SingleOrDefault(x => x.Alias == "termsAndConditionsMessage") == null)
                    {
                        // Add Document Type Properties
                        #region Tenant Home Page Content
                        PropertyType termsAndConditionsMessagePropType = new PropertyType(dataTypeService.GetDataType(-88), "termsAndConditionsMessage")
                        {
                            Name = "Terms and Conditions",
                            Description = "Message to display for required accepting Terms and Conditions",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(termsAndConditionsMessagePropType, CONTENT_TAB);
                        #endregion

                        contentTypeService.Save(contentType);
                        ConnectorContext.AuditService.Add(AuditType.New, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");

                        ContentHelper.CopyPhysicalAssets(new RegisterUpdateHomeEmbeddedResources());
                        //CreateMasterTemplate();
                    }

                    var telegramUsername = contentType.PropertyTypes.ToList().SingleOrDefault(x => x.Alias == "telegramUsername");
                    if (telegramUsername != null )
                    {
                        if (telegramUsername.Name != "Telegram URL")
                        {
                            telegramUsername.Name = "Telegram URL";
                            telegramUsername.Description = "";

                            telegramUsername.ValidationRegExp = "";
                            contentType.AddPropertyType(telegramUsername);

                            contentTypeService.Save(contentType);
                        }
                        else if(!string.IsNullOrEmpty(telegramUsername.ValidationRegExp))
                        {
                            telegramUsername.ValidationRegExp = "";
                            contentType.AddPropertyType(telegramUsername);

                            contentTypeService.Save(contentType);
                        }
                    }

                    var helpdeskTelegramAccount = contentType.PropertyTypes.ToList().SingleOrDefault(x => x.Alias == "helpdeskTelegramAccount");
                    if (helpdeskTelegramAccount != null )
                    {
                        if (helpdeskTelegramAccount.Name != "HelpDesk Telegram Bot")
                        {
                            helpdeskTelegramAccount.Name = "HelpDesk Telegram Bot";
                            helpdeskTelegramAccount.Description = "";

                            helpdeskTelegramAccount.ValidationRegExp = "";
                            contentType.AddPropertyType(helpdeskTelegramAccount);

                            contentTypeService.Save(contentType);
                        }
                        else if(!string.IsNullOrEmpty(helpdeskTelegramAccount.ValidationRegExp))
                        {
                            helpdeskTelegramAccount.ValidationRegExp = "";
                            contentType.AddPropertyType(helpdeskTelegramAccount);

                            contentTypeService.Save(contentType);
                        }
                    }

                    var whatsAppNumber = contentType.PropertyTypes.ToList().SingleOrDefault(x => x.Alias == "whatsAppNumber");
                    if (whatsAppNumber != null)
                    {
                        if (whatsAppNumber.Name != "Whatsapp URL")
                        {
                            whatsAppNumber.Name = "Whatsapp URL";
                            whatsAppNumber.Description = "";

                            whatsAppNumber.ValidationRegExp = "";
                            contentType.AddPropertyType(whatsAppNumber);

                            contentTypeService.Save(contentType);
                        }
                        else if(!string.IsNullOrEmpty(whatsAppNumber.ValidationRegExp))
                        {
                            whatsAppNumber.ValidationRegExp = "";
                            contentType.AddPropertyType(whatsAppNumber);

                            contentTypeService.Save(contentType);
                        }
                       
                    }
                    //var 
                    //var 
                }

                if (createDictionaryItems)
                {
                    var language = new LanguageDictionaryService(ConnectorContext.LocalizationService, ConnectorContext.DomainService, ConnectorContext.Logger);
                    // Check if parent Key exists, and skip if true
                    if (!language.CheckExists(typeof(Home_ParentKey)))
                    {
                        // Add Dictionary Items
                        var dictionaryItems = new List<Type>
                    {
                        typeof(Home_ParentKey),
                        typeof(Home_Register),
                        typeof(Site_ParentKey),
                        typeof(Site_AlreadyHaveAccount),
                        typeof(Register_ParentKey),
                        typeof(Register_RegisterTitle),
                        typeof(Register_RegisterStep),
                        typeof(Register_RegisterOf),
                        typeof(Register_PhoneNumber),
                        typeof(Register_PhoneNumberPlaceholder),
                        typeof(Register_IsMandatory),
                        typeof(Register_Continue),
                        typeof(Register_PhoneNumberInstructions),
                        typeof(Register_VerificationCode),
                        typeof(Register_VerificationCodePlaceholder),
                        typeof(Register_EnterCode),
                        typeof(Register_Enter6DigitVerificationCode),
                        typeof(Register_EnterVerificationCode),
                        typeof(Register_ResendCode),
                        typeof(Register_Wait),
                        typeof(Register_VerificationCodeInvalidOrExpired),
                        typeof(Register_Email),
                        typeof(Register_EmailPlaceholder),
                        typeof(Register_Password),
                        typeof(Register_PasswordPlaceholder),
                        typeof(Register_ConfirmPassword),
                        typeof(Register_FirstName),
                        typeof(Register_FirstNamePlaceholder),
                        typeof(Register_LastName),
                        typeof(Register_LastNamePlaceholder),
                        typeof(Register_Username),
                        typeof(Register_UsernamePlaceholder),
                        typeof(Register_Gender),
                        typeof(Register_DateOfBirth),
                        typeof(Register_DateOfBirthDay),
                        typeof(Register_DateOfBirthMonth),
                        typeof(Register_DateOfBirthMonthJanuary),
                        typeof(Register_DateOfBirthMonthFebruary),
                        typeof(Register_DateOfBirthMonthMarch),
                        typeof(Register_DateOfBirthMonthApril),
                        typeof(Register_DateOfBirthMonthMay),
                        typeof(Register_DateOfBirthMonthJune),
                        typeof(Register_DateOfBirthMonthJuly),
                        typeof(Register_DateOfBirthMonthAugust),
                        typeof(Register_DateOfBirthMonthSeptember),
                        typeof(Register_DateOfBirthMonthOctober),
                        typeof(Register_DateOfBirthMonthNovember),
                        typeof(Register_DateOfBirthMonthDecember),
                        typeof(Register_DateOfBirthYear),
                        typeof(Register_IAgreeWithThe),
                        typeof(Register_TermsAndConditions),
                        typeof(Register_Finish),
                        typeof(Register_IncorrectOrInvalid),
                        typeof(Register_VerifyEmailSentToTitle),
                        typeof(Register_VerifyEmailSentTo),
                        typeof(Register_VerifyEmailSentToPleaseClick),
                        typeof(Register_ResendVerificationEmail),
                        typeof(Register_ResendVerificationEmailSent),
                        typeof(Register_ChangeEmail),
                        typeof(Register_IsInvalid),
                        typeof(Register_Title),
                        typeof(Register_Address1),
                        typeof(Register_Address1Placeholder),
                        typeof(Register_Address2),
                        typeof(Register_Address2Placeholder),
                        typeof(Register_Address3),
                        typeof(Register_Address3Placeholder),
                        typeof(Register_CityOrTown),
                        typeof(Register_CityOrTownPlaceholder),
                        typeof(Register_PostalCode),
                        typeof(Register_PostalCodePlaceholder),
                        typeof(Register_Country),
                        typeof(Register_Preferences),
                        typeof(Register_Language),
                        typeof(Register_Currency),
                        typeof(Register_OddsDisplay),
                        typeof(Register_TimeZone),
                        typeof(Register_BonusCode),
                        typeof(Register_BonusCodePlaceholder),
                        typeof(Register_Referrer),
                        typeof(Register_ReferrerPlaceholder),
                        typeof(Register_ReceiveNotifications),
                        typeof(Register_ReceiveNotificationsViaInPlatformMessages),
                        typeof(Register_ReceiveNotificationsViaEmail),
                        typeof(Register_ReceiveNotificationsViaSMS),
                        typeof(Register_CookiesPolicy),
                        typeof(Register_PrivacyPolicy),
                        typeof(Register_MinimunAge),
                        typeof(Register_Age),
                        typeof(Register_AgeYearsOld),
                        typeof(Register_NewEmail),
                        typeof(Register_CurrentEmail)
                    };
                        language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items

                        ConnectorContext.AuditService.Add(AuditType.Save, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated, and Dictionaries Created");

                    }
                }

                var oldHome = contentTypeService.Get(HomeDocumentType.DOCUMENT_TYPE_ALIAS);
                if (oldHome != null)
                {
                    // remove old home document type
                    contentTypeService.Delete(contentTypeService.Get(HomeDocumentType.DOCUMENT_TYPE_ALIAS));
                    // remove old home template
                    fileService.DeleteTemplate(HomeDocumentType.TEMPLATE_ALIAS);
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(_02_HomeDocumentTypePhase2Milestone6), ex.Message);
                logger.Error(typeof(_02_HomeDocumentTypePhase2Milestone6), ex.StackTrace);
            }
        }

        [Obsolete]
        public void CreateMasterTemplate()
        {
            var layoutAlias = "Layout";
            ITemplate newTemplate = fileService.GetTemplate(layoutAlias);
            if (newTemplate == null)
            {
                // then create the master template
                newTemplate = new Template(layoutAlias, layoutAlias);
                fileService.SaveTemplate(newTemplate);
            }
            // assign templates to master
            var homeTemplate = fileService.GetTemplate(HomeDocumentType.TEMPLATE_ALIAS);
            homeTemplate.SetMasterTemplate(newTemplate);

            fileService.SaveTemplate(homeTemplate);
        }

        public void Initialize()
        {
            UpdateHomeDocumentType();
        }

        public void Terminate() { }
    }
}
