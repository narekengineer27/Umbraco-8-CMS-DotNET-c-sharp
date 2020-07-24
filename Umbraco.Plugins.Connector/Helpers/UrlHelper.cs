namespace Umbraco.Plugins.Connector.Helpers
{
    using System;
    using System.Linq;
    using Umbraco.Web;
    public class UrlHelper
    {
        private readonly UmbracoHelper helper;
        private const int seed = 30;

        public UrlHelper(UmbracoHelper helper)
        {
            this.helper = helper;
        }
        public string GetConfirmVerificationUrl(string tenantUid, string origin, string username, string languageCode, string id = "", string code = "")
        {
            var random = ContentHelper.RandomString(seed);
            code = !string.IsNullOrEmpty(code) ? code : random;
            var tenantRootPage = new NodeHelper().GetTenantRoot(tenantUid);
            var confirmEmailPage = helper.Content(tenantRootPage.Id).Children.SingleOrDefault(x => x.ContentType.Alias == "ConfirmEmail");
            var query = "?";
            query += !string.IsNullOrEmpty(id) ? $"a={id}&" : string.Empty;
            query += $"b={code}&c={username}";
            var confirmEmailPageUrl = confirmEmailPage != null ? confirmEmailPage.UrlAbsolute() : "confirm-email";
            var verificationUrl = new Uri(new Uri(confirmEmailPageUrl), $"{query}");
            return verificationUrl.ToString();
        }

        public string GetResetPasswordVerificationUrl(string tenantUid, string requestUrl, string username, string languageCode, string code = "")
        {
            var random = ContentHelper.RandomString(seed);
            code = !string.IsNullOrEmpty(code) ? code : random;
            var tenantRootPage = new NodeHelper().GetTenantRoot(tenantUid);
            var page = helper.Content(tenantRootPage.Id).Children.SingleOrDefault(x => x.ContentType.Alias == "resetPasswordViaEmail");
            var pageUrl = page != null ? page.GetUrl(languageCode) : "reset-password";
            try
            {
                var x = new Uri(pageUrl);
                var url = new Uri(requestUrl + x.PathAndQuery) + $"?b={code}&c={username}";
                var verificationUrl = url.ToString();
                return verificationUrl;
            }
            catch
            {
                var verificationUrl = new Uri(new Uri(requestUrl), $"{pageUrl}?b={code}&c={username}").ToString();
                return verificationUrl;
            }
            //var verificationUrl = new Uri(new Uri(requestUrl), $"{pageUrl}?b={code}&c={username}");
            //return pageUrl.ToString();
        }
    }
}
