namespace TotalCode.Core.Controllers.Pages
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    public class TotalCodeEditAccountPageController : BasePageController
    {

        private readonly TotalCodeApiService apiService;
        public TotalCodeEditAccountPageController()
        {
            apiService = new TotalCodeApiService();
        }

        public async Task<ActionResult> TotalCodeAccountEditPageTemplate()
        {
            var editAccountPage = GetModel<EditAccountPageViewModel>(CurrentPage);
            if (!LoginSession.LoggedIn || !editAccountPage.IsLoggedIn)
            {
                return Redirect("/");
            }
            string domain = Request.Url.ToString();
#if DEBUG
            domain = $"https://{editAccountPage.Subdomain}.{editAccountPage.Domain}";
#endif
            var origin = domain;
            var response = (GetCustomerInfoResponseContent)await apiService.GetCustomerInfoAsync(editAccountPage.TenantUid, origin, LoginSession.Username, LoginSession.Token);
            if (response.Success)
            {
                editAccountPage.Customer = response.Payload;
            }
            else
            {
                // probably token has expired, redirect to home to login again
                LoginSession.Logout();
                return Redirect("/");
            }

            return CurrentTemplate(editAccountPage);
        }
    }
}
