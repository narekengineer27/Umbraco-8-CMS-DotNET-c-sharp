namespace TotalCode.Core.Controllers.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web;

    public class TotalCodeGenericInfoPageController : BasePageController
    {
        public ActionResult Index(IPublishedContent content)
        {
            var model = new GenericInfoPageViewModel(content)
            {
                GenericInfoPageContent = CurrentPage.Value<string>("genericInfoPageContent"),
                TwoLetterISOLanguageName = Umbraco.CultureDictionary.Culture.TwoLetterISOLanguageName,
                GenericInfoPageTitle = CurrentPage.Value<string>("genericInfoPageTitle")
            };
            return CurrentTemplate(model);
        }
    }
}
