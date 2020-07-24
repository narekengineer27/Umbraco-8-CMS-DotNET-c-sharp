using System.Linq;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Web.PropertyEditors;

namespace Umbraco.Plugins.Connector.Content
{
    public class _45_TitleOnAllPagesMigration : IComponent
    {
        public static string
                TAB = "Content",
                DOCUMENT_PAGE_BASE = "totalCodeBasePage",
                DOCUMENT_TYPE_WHITELIST = "totalCodeComment,ConfirmEmail",
                DOCUMENT_TYPE_BLOCKLIST = "totalCodeHomePage",
                propertyAlias = "pageTitle",
                propertyName = "Title";


        private readonly IDataTypeService _dataTypeService;
        private readonly IContentTypeService _contentTypeService;
        private readonly ILogger _logger;

        public _45_TitleOnAllPagesMigration(IContentTypeService contentTypeService, IDataTypeService dataTypeService, ILogger logger)
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
                    var pages = _contentTypeService.GetAll().Where(p => p.ParentId == basePage.Id && !DOCUMENT_TYPE_BLOCKLIST.Split(',').Any(x => x == p.Alias));
                    if (pages != null && pages.Any())
                    {
                        foreach (var page in pages)
                        {
                            var contentType = _contentTypeService.Get(page.Id);
                            if (contentType != null)
                            {
                                var changed = false;

                                if (!contentType.PropertyTypeExists(propertyAlias))
                                {
                                    PropertyType metaDescriptionPropType = new PropertyType(_dataTypeService.GetDataType(-88), propertyAlias)
                                    {
                                        Name = propertyName,
                                        Variations = ContentVariation.Culture
                                    };
                                    contentType.AddPropertyType(metaDescriptionPropType, TAB);
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

                    foreach (var str in DOCUMENT_TYPE_WHITELIST.Split(',').Where(p => !DOCUMENT_TYPE_BLOCKLIST.Split(',').Any(x => x == p)))
                    {
                        var page = _contentTypeService.Get(str);
                        if (!page.PropertyTypeExists($"{propertyAlias}Description"))
                        {
                            PropertyType metaDescriptionPropType = new PropertyType(_dataTypeService.GetDataType(-88), propertyAlias)
                            {
                                Name = propertyName,
                                Variations = ContentVariation.Culture
                            };
                            page.AddPropertyType(metaDescriptionPropType, TAB);
                            _contentTypeService.Save(page);
                            ConnectorContext.AuditService.Add(AuditType.New, -1, page.Id, "Document Type", $"Document Type '{page.Alias}' has been updated");
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.Error(typeof(_45_TitleOnAllPagesMigration), ex.Message);
                _logger.Error(typeof(_45_TitleOnAllPagesMigration), ex.StackTrace);
            }
        }

        public void Terminate() { }
    }
}
