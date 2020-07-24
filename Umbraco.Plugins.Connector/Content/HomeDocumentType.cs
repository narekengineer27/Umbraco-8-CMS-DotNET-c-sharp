namespace Umbraco.Plugins.Connector.Content
{
    using System.Linq;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Services;
    using Umbraco.Core.Composing;
    using System.Collections.Generic;
    using Umbraco.Plugins.Connector.Helpers;
    public class HomeDocumentType : IComponent
    {
        public static string
            CONTAINER = "Total Code Container",
            DOCUMENT_TYPE_ALIAS = "TotalCodeTenantHome",
            DOCUMENT_TYPE_NAME = "Total Code Tenant Home",
            DOCUMENT_TYPE_DESCRIPTION = "Total Code Tenant Home Page (Simple Page)",
            ICON = "icon-home color-blue",
            TEMPLATE_ALIAS = "TotalCodeTenantHomeTemplate",
            TEMPLATE_NAME = "Total Code Tenant Home Template",
            TENANT_TAB = "Tenant Info",
            CONTENT_TAB = "Content";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public HomeDocumentType(IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.fileService = fileService;
            this.logger = logger;
        }

        private void CreateHomeDocumentType()
        {
            try
            {
                var container = contentTypeService.GetContainers(CONTAINER, 1).FirstOrDefault();
                int containerId = 0;

                if (container == null)
                {
                    var newcontainer = contentTypeService.CreateContainer(-1, CONTAINER);

                    if (newcontainer.Success)
                        containerId = newcontainer.Result.Entity.Id;
                }
                else
                {
                    containerId = container.Id;
                }

                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {
                    //http://refreshwebsites.co.uk/blog/umbraco-document-types-explained-in-60-seconds/
                    //https://our.umbraco.org/forum/developers/api-questions/43278-programmatically-creating-a-document-type
                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = DOCUMENT_TYPE_NAME,
                        Alias = DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = true,
                        Description = DOCUMENT_TYPE_DESCRIPTION,
                        Icon = ICON,
                        SortOrder = 0,
                        Variations = ContentVariation.Culture
                    };

                    // Create the Template if it doesn't exist
                    if (fileService.GetTemplate(TEMPLATE_ALIAS) == null)
                    {
                        //then create the template
                        Template newTemplate = new Template(TEMPLATE_NAME, TEMPLATE_ALIAS);
                        fileService.SaveTemplate(newTemplate);
                    }

                    // Set templates for document type
                    var template = fileService.GetTemplate(TEMPLATE_ALIAS);
                    docType.AllowedTemplates = new List<ITemplate> { template };
                    docType.SetDefaultTemplate(template);
                    docType.AddPropertyGroup(CONTENT_TAB);
                    docType.AddPropertyGroup(TENANT_TAB);

                    // Set Document Type Properties
                    #region Tenant Home Page Content
                    PropertyType brandLogoPropType = new PropertyType(dataTypeService.GetDataType(1043), "brandLogo")
                    {
                        Name = "Brand Logo",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(brandLogoPropType, CONTENT_TAB);

                    PropertyType homeContentProType = new PropertyType(dataTypeService.GetDataType(-87), "homeContent")
                    {
                        Name = "Content",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(homeContentProType, CONTENT_TAB);
                    #endregion

                    #region Tenant Info Tab
                    PropertyType tenantUidPropType = new PropertyType(dataTypeService.GetDataType(-92), "tenantUid")
                    {
                        Name = "Tenant Uid",
                        Description = "Tenant Unique Id",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(tenantUidPropType, TENANT_TAB);

                    PropertyType brandNamePropType = new PropertyType(dataTypeService.GetDataType(-92), "brandName")
                    {
                        Name = "Brand Name",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(brandNamePropType, TENANT_TAB);

                    PropertyType domainPropType = new PropertyType(dataTypeService.GetDataType(-92), "domain")
                    {
                        Name = "Domain",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(domainPropType, TENANT_TAB);

                    PropertyType subDomainPropType = new PropertyType(dataTypeService.GetDataType(-92), "subDomain")
                    {
                        Name = "Sub Domain",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(subDomainPropType, TENANT_TAB);

                    PropertyType appIdPropType = new PropertyType(dataTypeService.GetDataType(-92), "appId")
                    {
                        Name = "App Id",
                        Variations = ContentVariation.Nothing
                    };

                    docType.AddPropertyType(appIdPropType, TENANT_TAB);

                    PropertyType apiKeyPropType = new PropertyType(dataTypeService.GetDataType(-92), "apiKey")
                    {
                        Name = "Api Key",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(apiKeyPropType, TENANT_TAB);

                    PropertyType defaultLanguagePropType = new PropertyType(dataTypeService.GetDataType(-92), "defaultLanguage")
                    {
                        Name = "Default Language",
                        Description = "System Default Language",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(defaultLanguagePropType, TENANT_TAB);

                    PropertyType languagestPropType = new PropertyType(dataTypeService.GetDataType(-92), "languages")
                    {
                        Name = "Alternate Languages",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(languagestPropType, TENANT_TAB);

                    PropertyType tenantStatusPropType = new PropertyType(dataTypeService.GetDataType(-92), "tenantStatus")
                    {
                        Name = "Tenant Status",
                        Description = "Total Code Tenant Status",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(tenantStatusPropType, TENANT_TAB);
                    #endregion

                    contentTypeService.Save(docType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_NAME}' has been created");

                    ContentHelper.CopyPhysicalAssets(new EmbeddedResources());
                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(HomeDocumentType), ex.Message);
                logger.Error(typeof(HomeDocumentType), ex.StackTrace);
            }
        }

        public void SetContentVariations()
        {
            var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
            if (contentType != null)
            {
                ContentType docType = (ContentType)contentType;
                docType.Variations = ContentVariation.Culture;
                contentTypeService.Save(docType);
            }
        }

        public void Initialize()
        {
            // June 27 2019, adjustment to make it work with Renante's work
            //CreateHomeDocumentType();
        }

        public void Terminate() { }
    }
}
