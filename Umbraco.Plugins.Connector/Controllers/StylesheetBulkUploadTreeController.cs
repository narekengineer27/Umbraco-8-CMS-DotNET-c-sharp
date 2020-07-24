using System.Net.Http.Formatting;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Core;
using System;


namespace Umbraco.Plugins.Connector
{
    [PluginController("StylesheetBulkUploadSurface")]
    [Tree(Constants.Applications.Settings, "StylesheetBulkUpload", TreeGroup = Constants.Trees.Groups.Templating, TreeTitle = "Stylesheet Theme Folder Upload", SortOrder = 0)]
    public class StylesheetBulkUploadTreeController : TreeController
    {
        protected override TreeNode CreateRootNode(FormDataCollection queryStrings)
        {
            var root = base.CreateRootNode(queryStrings);
            root.Icon = "icon icon-script-alt";
            root.MenuUrl = null;
            root.HasChildren = false;
            root.RoutePath = "/settings/StylesheetBulkUpload/dashboard";
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