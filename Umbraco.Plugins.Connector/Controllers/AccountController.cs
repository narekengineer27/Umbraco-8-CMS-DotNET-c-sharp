namespace Umbraco.Plugins.Connector.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.UI;
    using Umbraco.Core.Services;
    using Umbraco.Web.Mvc;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Plugins.Connector.Models;
    public class AccountController : BaseController
    {
        private readonly Helpers.UrlHelper helper;
        public AccountController()
        {
            helper = new Helpers.UrlHelper(Umbraco);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangePasswordViaEmail(string tenantUid, string username, string code, string password)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = await apiService.VerifyPasswordResetEmailAsync(tenantUid, origin, username, code, password, authorization.AccessToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangePasswordViaSms(string tenantUid, string username, string code, string password)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = await apiService.VerifyPasswordResetSmsAsync(tenantUid, origin, username, code, password, authorization.AccessToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditCommunicationPreferences(string tenantUid, string username, string customerToken, string notify = "false", string notifyViaSms = "false", string notifyViaEmail = "false", string notifyViaPlatform = "false")
        {
            var notifications = new EditCommunicationPreferences
            {
                Username = username,
                CommunicationPreferences = new CommunicationPreferenceParameter[]
                {
                    new CommunicationPreferenceParameter
                    {
                        Communication = "Notification",
                        IsSelected = bool.Parse(notify)
                    },
                    new CommunicationPreferenceParameter
                    {
                        Communication = "TextMessage",
                        IsSelected = bool.Parse(notifyViaSms)
                    },
                    new CommunicationPreferenceParameter
                    {
                        Communication = "Email",
                        IsSelected = bool.Parse(notifyViaEmail)
                    },
                    new CommunicationPreferenceParameter
                    {
                        Communication = "InPlatformMessage",
                        IsSelected = bool.Parse(notifyViaPlatform)
                    }
                }

            };
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);

            var response = await apiService.EditCommunicationPreferencesAsync(notifications, tenantUid, origin, customerToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditCustomer(string tenantUid, string title, string firstname, string lastname, string gender, string day, string month, string year, string country, string countrycode, string timezone, string odds, string language, string username, string customerGuid, string customerToken, string address1 = "", string address2 = "", string address3 = "", string town = "", string county = "", string postalcode = "", string notify = "true")
        {
            var customer = new EditCustomer
            {
                Username = username,
                Fields = new Models.EditCustomer.EditFields
                {
                    CountryCode = countrycode,
                    DOB = !string.IsNullOrEmpty(day) ? $"{year}-{month}-{day}" : string.Empty,
                    FirstName = firstname,
                    LastName = lastname,
                    Gender = gender,
                    LanguageCode = language,
                    OddsDisplay = odds,
                    TimeZoneCode = timezone,
                    Title = title,
                    AddressLine1 = address1,
                    AddressLine2 = address2,
                    AddressLine3 = address3,
                    County = county,
                    Town = town,
                    Country = country,
                    PostCode = postalcode,
                    NotificationComPref = notify
                }
            };
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);

            var response = await apiService.EditCustomerAsync(customer, tenantUid, origin, customerToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditCustomerUsername(string tenantUid,  string username, string customerGuid, string customerToken)
        {
            var customer = new EditUsername
            {
                Username = username,
                CustomerGuid = customerGuid
            };
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);

            var response = await apiService.EditCustomerUsernameAsync(customer, tenantUid, origin, customerToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditEmail(string tenantUid, string username, string customerToken, string email, string language)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var verificationUrl = new Uri(new Uri(origin), helper.GetConfirmVerificationUrl(tenantUid, origin, email, language));

            var newEmail = new EditEmail
            {
                Username = LoginSession.Username,
                Email = email,
                VerificationUrl = verificationUrl.ToString()
            };
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);

            var response = await apiService.EditEmailAsync(newEmail, tenantUid, origin, customerToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditMobileNumber(string tenantUid, string username, string customerToken, string mobile, string countryCode)
        {
            var newMobile = new EditMobileNumber
            {
                Username = LoginSession.Username,
                Mobile = $"{countryCode}{mobile}"
            };
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);

            var response = await apiService.EditMobileAsync(newMobile, tenantUid, origin, customerToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditPassword(string tenantUid, string username, string customerToken, string oldpassword, string newpassword)
        {
            var newPassword = new EditPassword
            {
                Username = LoginSession.Username,
                OldPassword = oldpassword,
                NewPassword = newpassword
            };

            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);

            var response = await apiService.EditPasswordAsync(newPassword, tenantUid, origin, customerToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ForgotPasswordSendEmail(string tenantUid, string email, string username, string language, string requestUrl)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var verificationUrl = helper.GetResetPasswordVerificationUrl(tenantUid, requestUrl, username, language);
            var response = await apiService.SendPasswordEmailAsync(tenantUid, origin, username, email, language, verificationUrl.ToString(), authorization.AccessToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ForgotPasswordSendSms(string tenantUid, string countryCode, string mobile, string username, string language)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var mobileNumber = $"{countryCode}{mobile}";
            var response = await apiService.SendPasswordSmsAsync(tenantUid, origin, username, mobileNumber, language, authorization.AccessToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ForgotUsernameEmail(string tenantUid, string email, string language)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = await apiService.ForgotUsernameEmailAsync(tenantUid, origin, email, language, authorization.AccessToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ForgotUsernameSms(string tenantUid, string countryCode, string mobile, string language)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var mobileNumber = $"{countryCode}{mobile}";
            var response = await apiService.ForgotUsernameSmsAsync(tenantUid, origin, mobileNumber, language, authorization.AccessToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetCustomerEmail(string tenantUid, string customerGuid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = apiService.GetCustomerEmail(tenantUid, origin, customerGuid, authorization.AccessToken);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> SendVerificationEmail(string tenantUid, string email, string language)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var verificationUrl = helper.GetConfirmVerificationUrl(tenantUid, origin, email, language);
            var postRegisterResponse = await apiService.VerificationEmailAsync(tenantUid.ToString(), origin, verificationUrl, email, language, authorization.AccessToken);
            return Json(postRegisterResponse, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> GetAccessToken(string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = authorization.AccessToken;

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetCustomer(string tenantUid, string username, string customerToken)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);

            var response = await apiService.GetCustomerInfoAsync(tenantUid, origin, username, customerToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> GetCustomerSummaryAsync(string tenantUid, string customerToken)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var response = await apiService.GetCustomerSummaryAsync(tenantUid, origin, LoginSession.DecodeJwtToken(customerToken));

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> GetHubConnectionAsync(string userId, string tenantUid, string customerToken)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var response = await apiService.GetHubConnectionAsync(userId, origin, customerToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> GetAnonymousHubConnectionAsync(string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            
            var response = await apiService.GetAnonymousHubConnectionAsync(origin,tenantUid, authorization.AccessToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }


        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Login(string tenantUid, string credential, string password, string rememberMe = "off")
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = await apiService.LoginAsync(tenantUid, origin, credential, password, rememberMe, authorization.AccessToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> LoginForm(string tenantUidForm, string credentialForm, string passwordForm, string rememberMe = "off")
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUidForm);
            var key = ApiKeyCache.GetByTenantUid(tenantUidForm);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = await apiService.LoginFormAsync(tenantUidForm, origin, credentialForm, passwordForm, rememberMe, authorization.AccessToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> Logout()
        {
            await apiService.LogoutAsync();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> RefreshToken(string tenantUid, string token)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var response = (LoginResponseContent)await apiService.RefreshTokenAsync(tenantUid, origin, token);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
