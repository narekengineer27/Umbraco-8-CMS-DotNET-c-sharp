using TotalCode.Core.Models.Pages;
using TotalCode.Core.Pages.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Plugins.Connector.Models;

namespace TotalCode.Core.Controllers.Pages
{
    public class totalCodeLoginPageController : BasePageController
    {
        public ActionResult Index()
        {
            var model = GetModel<LoginPageViewModel>(CurrentPage);
            model.LoginPageTitle = CurrentPage.Value<string>("loginPageTitle");

            if (LoginSession.LoggedIn || model.IsLoggedIn)
            {
                return Redirect(model.RootUrl);
            }
            return View(model);
        }
    }
}