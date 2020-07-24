using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Core.Composing;
using Umbraco.Core.Dashboards;
using Umbraco.Web;
using Umbraco.Web.Dashboards;
using Umbraco.Web.Editors;
using Umbraco.Web.Trees;
using System.Threading.Tasks;
using Umbraco.Plugins.Connector.Interfaces;
using Umbraco.Plugins.Connector.Models;

namespace Umbraco.Plugins.Connector.Services
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class RemoveDashboard : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Dashboards().Remove<RedirectUrlDashboard>();
        }
    }

    [Weight(20)]
    public class MyRedirectUrlDashboard : IDashboard
    {
        public string Alias => "contentRedirectManager";

        public string[] Sections => new[] { "content" };

        public string View => "views/dashboard/content/redirecturls.html";

        public IAccessRule[] AccessRules
        {
            get
            {
                var rules = new IAccessRule[]
                {
                    new AccessRule {Type = AccessRuleType.Deny, Value = "managers"}
                };
                return rules;
            }
        }
    }

    public class SubscribeToEditorModelEventsComposer : ComponentComposer<SubscribeToEditorModelEvents>
    {
        //this automatically adds the component to the Components collection of the Umbraco composition
    }

    public class SubscribeToEditorModelEvents : IComponent
    {
        // initialize: runs once when Umbraco starts
        public void Initialize()
        {
            EditorModelEventManager.SendingContentModel += EditorModelEventManager_SendingContentModel;
            TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;
        }

        // terminate: runs once when Umbraco stops
        public void Terminate()
        {

        }

        private void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            if (sender.TreeAlias == "content" && sender.UmbracoContext.Security.CurrentUser.Groups.Any(x => x.Name.InvariantEquals("Managers")))
            {
                var junk = new List<Umbraco.Web.Models.Trees.TreeNode>();
                foreach (var branch in e.Nodes)
                {
                    var helper = Umbraco.Web.Composing.Current.UmbracoHelper;
                    var node = helper.Content(branch.Id);

                    if (node != null)
                    {
                        if (node.ContentType.Alias != "totalCodeHomePage" && 
                            node.ContentType.Alias != "totalCodeCategoriesPage" &&
                            node.ContentType.Alias != "totalCodeCategoryPage" && 
                            node.ContentType.Alias != "totalCodeGenericPage" && 
                            node.ContentType.Alias != "totalCodeArticlePage" &&
                            node.ContentType.Alias != "totalCodeArticlePage" &&
                            node.ContentType.Alias != "totalCodeTermsPage" &&
                            node.ContentType.Alias != "totalCodeAffiliatePage")
                        {
                            junk.Add(branch);
                        }
                        if (node.HasProperty("gameType"))
                        {
                            if (node.GetProperty("gameType").Value().ToString() != "Casino")
                            {
                                junk.Add(branch);
                            }
                        }
                    }
                }
                foreach (var j in junk)
                {
                    e.Nodes.Remove(j);
                }
            }
        }

        private void EditorModelEventManager_SendingContentModel(System.Web.Http.Filters.HttpActionExecutedContext sender, EditorModelEventArgs<Umbraco.Web.Models.ContentEditing.ContentItemDisplay> e)
        {
            if (e.UmbracoContext.Security.CurrentUser.Groups.Any(x => x.Name.InvariantEquals("Managers")))
            {
                if (e.Model.ContentTypeAlias == "totalCodeHomePage")
                {
                    foreach (var variant in e.Model.Variants)
                    {
                        var tabs_new = variant.Tabs.Take(0).ToList();
                        foreach (var tab in variant.Tabs)
                        {
                            if (tab.Alias == "Content" || tab.Alias == "Footer" || tab.Alias == "Sliders" || tab.Alias == "Account Menu")
                            {
                                if (tab.Alias == "Content")
                                {
                                    var properties_new = tab.Properties.Take(0).ToList();
                                    foreach (var property in tab.Properties)
                                    {
                                        if (property.Alias == "logo")
                                        {
                                            properties_new.Add(property);
                                        }
                                        if (property.Alias == "externalUrlsTopMenu" || property.Alias == "externalUrlsMainMenu" || property.Alias == "externalUrlsFooter" || property.Alias == "externalUrlsAccountMenu" || property.Alias == "theme")
                                        {
                                            var property_readonly = property;
                                            property_readonly.View = "readonlyvalue";
                                            properties_new.Add(property_readonly);
                                        }
                                    }
                                    tab.Properties = properties_new;
                                }
                                if (tab.Alias == "Account Menu")
                                {
                                    var properties_new = tab.Properties.Take(0).ToList();
                                    foreach (var property in tab.Properties)
                                    {
                                        if (property.Alias == "extras")
                                        {
                                            properties_new.Add(property);
                                        }
                                    }
                                    tab.Properties = properties_new;
                                }
                                tabs_new.Add(tab);
                            }
                        }
                        variant.Tabs = tabs_new;
                    }

                }

                if (e.Model.ContentTypeAlias == "totalCodeGenericPage")
                {
                    foreach (var variant in e.Model.Variants)
                    {
                        var tabs_new = variant.Tabs.Take(0).ToList();
                        foreach (var tab in variant.Tabs)
                        {
                            if (tab.Alias == "Sliders")
                            {
                                tabs_new.Add(tab);
                            }
                        }
                        variant.Tabs = tabs_new;
                    }

                }
            }

        }

    }
}
