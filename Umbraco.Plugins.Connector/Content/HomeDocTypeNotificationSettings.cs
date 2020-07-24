

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
    public class _39_HomeDocTypeNotificationSettings : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly IFileService fileService;
        private readonly ILogger logger;
        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;


        public static string
                        DATA_TYPE_CONTAINER = "Total Code Data Types",
                        DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
                        NOTIFICATION_SETTINGS_TAB = "Notification Settings";

        public _39_HomeDocTypeNotificationSettings(ILocalizationService localizationService, IDomainService domainService, IFileService fileService, ILogger logger, IContentTypeService contentTypeService, IDataTypeService dataTypeService)
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
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);

                if (contentType == null)
                    return;

                if (!contentType.PropertyGroups.Any(x => x.Name == NOTIFICATION_SETTINGS_TAB))
                {
                    contentType.AddPropertyGroup(NOTIFICATION_SETTINGS_TAB);
                }

                const string ddlNotificationPositions = "ddl Notification Positions";

                var dataTypeContainer = dataTypeService.GetContainers(DATA_TYPE_CONTAINER, 1).FirstOrDefault();
                var dataTypeContainerId = -1;

                if (dataTypeContainer != null) dataTypeContainerId = dataTypeContainer.Id;
                var exists = dataTypeService.GetDataType(ddlNotificationPositions) != null;
                if (!exists)
                {
                    var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.DropDown.Flexible", out IDataEditor editor);
                    if (editor != null)
                    {
                        var positionList = new List<ValueListConfiguration.ValueListItem>();
                        positionList.Add(new ValueListConfiguration.ValueListItem { Id = 1, Value = "Top Right" });
                        positionList.Add(new ValueListConfiguration.ValueListItem { Id = 2, Value = "Bottom Right" });
                        positionList.Add(new ValueListConfiguration.ValueListItem { Id = 3, Value = "Bottom Left" });
                        positionList.Add(new ValueListConfiguration.ValueListItem { Id = 4, Value = "Top Left" });
                        positionList.Add(new ValueListConfiguration.ValueListItem { Id = 5, Value = "Top Center" });
                        positionList.Add(new ValueListConfiguration.ValueListItem { Id = 6, Value = "Bottom Center" });

                        DataType ddlNotificationPositionsDataType = new DataType(editor, dataTypeContainerId)
                        {
                            Name = ddlNotificationPositions,
                            ParentId = dataTypeContainerId,
                            Configuration = new DropDownFlexibleConfiguration()
                            {
                                Multiple = false,
                                Items = positionList
                            }

                        };
                        dataTypeService.Save(ddlNotificationPositionsDataType);
                    }
                }
                var isNewPropertyAdded = false;
                if (!contentType.PropertyTypeExists("notificationPosition"))
                {
                    isNewPropertyAdded = true;
                    var ddlPositionsDataType = dataTypeService.GetDataType(ddlNotificationPositions);

                    PropertyType ddlPositionsPropType = new PropertyType(ddlPositionsDataType, "notificationPosition")
                    {
                        Name = "Position",
                        Variations = ContentVariation.Culture
                    };
                    contentType.AddPropertyType(ddlPositionsPropType, NOTIFICATION_SETTINGS_TAB);
                }

                if (!contentType.PropertyTypeExists("notificationBgColor"))
                {
                    isNewPropertyAdded = true;
                    PropertyType notificationBackgroundColor = new PropertyType(dataTypeService.GetDataType(6018), "notificationBgColor")
                    {
                        Name = "Background Color",
                        Variations = ContentVariation.Culture
                    };
                    contentType.AddPropertyType(notificationBackgroundColor, NOTIFICATION_SETTINGS_TAB);
                }

                if (!contentType.PropertyTypeExists("notificationWidth"))
                {
                    isNewPropertyAdded = true;
                    PropertyType widthPropType = new PropertyType(dataTypeService.GetDataType(-88), "notificationWidth")
                    {
                        Name = "Width",
                        Description="If required enter only numeric value",
                        Variations = ContentVariation.Culture
                    };
                    contentType.AddPropertyType(widthPropType, NOTIFICATION_SETTINGS_TAB);
                }

                if (isNewPropertyAdded)
                {
                    contentTypeService.Save(contentType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                }
            }

            catch (Exception ex)
            {
                logger.Error(typeof(_39_HomeDocTypeNotificationSettings), ex.Message);
                logger.Error(typeof(_39_HomeDocTypeNotificationSettings), ex.StackTrace);
            }

        }
    public void Terminate()
        {
        }
    }
}