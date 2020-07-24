using TotalCode.Core.Models.Pages;
using TotalCode.Core.Pages.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web;

namespace TotalCode.Core.Controllers.Pages
{
    public class TotalCodeTermsPageController : BasePageController
    {
        public ActionResult Index()
        {
            var model = GetModel<TermsPageViewModel>(CurrentPage);
            model.Body = CurrentPage.Value("bodyText");
            return CurrentTemplate(model);
        }
    }
}
