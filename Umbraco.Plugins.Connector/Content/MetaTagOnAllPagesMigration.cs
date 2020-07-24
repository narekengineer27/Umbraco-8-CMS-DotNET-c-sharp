using System.Linq;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Web.PropertyEditors;

namespace Umbraco.Plugins.Connector.Content
{
    public class _44_MetaTagOnAllPagesMigration : IComponent
    {
        public static string
                META_TAB = "(Advanced) Meta Tags",
                DOCUMENT_PAGE_BASE = "totalCodeBasePage",
                DOCUMENT_TYPE_WHITELIST = "totalCodeComment,ConfirmEmail",
                propertyAlias = "meta",
                propertyName = "Meta Tag";


        private readonly IDataTypeService _dataTypeService;
        private readonly IContentTypeService _contentTypeService;
        private readonly ILogger _logger;

        public _44_MetaTagOnAllPagesMigration(IContentTypeService contentTypeService, IDataTypeService dataTypeService, ILogger logger)
        {
            _logger = logger;
            _dataTypeService = dataTypeService;
            _contentTypeService = contentTypeService;
        }

        public void Initialize()
        {
            try
            {
                var basePage = _contentTypeService.Get(DOCUMENT_PAGE_BASE);
                if (basePage != null)
                {
                    var pages = _contentTypeService.GetAll().Where(p => p.ParentId == basePage.Id);
                    if(pages != null && pages.Any())
                    {
                        foreach(var page in pages)
                        {
                            var contentType = _contentTypeService.Get(page.Id);
                            if (contentType != null)
                            {
                                var changed = false;

                                if (!contentType.PropertyTypeExists($"{propertyAlias}Description"))
                                {
                                    PropertyType metaDescriptionPropType = new PropertyType(_dataTypeService.GetDataType(-88), $"{propertyAlias}Description")
                                    {
                                        Name = $"Description {propertyName}",
                                        Variations = ContentVariation.Culture
                                    };
                                    contentType.AddPropertyType(metaDescriptionPropType, META_TAB);
                                    changed = true;
                                }

                                if (changed)
                                {
                                    _contentTypeService.Save(contentType);
                                    ConnectorContext.AuditService.Add(AuditType.New, -1, page.Id, "Document Type", $"Document Type '{contentType.Alias}' has been updated");
                                }
                            }
                        }
                    }

                    foreach (var str in DOCUMENT_TYPE_WHITELIST.Split(','))
                    {
                        var page = _contentTypeService.Get(str);
                        if (!page.PropertyTypeExists($"{propertyAlias}Description"))
                        {
                            PropertyType metaDescriptionPropType = new PropertyType(_dataTypeService.GetDataType(-88), $"{propertyAlias}Description")
                            {
                                Name = $"Description {propertyName}",
                                Variations = ContentVariation.Culture
                            };
                            page.AddPropertyType(metaDescriptionPropType, META_TAB);
                            _contentTypeService.Save(page);
                            ConnectorContext.AuditService.Add(AuditType.New, -1, page.Id, "Document Type", $"Document Type '{page.Alias}' has been updated");
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.Error(typeof(_44_MetaTagOnAllPagesMigration), ex.Message);
                _logger.Error(typeof(_44_MetaTagOnAllPagesMigration), ex.StackTrace);
            }
        }

        public void Terminate() { }
    }
}
