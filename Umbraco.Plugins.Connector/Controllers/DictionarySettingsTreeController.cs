namespace Umbraco.Plugins.Connector
{
    using System.Net.Http.Formatting;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.Trees;
    using Umbraco.Core;
    using System;

    [PluginController("DictionarySettingsSurface")]
    [Tree("settings", "DictionarySettings", TreeGroup = "settingsGroup", TreeTitle = "Dictionary Manager", SortOrder = 1)]
    public class DictionarySettingsTreeController : TreeController
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
                var dictionaryNode = CreateTreeNode(id: "1", parentId: "-1", queryStrings: queryStrings, title: "Dictionary Import & Export", icon: "icon-books", hasChildren: false);
                nodes.Add(dictionaryNode);
                return nodes;
            }

            // this tree doesn't support rendering more than 1 level
            throw new NotSupportedException();
        }
    }
}