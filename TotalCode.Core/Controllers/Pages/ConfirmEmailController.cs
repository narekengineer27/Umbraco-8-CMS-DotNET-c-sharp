namespace Umbraco.Plugins.Connector.Controllers
{
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using TotalCode.Core.Models;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Core.Services;
    using Umbraco.Core.Logging;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web;
    using Umbraco.Web.Models;

    public class ConfirmEmailController : BasePageController
    {
        private readonly TotalCodeApiService verificationService;
        private readonly ILogger logger;
        private readonly Helpers.UrlHelper helper;
        public ConfirmEmailController(ILogger logger)
        {
            this.logger = logger;
            verificationService = new TotalCodeApiService();
            helper = new Helpers.UrlHelper(Umbraco);
            using (var scope = ConnectorContext.ScopeProvider.CreateScope(autoComplete: true))
            {
                ApiKeyCache.UpdateCache(scope.Database);
            }
        }
        public async Task<ActionResult> TotalCodeTenantConfirmEmail(ContentModel model)
        {
            var confirmModel = GetModel<ConfirmEmailModel>(CurrentPage);

            confirmModel.BackUrl = LoginSession.LoggedIn ? model.Content.Parent.Children.FirstOrDefault(x => x.ContentType.Alias.Equals("totalCodeAccountPage")).Url : confirmModel.Root.Url;

            if (Request.QueryString.Count == 0)
            {
                return Redirect("/");
            }
            //string domain = System.Net.WebUtility.UrlDecode(Request.Url.ToString());
            string id = Request.QueryString["a"], code = Request.QueryString["b"].Replace(" ", "+"), email = Request.QueryString["c"];
            var tenantUid = model.Content.Parent.GetProperty("tenantUid").GetValue().ToString();
            var origin = TenantHelper.GetCurrentTenantUrl(model);
            string domain = helper.GetConfirmVerificationUrl(tenantUid, origin, email, Umbraco.CultureDictionary.Culture.TwoLetterISOLanguageName, id, code);
            var confirmationData = new ConfirmationData
            {
                ClientId = id,
                Code = code,
                Email = email,
                VerificationUrl = new Uri(domain).ToString()
            };

            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = (ConfirmEmailResponseContent)await verificationService.ConfirmEmailAsync(tenantUid, origin, confirmationData.Email, confirmationData.Code, confirmationData.VerificationUrl, authorization.AccessToken);


            confirmationData.IsConfirmed = response.Success;

            if (!response.Success)
            {
                // TODO: remove logging
                logger.Info<ConfirmEmailController>($"Verification Url: {confirmationData.VerificationUrl}");
                logger.Info<ConfirmEmailController>(JsonConvert.SerializeObject(response));
            }

            ViewData["ConfirmationData"] = confirmationData;

            return Index(confirmModel);
        }

        #region Dependant Class
        public class ConfirmationData
        {
            public string ClientId { get; set; }
            public string Code { get; set; }
            public string Email { get; set; }
            public string VerificationUrl { get; set; }
            public bool IsConfirmed { get; set; }
        }
        #endregion
    }
}
