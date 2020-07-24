
namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Dictionaries;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web.PropertyEditors;

   public class _40_GenericInfoPageDocumentType : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly IFileService fileService;
        private readonly ILogger logger;
        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;


        public static string
                        DOCUMENT_TYPE_ALIAS = "totalCodeGenericInfoPage",
                        DOCUMENT_TYPE_NAME = "Generic Info Page",
                        DOCUMENT_TYPE_CONTAINER = "Total Code Container",
                        PARENT_TEMPLATE_ALIAS = "totalCodeLayout",
                        TEMPLATE_ALIAS = "totalCodeGenericInfoPageTemplate",
                        TEMPLATE_NAME = "Total Code Generic Info Page Template",
                        NESTED_DOCUMENT_TYPE_ICON = "icon-universal color-green",
                        NESTED_DOCUMENT_TYPE_PARENT_ALIAS = "totalCodeBasePage";

        public _40_GenericInfoPageDocumentType(ILocalizationService localizationService, IDomainService domainService, IFileService fileService, ILogger logger, IContentTypeService contentTypeService, IDataTypeService dataTypeService)
        {
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            this.contentTypeService = contentTypeService;
            this.fileService = fileService;
            this.dataTypeService = dataTypeService;
        }

        public void Initialize()
        {

            try
            {
                var container = contentTypeService.GetContainers(DOCUMENT_TYPE_CONTAINER, 1).FirstOrDefault();
                int containerId = -1;

                if (container != null)
                    containerId = container.Id;


                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (contentType != null)
                    return;

                    const string CONTENT_TAB = "CONTENT";

                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = DOCUMENT_TYPE_NAME,
                        Alias = DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = true,
                        Description = "",
                        Icon = NESTED_DOCUMENT_TYPE_ICON,
                        ParentId = contentTypeService.Get(NESTED_DOCUMENT_TYPE_PARENT_ALIAS).Id,
                        SortOrder = 0,
                        Variations = ContentVariation.Culture,
                    };
                

                // Create the Template if it doesn't exist
                if (fileService.GetTemplate(TEMPLATE_ALIAS) == null)
                {
                    //then create the template
                    Template newTemplate = new Template(TEMPLATE_NAME, TEMPLATE_ALIAS);
                    ITemplate masterTemplate = fileService.GetTemplate(PARENT_TEMPLATE_ALIAS);
                    newTemplate.SetMasterTemplate(masterTemplate);
                    fileService.SaveTemplate(newTemplate);
                }

                var template = fileService.GetTemplate(TEMPLATE_ALIAS);
                docType.AllowedTemplates = new List<ITemplate> { template };
                docType.SetDefaultTemplate(template);

                docType.AddPropertyGroup(CONTENT_TAB);

                #region Content

                PropertyType CotentPageTitlePropType = new PropertyType(dataTypeService.GetDataType(-88), "genericInfoPageTitle")
                {
                    Name = "Page Title",
                    Variations = ContentVariation.Culture
                };
                docType.AddPropertyType(CotentPageTitlePropType, CONTENT_TAB);


                PropertyType CotentPageContentPropType = new PropertyType(dataTypeService.GetDataType(-87), "genericInfoPageContent")
                {
                    Name = "Page Content",
                    Variations = ContentVariation.Culture
                };
                docType.AddPropertyType(CotentPageContentPropType, CONTENT_TAB);

                #endregion
                contentTypeService.Save(docType);
                ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_NAME}' has been created");

                ContentHelper.CopyPhysicalAssets(new GenericInfoPageEmbeddedResources());

            }

            catch (Exception ex)
            {
                logger.Error(typeof(_40_GenericInfoPageDocumentType), ex.Message);
                logger.Error(typeof(_40_GenericInfoPageDocumentType), ex.StackTrace);
            }
        }

        public void Terminate()
        {
            
        }
    }
}
