namespace Umbraco.Plugins.Connector.Controllers
{
    using System.Web.Mvc;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web;
    using Umbraco.Web.Models;

    public class ResetPasswordViaEmailController : BasePageController
    {
        private readonly TotalCodeApiService verificationService;
        private readonly Helpers.UrlHelper helper;
        public ResetPasswordViaEmailController()
        {
            verificationService = new TotalCodeApiService();
            this.helper = new Helpers.UrlHelper(Umbraco);
            using (var scope = ConnectorContext.ScopeProvider.CreateScope(autoComplete: true))
            {
                ApiKeyCache.UpdateCache(scope.Database);
            }
        }
        public ActionResult TotalCodeTenantResetPassword(ContentModel model)
        {
            var confirmModel = GetModel<ResetPasswordModel>(CurrentPage);

            if (Request.QueryString.Count == 0)
            {
                return Redirect("/");
            }
            //string domain = Request.Url.ToString();

            var code = Request.QueryString["b"].Replace(" ", "+");
            var username = Request.QueryString["c"];
            var origin = TenantHelper.GetCurrentTenantUrl(model);
            var tenantUid = model.Content.Parent.Value<string>("tenantUid");
            var domain = helper.GetResetPasswordVerificationUrl(tenantUid, origin, username, code);
            var confirmationData = new ConfirmationData
            {
                Code = code,
                Username = username,
                VerificationUrl = domain
            };

            ViewData["ConfirmationData"] = confirmationData;

            return Index(confirmModel);
        }

        #region Dependant Class
        public class ConfirmationData
        {
            public string Code { get; set; }
            public string Username { get; set; }
            public string VerificationUrl { get; set; }
        }
        #endregion
    }
}
