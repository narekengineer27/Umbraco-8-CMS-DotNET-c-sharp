//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using System.Web.Security;
//using Umbraco.Core.Models.PublishedContent;
//using Umbraco.Web;
//using Umbraco.Web.Mvc;

//namespace TotalCode.Core.Controllers
//{
//    public class AccountController : SurfaceController
//    {

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Login()
//        {
//            Session["login"] = true;

//            var root = CurrentPage.Root();

//            if (root.HasValue("accountLandingPage"))
//            {
//                return RedirectToUmbracoPage(root.Value<IEnumerable<IPublishedContent>>("accountLandingPage").FirstOrDefault().Id);
//            }
//            else
//            {
//                return RedirectToUmbracoPage(root.Id);
//            }
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Logout()
//        {
//            Session["login"] = false;

//            var root = CurrentPage.Root();

//            return RedirectToUmbracoPage(root.Id);
//        }
//    }
//}
