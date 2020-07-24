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
    public class _35_FooterLinkGroupChangesDocumentType : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly IFileService fileService;
        private readonly ILogger logger;
        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;

        public static string
                        DATA_TYPE_CONTAINER = "Total Code Data Types",
                        DOCUMENT_TYPE_ALIAS = "totalCodeFooterLinkGroup",
                        NESTED_FOOTERLINKSSETTING_DOCUMENT_TYPE_NAME = "Total Code Footer Link Settings",
                        NESTED_FOOTERLINKSSETTING_DOCUMENT_TYPE_ALIAS = "totalCodeFooterLinkSettings",
                        DOCUMENT_TYPE_CONTAINER = "Nested Contents",
                        NESTED_DOCUMENT_TYPE_ICON = "icon-document",
                        NESTED_TAB_NAME = "Content";


        public _35_FooterLinkGroupChangesDocumentType(ILocalizationService localizationService, IDomainService domainService, IFileService fileService, ILogger logger, IContentTypeService contentTypeService, IDataTypeService dataTypeService)
        {
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            this.contentTypeService = contentTypeService;
            this.fileService = fileService;
            this.dataTypeService = dataTypeService;
        }

        private void Reconfigure()
        {
            try
            {
                var oldControlAlias = "links";
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);

                if (contentType == null)
                    return;

                if (!contentType.PropertyTypeExists(oldControlAlias))
                    return;
                

                var MNTPpropertyName = "Total Code MNTP Menu - Single Site";
                var NestedContentPropertyName = "Nested Content Footer Link Group";

                var dataTypeContainer = dataTypeService.GetContainers(DATA_TYPE_CONTAINER, 1).FirstOrDefault();
                var dataTypeContainerId = -1;

                if (dataTypeContainer != null) dataTypeContainerId = dataTypeContainer.Id;

                var exists = dataTypeService.GetDataType(MNTPpropertyName) != null;
                if (!exists)
                {
                    var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.MultiNodeTreePicker", out IDataEditor editor);
                    if (editor != null)
                    {
                        DataType MNTPMenu_SingleSite = new DataType(editor, dataTypeContainerId)
                        {
                            Name = MNTPpropertyName,
                            ParentId = dataTypeContainerId,
                            Configuration = new MultiNodePickerConfiguration()
                            {
                                MaxNumber = 1,
                                MinNumber = 1,
                                ShowOpen = false,
                                TreeSource = new MultiNodePickerConfigurationTreeSource()
                                {
                                    StartNodeQuery = "$site",
                                    ObjectType = "content"
                                },
                            }
                        };
                        dataTypeService.Save(MNTPMenu_SingleSite);
                    }
                }


                var container = contentTypeService.GetContainers(DOCUMENT_TYPE_CONTAINER, 1).FirstOrDefault();
                int containerId = -1;

                if (container != null)
                    containerId = container.Id;

                contentType = contentTypeService.Get(NESTED_FOOTERLINKSSETTING_DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {

                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = NESTED_FOOTERLINKSSETTING_DOCUMENT_TYPE_NAME,
                        Alias = NESTED_FOOTERLINKSSETTING_DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = "",
                        Icon = NESTED_DOCUMENT_TYPE_ICON,
                        IsElement = true,
                        SortOrder = 0,
                        ParentId = 1087,
                        Variations = ContentVariation.Culture
                    };

                    docType.AddPropertyGroup(NESTED_TAB_NAME);

                    #region Nested Document Type Properties

                    var FooterContent_MNTPpropertyName = dataTypeService.GetDataType(MNTPpropertyName);
                    PropertyType internalLinkPropType = new PropertyType(FooterContent_MNTPpropertyName, "internalLink")
                    {
                        Name = "Internal Link",
                        Description = "",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(internalLinkPropType, NESTED_TAB_NAME);

                    PropertyType ExternalLinkTextPropType = new PropertyType(dataTypeService.GetDataType(-88), "externalLinkText")
                    {
                        Name = "External Link Text",
                        Description = "",
                        Variations = ContentVariation.Culture,
                    };
                    docType.AddPropertyType(ExternalLinkTextPropType, NESTED_TAB_NAME);

                    PropertyType LinkUrlPropType = new PropertyType(dataTypeService.GetDataType(-88), "externalLinkUrl")
                    {
                        Name = "External Link URL",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        ValidationRegExp = "https?://[a-zA-Z0-9-.]+.[a-zA-Z]{2,}"
                    };
                    docType.AddPropertyType(LinkUrlPropType, NESTED_TAB_NAME);


                    #endregion

                    contentTypeService.Save(docType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{NESTED_FOOTERLINKSSETTING_DOCUMENT_TYPE_ALIAS}' has been created");
                }


                exists = dataTypeService.GetDataType(NestedContentPropertyName) != null;
                if (!exists)
                {
                    var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.NestedContent", out IDataEditor editor);
                    if (editor != null)
                    {
                        DataType bannerSliderNestedDataType = new DataType(editor, dataTypeContainerId)
                        {
                            Name = NestedContentPropertyName,
                            ParentId = dataTypeContainerId,
                            Configuration = new NestedContentConfiguration
                            {
                                MinItems = 0,
                                MaxItems = 999,
                                ConfirmDeletes = true,
                                HideLabel = false,
                                ShowIcons = true,
                                ContentTypes = new[]
                                {
                                    new NestedContentConfiguration.ContentType { Alias = NESTED_FOOTERLINKSSETTING_DOCUMENT_TYPE_ALIAS, TabAlias = NESTED_TAB_NAME }
                                }
                            }
                        };
                        dataTypeService.Save(bannerSliderNestedDataType);
                    }
                }
                contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                contentType.RemovePropertyType(oldControlAlias);


                var FooterContent_NestedContentPropertyName = dataTypeService.GetDataType(NestedContentPropertyName);
                PropertyType linksPropType = new PropertyType(FooterContent_NestedContentPropertyName, "footerLinks")
                {
                    Name = "Links",
                    Description = "",
                    Variations = ContentVariation.Culture
                };
                contentType.AddPropertyType(linksPropType, NESTED_TAB_NAME);

                contentTypeService.Save(contentType);
                ConnectorContext.AuditService.Add(AuditType.New, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been created");

            }
            catch (Exception ex)
            {
                logger.Error(typeof(_35_FooterLinkGroupChangesDocumentType), ex.Message);
                logger.Error(typeof(_35_FooterLinkGroupChangesDocumentType), ex.StackTrace);
            }
        }
        public void Initialize()
        {
            Reconfigure();
        }


        public void Terminate()
        {
        }
    }
}