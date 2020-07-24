namespace TotalCode.Core.Controllers.Pages
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using TotalCode.Core.Models;
    using Umbraco.Core.Services;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Pages.Controllers;
    public class TotalCodeHelpPageController : BasePageController
    {
        public ActionResult Index()
        {
            var model = GetModel<HelpPageViewModel>(CurrentPage);

            return CurrentTemplate(model);
        }
    }
}
