namespace Umbraco.Plugins.Connector.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web.Mvc;
    public class RegistrationController : SurfaceController
    {
        private readonly IContentService contentService;
        private readonly TotalCodeApiService verificationService;
        private readonly Helpers.UrlHelper helper;

        public RegistrationController()
        {
            verificationService = new TotalCodeApiService();
            contentService = ConnectorContext.ContentService;
            helper = new Helpers.UrlHelper(Umbraco);
            using (var scope = ConnectorContext.ScopeProvider.CreateScope(autoComplete: true))
            {
                ApiKeyCache.UpdateCache(scope.Database);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Register(string tenantUid,
            string email,
            string username,
            string password,
            string mobile,
            string title = "",
            string firstName = "",
            string lastName = "",
            string gender = "",
            string day = "",
            string month = "",
            string year = "",
            string dob = "",
            string address1 = "",
            string address2 = "",
            string address3 = "",
            string town = "",
            string county = "",
            string postalcode = "",
            string currency = "",
            string odds = "",
            string language = "",
            string country = "",
            string timeZone = "",
            string bonusCode = "",
            string referrer = "",
            string notify = "",
            string notifyViaPlatform = "",
            string notifyViaEmail = "",
            string notifyViaSMS = "")
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);

            if (dob == "")
            {
                if (year != "" && month != "" && day != "")
                {
                    dob = year + "-" + month + "-" + day;
                }
            }
            if (Request.Cookies.AllKeys.Contains("referrer"))
            {
                if (!String.IsNullOrEmpty(Request.Cookies.Get("referrer").Value))
                {
                    referrer = Request.Cookies.Get("referrer").Value;
                }
            }

            CustomerRegisterResponseContent response = (CustomerRegisterResponseContent)await verificationService.CustomerRegisterAsync(
                tenantUid: tenantUid.ToString(),
                origin: origin,
                email: email,
                username: username,
                password: password,
                mobile: mobile.TrimStart(new Char[] { '0' }),
                title: title,
                firstName: firstName,
                lastName: lastName,
                gender: gender,
                dob: dob,
                address1: address1,
                address2: address2,
                address3: address3,
                town: town,
                county: county,
                postCode: postalcode,
                currency: currency,
                odds: odds,
                language: language,
                country: country,
                timeZone: timeZone,
                bonusCode: bonusCode,
                referrer: referrer,
                notify: notify == "on" ? "true" : "false",
                notifyViaPlatform: notifyViaPlatform == "on" ? "true" : "false",
                notifyViaEmail: notifyViaEmail == "on" ? "true" : "false",
                notifyViaSMS: notifyViaSMS == "on" ? "true" : "false",
                authorization: authorization.AccessToken);

            if (response.Success && !response.Payload.IsActive)
            {
                var verificationUrl = helper.GetConfirmVerificationUrl(tenantUid, origin, email, language, response.Payload.Id.ToString());
                var postRegisterResponse = await verificationService.VerificationEmailAsync(tenantUid.ToString(), origin, verificationUrl, email, language, authorization.AccessToken);
                postRegisterResponse.RelatedResponse = response;
                return Json(postRegisterResponse, JsonRequestBehavior.DenyGet);
            }
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangeEmail(string tenantUid, string id, string username, string email, string language)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = (ChangeEmailResponseContent)await verificationService.ChangeEmailAsync(tenantUid, origin, username, email, authorization.AccessToken);
            if (response.Success)
            {
                var verificationUrl = helper.GetConfirmVerificationUrl(tenantUid, origin, email, language, id);
                var postRegisterResponse = await verificationService.VerificationEmailAsync(tenantUid.ToString(), origin, verificationUrl, email, language, authorization.AccessToken);
                postRegisterResponse.RelatedResponse = response;
                return Json(postRegisterResponse, JsonRequestBehavior.DenyGet);
            }
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VerifyEmailConfirm(string tenantUid, string email, string code, string verificationUrl, string language)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { response = "Conflict, email is missing" }, JsonRequestBehavior.DenyGet);
            }
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = await verificationService.ConfirmEmailAsync(tenantUid, origin, email, code, new Uri(verificationUrl).ToString(), authorization.AccessToken);
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VerifyEmailSendCode(string tenantUid, string id, string email, string language)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var verificationUrl = helper.GetConfirmVerificationUrl(tenantUid, origin, email,language, id);
            var response = await verificationService.VerificationEmailAsync(tenantUid.ToString(), origin, new Uri(verificationUrl).ToString(), email, language, authorization.AccessToken);
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VerifyEmailReSendCode(string tenantUid, string id, string email, string language)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var verificationUrl = helper.GetConfirmVerificationUrl(tenantUid, origin, email,language, id);
            var response = await verificationService.VerificationEmailAsync(tenantUid.ToString(), origin, verificationUrl, email, language, authorization.AccessToken);
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VerifyMobileSendSMS(string tenantUid, string countryCode, string mobile, string language)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var mobileNumber = $"{countryCode}{mobile.TrimStart(new Char[] { '0' })}";
            var response = await verificationService.VerifyMobileAsync(tenantUid.ToString(), origin, mobileNumber, language, authorization.AccessToken);
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VerifyMobileReSendSMS(string tenantUid, string mobile, string language)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = await verificationService.VerifyMobileAsync(tenantUid.ToString(), origin, mobile, language, authorization.AccessToken);
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VerifyMobileValidateSMS(string tenantUid, string mobile, string code, string language)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = await verificationService.ValidateSmsVerificationCodeAsync(tenantUid, origin, mobile, code, authorization.AccessToken);
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VerifyMobileValidateSMS2(string tenantUid, string mobile, string code, string language, string token)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var response = await verificationService.ValidateSmsVerificationCodeAsync(tenantUid, origin, mobile, code, token);
            return Json(response, JsonRequestBehavior.DenyGet);
        }
    }
}
