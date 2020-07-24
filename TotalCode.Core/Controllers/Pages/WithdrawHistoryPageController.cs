using TotalCode.Core.Models.Pages;
using TotalCode.Core.Pages.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TotalCode.Core.Controllers.Pages
{
    public class TotalCodeWithdrawHistoryPageController : BasePageController
    {
        public ActionResult Index()
        {
            var model = GetModel<WithdrawHistoryPageViewModel>(CurrentPage);

            if (string.IsNullOrEmpty(model.Token))
            {
                return Redirect("/?login=y");
            }

            return CurrentTemplate(model);
        }
    }
}
