namespace TotalCode.Core.Controllers.Pages
{
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Plugins.Connector;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    public class TotalCodeAccountPageController : BasePageController
    {
        //public ActionResult Index()
        //{
        //    var model = GetModel<AccountPageViewModel>(CurrentPage);
        //    return CurrentTemplate(model);
        //}
        private readonly TotalCodeApiService apiService;
        public TotalCodeAccountPageController()
        {
            apiService = new TotalCodeApiService();
        }

        public async Task<ActionResult> TotalCodeAccountPageTemplate()
        {
            var accountPage = GetModel<AccountPageViewModel>(CurrentPage);
            if (!LoginSession.LoggedIn || !accountPage.IsLoggedIn)
            {
                return Redirect("/");
            }
            string domain = Request.Url.ToString();
#if DEBUG
            domain = $"https://{accountPage.Subdomain}.{accountPage.Domain}";
#endif
            var origin = domain;
            var decodedJwt = LoginSession.DecodedJwtToken;
            var response = (GetCustomerInfoResponseContent)await apiService.GetCustomerInfoAsync(accountPage.TenantUid, origin, LoginSession.Username, LoginSession.Token);
            if (response.Success)
            {
                accountPage.Customer = response.Payload;
                var walletResponse = (GetCustomerAccountBalanceResponseContent)await apiService.GetAccountBalanceAsync(accountPage.TenantUid, origin, response.Payload.CustomerGuid.Value.ToString(), LoginSession.Token);
                if (walletResponse.Success)
                {
                    accountPage.CustomerWallet = new CustomerWallet
                    {
                        TotalAccountBalance = DefaultAllowedValues.DecimalAccuracy(walletResponse.Payload.TotalAccountBalance, decodedJwt.CurrencyCode),
                        BonusBalance = DefaultAllowedValues.DecimalAccuracy(walletResponse.Payload.BonusBalance, decodedJwt.CurrencyCode),
                        WithdrawableBalance = DefaultAllowedValues.DecimalAccuracy(walletResponse.Payload.WithdrawableBalance, decodedJwt.CurrencyCode),
                        CustomerGuid = walletResponse.Payload.CustomerGuid
                    };
                }
                else
                {
                    accountPage.CustomerWallet = new CustomerWallet
                    {
                        BonusBalance = DefaultAllowedValues.DecimalAccuracy(0.00m, decodedJwt.CurrencyCode),
                        TotalAccountBalance = DefaultAllowedValues.DecimalAccuracy(0.00m, decodedJwt.CurrencyCode),
                        WithdrawableBalance = DefaultAllowedValues.DecimalAccuracy(0.00m, decodedJwt.CurrencyCode),
                        CustomerGuid = response.Payload.CustomerGuid
                    };
                }
            }
            else
            {
                accountPage.Customer = new PayloadContent();
                accountPage.CustomerWallet = new CustomerWallet();
                ConnectorContext.Logger.Debug(typeof(TotalCodeAccountPageController), JsonConvert.SerializeObject(response));
            }
            //else
            //{
            //    // probably token has expired, redirect to home to login again
            //    LoginSession.Logout();
            //    return Redirect("/");
            //}

            HttpCookie cookie = new HttpCookie("IsAuthenticatedPage");
            cookie.Value = "1";
            cookie.Expires = DateTime.UtcNow.AddDays(999);
            HttpContext.Response.Cookies.Add(cookie);

            return CurrentTemplate(accountPage);
        }
    }
}
