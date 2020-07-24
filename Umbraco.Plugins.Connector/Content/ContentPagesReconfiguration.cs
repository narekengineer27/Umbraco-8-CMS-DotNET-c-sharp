namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Services;
    using Umbraco.Core.Composing;
    using System.Collections.Generic;
    using Umbraco.Web.PropertyEditors;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Plugins.Connector.Dictionaries;
    using Newtonsoft.Json.Linq;

    public class _20_ContentPagesReconfiguration : IComponent
    {
        public static string
            DATA_TYPE_CONTAINER = "Total Code Data Types",
            CONTAINER = "Total Code Container",
            CATEGORIES_DOCUMENT_TYPE_ALIAS = "totalCodeCategoriesPage",
            CATEGORY_DOCUMENT_TYPE_ALIAS = "totalCodeCategoryPage",
            TAB = "Content";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly ILogger logger;



        public _20_ContentPagesReconfiguration(IContentTypeService contentTypeService, IDataTypeService dataTypeService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.logger = logger;
        }

        private void CreateDocumentType()
        {
            try
            {
                var richTextContainer = dataTypeService.GetContainers(DATA_TYPE_CONTAINER, 1).FirstOrDefault();
                var richTextContainerId = -1;

                if (richTextContainer != null) richTextContainerId = richTextContainer.Id;

                var dataTypeExists = dataTypeService.GetDataType("Full Rich Text Editor") != null;
                if (!dataTypeExists)
                {
                    var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.TinyMCE", out IDataEditor editor);
                    if (created)
                    {
                        DataType richTextEditor = new DataType(editor, richTextContainerId)
                        {
                            Name = "Full Rich Text Editor",
                            ParentId = richTextContainerId,
                            Configuration = new RichTextConfiguration
                            {
                                Editor = JObject.Parse("{\"toolbar\":[\"ace\",\"removeformat\",\"undo\",\"redo\",\"cut\",\"copy\",\"paste\",\"styleselect\",\"bold\",\"italic\",\"underline\",\"strikethrough\",\"alignleft\",\"aligncenter\",\"alignright\",\"alignjustify\",\"bullist\",\"numlist\",\"outdent\",\"indent\",\"link\",\"unlink\",\"anchor\",\"umbmediapicker\",\"umbmacro\",\"table\",\"umbembeddialog\",\"hr\",\"subscript\",\"superscript\",\"charmap\",\"rtl\",\"ltr\"],\"stylesheets\":[],\"maxImageSize\":500,\"mode\":\"classic\"}"),
                                HideLabel = true
                            }
                        };
                        dataTypeService.Save(richTextEditor);

                        var container = contentTypeService.GetContainers(CONTAINER, 1).FirstOrDefault();
                        var containerId = container.Id;

                        string propertyName = "Page Content",
                        propertyDescription = "Text to be displayed on the Page";

                        // categories page
                        var contentCategoriesType = contentTypeService.Get(CATEGORIES_DOCUMENT_TYPE_ALIAS);
                        if (contentCategoriesType != null)
                        {
                            PropertyType richTextPropType = new PropertyType(dataTypeService.GetDataType(richTextEditor.Id), "pageContent")
                            {
                                Name = propertyName,
                                Description = propertyDescription,
                                Variations = ContentVariation.Culture
                            };
                            contentCategoriesType.AddPropertyType(richTextPropType, TAB);
                            contentTypeService.Save(contentCategoriesType);
                            ConnectorContext.AuditService.Add(AuditType.Save, -1, contentCategoriesType.Id, "Document Type", $"Document Type '{CATEGORIES_DOCUMENT_TYPE_ALIAS}' has been updated");
                        }

                        // category page
                        var contentCategoryType = contentTypeService.Get(CATEGORY_DOCUMENT_TYPE_ALIAS);
                        if (contentCategoryType != null)
                        {
                            PropertyType richTextPropType = new PropertyType(dataTypeService.GetDataType(richTextEditor.Id), "pageContent")
                            {
                                Name = propertyName,
                                Description = propertyDescription,
                                Variations = ContentVariation.Culture
                            };
                            contentCategoryType.AddPropertyType(richTextPropType, TAB);
                            contentTypeService.Save(contentCategoryType);
                            ConnectorContext.AuditService.Add(AuditType.Save, -1, contentCategoryType.Id, "Document Type", $"Document Type '{CATEGORY_DOCUMENT_TYPE_ALIAS}' has been updated");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(typeof(_20_ContentPagesReconfiguration), ex.Message);
                logger.Error(typeof(_20_ContentPagesReconfiguration), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            CreateDocumentType();
        }

        public void Terminate() { }
    }
}
