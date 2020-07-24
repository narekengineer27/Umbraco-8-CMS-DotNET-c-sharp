using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TotalCode.Core.Models.Pages;
using TotalCode.Core.Pages.Controllers;

namespace TotalCode.Core.Controllers.Pages
{
    public class TotalCodeSettingsPageController : BasePageController
    {
        public ActionResult Index()
        {
            var model = GetModel<SettingsPageViewModel>(CurrentPage);

            return CurrentTemplate(model);
        }
            
    }
}
