namespace Umbraco.Plugins.Connector.Content
{
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Core.Services;
    using Umbraco.Web.PropertyEditors;

    public class _31_MetaTags : IComponent
    {
        public static string
            DATA_TYPE_CONTAINER = "Total Code Data Types",
            DOCUMENT_TYPE_CONTAINER = "Total Code Container",
            DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
            META_TAB = "(Advanced) Meta Tags";

        private readonly IDataTypeService dataTypeService;
        private readonly IContentTypeService contentTypeService;
        private readonly ILogger logger;

        public _31_MetaTags(IContentTypeService contentTypeService, IDataTypeService dataTypeService, ILogger logger)
        {
            this.dataTypeService = dataTypeService;
            this.contentTypeService = contentTypeService;
            this.logger = logger;
        }

        private void UpdateDocumentType()
        {
            const string
                propertyAlias = "meta",
                propertyName = "Meta Tag";

            try
            {
                #region Home Document Type
                var container = contentTypeService.GetContainers(DOCUMENT_TYPE_CONTAINER, 1).FirstOrDefault();
                int containerId = -1;

                if (container != null)
                    containerId = container.Id;

                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (contentType != null)
                {
                    var changed = false;
                    #region Meta Tags Property Type
                    if (!contentType.PropertyTypeExists($"{propertyAlias}Author"))
                    {
                        PropertyType metaAuthorPropType = new PropertyType(dataTypeService.GetDataType(-88), $"{propertyAlias}Author")
                        {
                            Name = $"Author {propertyName}",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(metaAuthorPropType, META_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{propertyAlias}Copyright"))
                    {
                        PropertyType metaCopyrightPropType = new PropertyType(dataTypeService.GetDataType(-88), $"{propertyAlias}Copyright")
                        {
                            Name = $"Copyright {propertyName}",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(metaCopyrightPropType, META_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{propertyAlias}Description"))
                    {
                        PropertyType metaDescriptionPropType = new PropertyType(dataTypeService.GetDataType(-88), $"{propertyAlias}Description")
                        {
                            Name = $"Description {propertyName}",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(metaDescriptionPropType, META_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{propertyAlias}Keywords"))
                    {
                        PropertyType metaKeywordsPropType = new PropertyType(dataTypeService.GetDataType(-88), $"{propertyAlias}Keywords")
                        {
                            Name = $"Keywords {propertyName}",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(metaKeywordsPropType, META_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{propertyAlias}Robots"))
                    {
                        PropertyType metaRobotsPropType = new PropertyType(dataTypeService.GetDataType(-88), $"{propertyAlias}Robots")
                        {
                            Name = $"Robots {propertyName}",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(metaRobotsPropType, META_TAB);
                        changed = true;
                    }

                    #endregion

                    if (changed)
                        contentTypeService.Save(contentType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                }
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_31_MetaTags), ex.Message);
                logger.Error(typeof(_31_MetaTags), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            UpdateDocumentType();
        }

        public void Terminate() { }
    }
}
