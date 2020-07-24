namespace Umbraco.Plugins.Connector
{
    using System.Net.Http.Formatting;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.Trees;
    using Umbraco.Core;
    using System;

    [PluginController("ApiSettingsSurface")]
    [Tree("settings", "ApiSettings", TreeGroup = "settingsGroup", TreeTitle = "Umbraco Configuration", SortOrder = 0)]
    public class TotalCodeApiSettingsTreeController : TreeController
    {
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            if (id == Constants.System.Root.ToInvariantString())
            {
                var nodes = new TreeNodeCollection();

                var node = CreateTreeNode(id: "1", parentId: "-1", queryStrings: queryStrings, title: "Settings", icon: "icon-settings-alt-2", hasChildren: false);
                nodes.Add(node);
                return nodes;
            }

            // this tree doesn't support rendering more than 1 level
            throw new NotSupportedException();
        }
    }
}