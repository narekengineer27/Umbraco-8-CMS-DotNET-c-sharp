using System.Net.Http.Formatting;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Core;
using System;


namespace Umbraco.Plugins.Connector
{
    [PluginController("StylesheetBulkDownloadSurface")]
    [Tree(Constants.Applications.Settings, "StylesheetBulkDownload", TreeGroup = Constants.Trees.Groups.Templating, TreeTitle = "Stylesheet Theme Folder Download", SortOrder = 1)]
    public class StylesheetBulkDownloadTreeController : TreeController
    {
        protected override TreeNode CreateRootNode(FormDataCollection queryStrings)
        {
            var root = base.CreateRootNode(queryStrings);
            root.Icon = "icon icon-script-alt";
            root.MenuUrl = null;
            root.HasChildren = false;
            root.RoutePath = "/settings/StylesheetBulkDownload/dashboard";
            return root;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            return nodes;
        }
    }
}