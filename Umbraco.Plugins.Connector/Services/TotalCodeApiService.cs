namespace Umbraco.Plugins.Connector.Services
{
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Umbraco.Core.Cache;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Helpers;

    public class TotalCodeApiService : BaseService
    {
        private const int DEFAULT_LOGIN_EXPIRATION = 60; // MINUTES

        #region Api Urls

        private readonly string URL_API_KEY_LOGIN;
        private readonly string URL_CHANGE_EMAIL;
        private readonly string URL_CONFIRM_EMAIL;
        private readonly string URL_CUSTOMER_LOGIN;
        private readonly string URL_CUSTOMER_REGISTER;
        private readonly string URL_GET_ACCOUNT_BALANCE;
        private readonly string URL_GET_CUSTOMER;
        private readonly string URL_GET_CUSTOMER_EMAIL;
        private readonly string URL_RESET_PASSWORD_EMAIL;
        private readonly string URL_RESET_PASSWORD_SMS;
        private readonly string URL_RESET_PASSWORD_VERIFICATION_EMAIL;
        private readonly string URL_RESET_PASSWORD_VERIFICATION_SMS;
        private readonly string URL_RETRIEVE_USERNAME_EMAIL;
        private readonly string URL_RETRIEVE_USERNAME_SMS;
        private readonly string URL_VALIDATE_SMS_CODE;
        private readonly string URL_VERIFCATION_EMAIL_REQUEST;
        private readonly string URL_VERIFY_MOBILE;
        private readonly string URL_EDIT_CUSTOMER;
        private readonly string URL_CHANGE_MOBILE;
        private readonly string URL_CHANGE_PASSWORD;
        private readonly string URL_CHANGE_COMM_PREFERENCES;
        private readonly string URL_REFRESH_TOKEN;
        private readonly string URL_GET_GAME_DATA;
        private readonly string URL_GET_GAME_BY_ID;
        private readonly string URL_GET_POKER_LOBBY;
        private readonly string URL_GET_HUB_COMMECTION_DETAILS;
        private readonly string URL_CUSTOMER_PAYMENT_SYSTEMS;
        private readonly string URL_CHANGE_USERNAME;
        private readonly string URL_GET_ANONYMOUS_HUB_COMMECTION_DETAILS;
        #endregion

        public TotalCodeApiService()
        {
            URL_API_KEY_LOGIN = URL_USER_MANAGEMENT_DOMAIN + "/api/StaffUser/api-key-login";
            URL_CHANGE_EMAIL = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/change-email";
            URL_CONFIRM_EMAIL = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/confirm-email";
            URL_CUSTOMER_LOGIN = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Account/customer-login";
            URL_CUSTOMER_REGISTER = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/customer-register";
            URL_GET_ACCOUNT_BALANCE = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/Wallet/get-account-balance";
            URL_GET_CUSTOMER = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/get-customer";
            URL_GET_CUSTOMER_EMAIL = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/customer/get-emails";
            URL_RESET_PASSWORD_EMAIL = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Account/reset-password-email";
            URL_RESET_PASSWORD_SMS = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Account/reset-password-sms";
            URL_RESET_PASSWORD_VERIFICATION_EMAIL = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Account/reset-password-verification-email";
            URL_RESET_PASSWORD_VERIFICATION_SMS = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Account/reset-password-verification-sms";
            URL_RETRIEVE_USERNAME_EMAIL = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Account/retrieve-username-via-email";
            URL_RETRIEVE_USERNAME_SMS = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Account/retrieve-username-via-sms";
            URL_VALIDATE_SMS_CODE = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/validate-verification-code";
            URL_VERIFCATION_EMAIL_REQUEST = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/verification-email-request";
            URL_VERIFY_MOBILE = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/verify-mobile";
            URL_EDIT_CUSTOMER = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/edit-customer";
            URL_CHANGE_MOBILE = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/change-mobile";
            URL_CHANGE_PASSWORD = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Account/change-password";
            URL_CHANGE_COMM_PREFERENCES = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/change-communication-preferences";
            URL_REFRESH_TOKEN = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Account/refresh-token";
            URL_GET_GAME_DATA = URL_GAME_MANAGEMENT_DOMAIN + "/api/TenantGame/get-tenant-games";
            URL_GET_GAME_BY_ID = URL_GAME_MANAGEMENT_DOMAIN + "/api/TenantGame/get-tenant-game";
            URL_GET_POKER_LOBBY = URL_GAME_MANAGEMENT_DOMAIN + "/api/Poker/openlobby";
            URL_GET_HUB_COMMECTION_DETAILS = URL_NOTIFICATION_MANAGEMENT_DOMAIN + "/api/NotificationSignalR/gethubconnectiondetails";
            URL_CUSTOMER_PAYMENT_SYSTEMS = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/available-payment-systems";
            URL_CHANGE_USERNAME = URL_CUSTOMER_MANAGEMENT_DOMAIN + "/api/Customer/edit-username";
            URL_GET_ANONYMOUS_HUB_COMMECTION_DETAILS = URL_NOTIFICATION_MANAGEMENT_DOMAIN + "/api/NotificationSignalR/gethubconnectiondetailsanonymous";
        }

        private const string DATE_TIME_FORMAT = "yyyy-MM-ddTHH:mm:ss.fffZ";

        public IResponseContent ApiKeyLogin(string apiKey, string appId, string tenantUid = "")
        {
            var login = new ApiKeyLogin
            {
                ApiKey = apiKey,
                PlatformGuid = appId
            };

            try
            {
                IRestResponse response = SubmitPost(url: URL_API_KEY_LOGIN, origin: string.Empty, parameter: login, tenantUid: tenantUid);
                return AssertResponseContent<ApiKeyLoginResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new ApiKeyLoginResponseContent
                {
                    Exception = ex
                };
            }

        }

        public async Task<IResponseContent> ApiKeyLoginAsync(string apiKey, string appId, string tenantUid = "")
        {
            var login = new ApiKeyLogin
            {
                ApiKey = apiKey,
                PlatformGuid = appId
            };

            try
            {
                IRestResponse response = await SubmitPostAsync(url: URL_API_KEY_LOGIN, origin: string.Empty, parameter: login, tenantUid: tenantUid);
                return await AssertResponseContentAsync<ApiKeyLoginResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new ApiKeyLoginResponseContent
                {
                    Exception = ex
                };
            }

        }

        public async Task<IResponseContent> ApiKeyLoginAsync(ApiKeys key)
        {
            return await this.ApiKeyLoginAsync(key.ApiKey, key.AppId, key.TenantId != null ? key.TenantId.ToString() : string.Empty);
        }

        public async Task<IResponseContent> ChangeEmailAsync(string tenantUid, string origin, string username, string email, string authorization = "")
        {
            var changeEmail = new ChangeEmail
            {
                Email = email,
                Username = username,
            };
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_CHANGE_EMAIL, origin, changeEmail, authorization);
                return await AssertResponseContentAsync<ChangeEmailResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new ChangeEmailResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> ConfirmEmailAsync(string tenantUid, string origin, string email, string code, string verificationUrl, string authorization = "")
        {
            var date = DateTime.UtcNow;
            var confirmEmail = new ConfirmEmail
            {
                Email = email,
                VerificationDate = date.ToString(DATE_TIME_FORMAT, new CultureInfo("en")),
                VerificationUrl = new Uri(verificationUrl).ToString()
            };
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_CONFIRM_EMAIL, origin, confirmEmail, authorization);
                return await AssertResponseContentAsync<ConfirmEmailResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new ConfirmEmailResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> CustomerRegisterAsync(
            string tenantUid,
            string origin,
            string email,
            string username,
            string password,
            string mobile = "",
            string title = "",
            string firstName = "",
            string lastName = "",
            string gender = "",
            string dob = "",
            string address1 = "",
            string address2 = "",
            string address3 = "",
            string town = "",
            string county = "",
            string postCode = "",
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
            string notifyViaSMS = "",
            string authorization = ""
            )
        {
            var customer = new CustomerRegister
            {
                AddressLine1 = address1,
                AddressLine2 = address2,
                AddressLine3 = address3,
                CountryCode = country,
                County = county,
                CurrencyCode = currency,
                DOB = dob,
                Email = email,
                FirstName = firstName,
                Gender = gender,
                LanguageCode = language,
                LastName = lastName,
                Mobile = mobile,
                Password = password,
                PostCode = postCode,
                TimeZoneCode = timeZone,
                Title = title,
                Town = town,
                Username = username,
                BonusCode = bonusCode,
                Referrer = referrer,
                OddsDisplay = odds,
                EmailComPref = notifyViaEmail,
                InPlatformComPref = notifyViaPlatform,
                NotificationComPref = notify,
                TextMessageComPref = notifyViaSMS
            };
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_CUSTOMER_REGISTER, "", customer, authorization, tenantUid);
                return await AssertResponseContentAsync<CustomerRegisterResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new CustomerRegisterResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> ForgotUsernameEmailAsync(string tenantUid, string origin, string email, string language, string authorization = "")
        {
            var forgot = new RetrieveUsernameViaEmail
            {
                LanguageCode = language,
                Email = email,
                //TenantPlatformMapGuid = tenantUid
            };
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_RETRIEVE_USERNAME_EMAIL, origin, forgot, authorization);
                return await AssertResponseContentAsync<RetrieveUsernameResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new RetrieveUsernameResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> ForgotUsernameSmsAsync(string tenantUid, string origin, string mobile, string language, string authorization = "")
        {
            var forgot = new RetrieveUsernameViaSms
            {
                LanguageCode = language,
                MobileNumber = mobile,
                //TenantPlatformMapGuid = tenantUid
            };
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_RETRIEVE_USERNAME_SMS, origin, forgot, authorization);
                return await AssertResponseContentAsync<RetrieveUsernameResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new RetrieveUsernameResponseContent
                {
                    Exception = ex
                };
            }
        }

        public IResponseContent GetCustomerEmail(string tenantUid, string origin, string customerGuid, string customerToken)
        {
            var url = $"{URL_GET_CUSTOMER_EMAIL}/{customerGuid}";
            try
            {
                IRestResponse response = SubmitGet(url, origin, customerToken, tenantUid);
                return AssertResponseContent<GetCustomerEmailResponseCOntent>(response);
            }
            catch (Exception ex)
            {
                return new GetCustomerEmailResponseCOntent
                {
                    Exception = ex
                };
            }
        }

        public IResponseContent GetAccountBalance(string tenantUid, string origin, string customerGuid, string customerToken)
        {
            var customerInfo = new CustomerAccountBalance
            {
                CustomerGuid = customerGuid
            };
            // temporary fix using query string
            var url = $"{URL_GET_ACCOUNT_BALANCE}/{customerGuid}";
            try
            {
                //IRestResponse response = SubmitGet(URL_GET_ACCOUNT_BALANCE, origin, customerInfo, customerToken);
                IRestResponse response = SubmitGet(url, origin, customerInfo, customerToken, tenantUid);
                return AssertResponseContent<GetCustomerAccountBalanceResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new GetCustomerAccountBalanceResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> GetAccountBalanceAsync(string tenantUid, string origin, string customerGuid, string customerToken)
        {
            var customerInfo = new CustomerAccountBalance
            {
                CustomerGuid = customerGuid
            };
            // temporary fix using query string
            var url = $"{URL_GET_ACCOUNT_BALANCE}/{customerGuid}";
            try
            {
                //IRestResponse response = await SubmitGetAsync(URL_GET_ACCOUNT_BALANCE, origin, customerInfo, customerToken);
                IRestResponse response = await SubmitGetAsync(url, origin, customerInfo, customerToken, tenantUid);
                return await AssertResponseContentAsync<GetCustomerAccountBalanceResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new GetCustomerAccountBalanceResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> GetCustomerInfoAsync(string tenantUid, string origin, string username, string customerToken)
        {
            var customerInfo = new CustomerInfo
            {
                Username = username
            };

            try
            {
                IRestResponse response = await SubmitPostAsync(URL_GET_CUSTOMER, origin, customerInfo, customerToken, tenantUid);
                return await AssertResponseContentAsync<GetCustomerInfoResponseContent>(response);
            }
            catch (Exception ex)
            {
                var logger = ConnectorContext.Logger;
                logger.Debug(typeof(TotalCodeApiService), ex.Message);
                logger.Debug(typeof(TotalCodeApiService), ex.StackTrace);
                logger.Debug(typeof(TotalCodeApiService), JsonConvert.SerializeObject(ex));
                return new GetCustomerInfoResponseContent
                {
                    Exception = ex
                };
            }
        }

        public CustomerSummary GetCustomerSummary(string tenantUid, string origin, JwtCustomerDataResponseContent decodedJwt)
        {
            var response = (GetCustomerAccountBalanceResponseContent)GetAccountBalance(tenantUid, origin, decodedJwt.CustomerGuid.Value.ToString(), decodedJwt.EncodedToken);
            if (response.Success)
            {
                return new CustomerSummary
                {
                    CustomerGuid = decodedJwt.CustomerGuid.Value,
                    CurrencyCode = decodedJwt.CurrencyCode,
                    Balance = new Balance
                    {
                        CurrentBalance = DefaultAllowedValues.DecimalAccuracy(response.Payload.TotalAccountBalance, decodedJwt.CurrencyCode),
                        Bonus = DefaultAllowedValues.DecimalAccuracy(response.Payload.BonusBalance, decodedJwt.CurrencyCode),
                        Withdrawable = DefaultAllowedValues.DecimalAccuracy(response.Payload.WithdrawableBalance, decodedJwt.CurrencyCode),
                        IsLiveBalance = true
                    },
                    LanguageCode = decodedJwt.LanguageCode,
                    OddsDisplayName = decodedJwt.OddsDisplay,
                    TenantPlatformMapGuid = decodedJwt.TenantPlatformMapGuid,
                    TimeZone = decodedJwt.TimeZone,
                    Username = decodedJwt.Username
                };
            }
            else return new CustomerSummary
            {
                Username = decodedJwt.Username,
                CurrencyCode = decodedJwt.CurrencyCode,
                Balance = new Balance
                {
                    Bonus = DefaultAllowedValues.DecimalAccuracy(0.00m, decodedJwt.CurrencyCode),
                    CurrentBalance = DefaultAllowedValues.DecimalAccuracy(0.00m, decodedJwt.CurrencyCode),
                    Withdrawable = DefaultAllowedValues.DecimalAccuracy(0.00m, decodedJwt.CurrencyCode),
                    IsLiveBalance = false,
                    BalanceRetrievalFailureMessage = response.Message,
                    Errors = response.Errors
                },
                LanguageCode = decodedJwt.LanguageCode,
                OddsDisplayName = decodedJwt.OddsDisplay,
                TimeZone = decodedJwt.TimeZone,
                TenantPlatformMapGuid = decodedJwt.TenantPlatformMapGuid,
                CustomerGuid = decodedJwt.CustomerGuid
            };
        }

        public async Task<CustomerSummary> GetCustomerSummaryAsync(string tenantUid, string origin, JwtCustomerDataResponseContent decodedJwt)
        {
            var response = (GetCustomerAccountBalanceResponseContent)await GetAccountBalanceAsync(tenantUid, origin, decodedJwt.CustomerGuid.Value.ToString(), decodedJwt.EncodedToken);
            var payload = response.Payload;
            return new CustomerSummary
            {
                CustomerGuid = decodedJwt.CustomerGuid.Value,
                CurrencyCode = decodedJwt.CurrencyCode,
                Balance = new Balance
                {
                    CurrentBalance = payload != null ? DefaultAllowedValues.DecimalAccuracy(payload.TotalAccountBalance, decodedJwt.CurrencyCode) : DefaultAllowedValues.DecimalAccuracy(0.00m, decodedJwt.CurrencyCode),
                    Bonus = payload != null ? DefaultAllowedValues.DecimalAccuracy(payload.BonusBalance, decodedJwt.CurrencyCode) : DefaultAllowedValues.DecimalAccuracy(0.00m, decodedJwt.CurrencyCode),
                    Withdrawable = payload != null ? DefaultAllowedValues.DecimalAccuracy(payload.WithdrawableBalance, decodedJwt.CurrencyCode) : DefaultAllowedValues.DecimalAccuracy(0.00m, decodedJwt.CurrencyCode),
                    IsLiveBalance = response.Success
                },
                LanguageCode = decodedJwt.LanguageCode,
                OddsDisplayName = decodedJwt.OddsDisplay,
                TenantPlatformMapGuid = decodedJwt.TenantPlatformMapGuid,
                TimeZone = decodedJwt.TimeZone,
                Username = decodedJwt.Username
            };
        }

        public JwtCustomerDataResponseContent Decode(string token)
        {
            return DecodeJwt(token);
        }

        public async Task<JwtCustomerDataResponseContent> DecodeAsync(string token)
        {
            return await DecodeJwtAsync(token);
        }

        public async Task<IResponseContent> LoginAsync(string tenantUid, string origin, string credential, string password, string rememberMe = "off", string authorization = "")
        {
            var login = new Login
            {
                Credential = credential,
                Password = password,
                RememberMe = rememberMe != "off" ? true : false
            };

            try
            {
                IRestResponse response = await SubmitPostAsync(URL_CUSTOMER_LOGIN, origin, login, authorization, tenantUid);
                var responseResult = (LoginResponseContent)await AssertResponseContentAsync<LoginResponseContent>(response);
                #region Default Cookie Generator [Obsolete]
                // format date to Thu, 18 Dec 2013 12:00:00 UTC used with default cookie generator
                //const string dateFormat = "ddd, dd MMM yyyy hh:mm:ss UTC";
                //if (rememberMe.Equals("on"))
                //{
                //    responseResult.Expires = DateTime.UtcNow.AddDays(999).ToString(dateFormat);
                //}

                // to use with cookies plugin 
                #endregion
                if (rememberMe.Equals("on"))
                {
                    responseResult.Expires = 999;
                }
                else responseResult.Expires = DEFAULT_LOGIN_EXPIRATION;
                responseResult.Credential = credential;
                responseResult.LastLogin = DateTime.UtcNow;

                return responseResult;
            }
            catch (Exception ex)
            {
                return new LoginResponseContent
                {
                    Exception = ex
                };
            }
        }
        public async Task<IResponseContent> LoginFormAsync(string tenantUid, string origin, string credential, string password, string rememberMe = "off", string authorization = "")
        {
            var login = new LoginForm
            {
                Credential = credential,
                Password = password,
                RememberMe = rememberMe != "off" ? true : false,
                authType = "internal"
            };

            try
            {
                IRestResponse response = await SubmitPostAsync(URL_CUSTOMER_LOGIN, origin, login, authorization, tenantUid);
                var responseResult = (LoginResponseContent)await AssertResponseContentAsync<LoginResponseContent>(response);
                #region Default Cookie Generator [Obsolete]
                // format date to Thu, 18 Dec 2013 12:00:00 UTC used with default cookie generator
                //const string dateFormat = "ddd, dd MMM yyyy hh:mm:ss UTC";
                //if (rememberMe.Equals("on"))
                //{
                //    responseResult.Expires = DateTime.UtcNow.AddDays(999).ToString(dateFormat);
                //}

                // to use with cookies plugin 
                #endregion
                if (rememberMe.Equals("on"))
                {
                    responseResult.Expires = 999;
                }
                else responseResult.Expires = DEFAULT_LOGIN_EXPIRATION;
                responseResult.Credential = credential;
                responseResult.LastLogin = DateTime.UtcNow;

                return responseResult;
            }
            catch (Exception ex)
            {
                return new LoginResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task LogoutAsync() => await LoginSession.LogoutAsync();
        public void Logout() => LoginSession.Logout();

        public async Task<IResponseContent> SendPasswordEmailAsync(string tenantUid, string origin, string username, string email, string language, string url, string authorization = "")
        {
            var reset = new ResetPasswordVerificationEmail
            {
                Email = email,
                LanguageCode = language,
                //TenantPlatformMapGuid = tenantUid,
                Username = username,
                VerificationUrl = url
            };
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_RESET_PASSWORD_VERIFICATION_EMAIL, origin, reset, authorization, tenantUid);
                return await AssertResponseContentAsync<ResetPasswordVerificationEmailResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new ResetPasswordVerificationEmailResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> SendPasswordSmsAsync(string tenantUid, string origin, string username, string mobile, string language, string authorization = "")
        {
            var reset = new ResetPasswordVerificationSms
            {
                LanguageCode = language,
                //TenantPlatformMapGuid = tenantUid,
                Username = username,
                MobileNumber = mobile
            };
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_RESET_PASSWORD_VERIFICATION_SMS, origin, reset, authorization, tenantUid);
                return await AssertResponseContentAsync<ResetPasswordVerificationSmsResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new ResetPasswordVerificationSmsResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> ValidateSmsVerificationCodeAsync(string tenantUid, string origin, string mobile, string code, string authorization = "")
        {
            var date = DateTime.UtcNow;
            var validateSms = new ValidateVerificationCode
            {
                //TenantPlatformMapGuid = tenantUid,
                Mobile = mobile,
                VerificationCode = code,
                VerificationDate = date.ToString(DATE_TIME_FORMAT, new CultureInfo("en"))
            };

            try
            {
                IRestResponse response = await SubmitPostAsync(URL_VALIDATE_SMS_CODE, origin, validateSms, authorization, tenantUid);
                return await AssertResponseContentAsync<ValidateVerificationCodeResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new ValidateVerificationCodeResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> VerificationEmailAsync(string tenantUid, string origin, string verificationUrl, string email, string language, string authorization = "")
        {
            var verifyEmail = new VerifyEmail
            {
                //TenantPlatformMapGuid = tenantUid,
                Email = email,
                VerificationUrl = verificationUrl
            };

            try
            {
                IRestResponse response = await SubmitPostAsync(URL_VERIFCATION_EMAIL_REQUEST, origin, verifyEmail, authorization, tenantUid);
                return await AssertResponseContentAsync<VerifyEmailResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new VerifyEmailResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> VerifyMobileAsync(string tenantUid, string origin, string mobile, string language, string authorization = "")
        {
            var date = DateTime.UtcNow;
            var verifyMobile = new VerifyMobile
            {
                //TenantPlatformMapGuid = tenantUid,
                Mobile = mobile,
                LanguageCode = language,
                RequestDate = date.ToString(DATE_TIME_FORMAT, new CultureInfo("en"))
            };

            try
            {
                IRestResponse response = await SubmitPostAsync(URL_VERIFY_MOBILE, origin, verifyMobile, authorization, tenantUid);
                return await AssertResponseContentAsync<VerifyMobileResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new VerifyMobileResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> VerifyPasswordResetEmailAsync(string tenantUid, string origin, string username, string url, string newPassword, string authorization = "")
        {
            var email = new ResetPasswordEmail
            {
                NewPassword = newPassword,
                //TenantPlatformMapGuid = tenantUid,
                Username = username,
                VerificationUrl = url
            };
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_RESET_PASSWORD_EMAIL, origin, email, authorization, tenantUid);
                return await AssertResponseContentAsync<ResetPasswordEmailResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new ResetPasswordEmailResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> VerifyPasswordResetSmsAsync(string tenantUid, string origin, string username, string code, string newPassword, string authorization = "")
        {
            var sms = new ResetPasswordSms
            {
                NewPassword = newPassword,
                //TenantPlatformMapGuid = tenantUid,
                Username = username,
                VerificationCode = code
            };
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_RESET_PASSWORD_SMS, origin, sms, authorization, tenantUid);
                return await AssertResponseContentAsync<ResetPasswordSmsResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new ResetPasswordSmsResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> EditCustomerAsync(EditCustomer customer, string tenantUid, string origin, string token)
        {
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_EDIT_CUSTOMER, origin, customer, token, tenantUid);
                return await AssertResponseContentAsync<EditCustomerResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new EditCustomerResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> EditCustomerUsernameAsync(EditUsername customerUsername, string tenantUid, string origin, string token)
        {
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_CHANGE_USERNAME, origin, customerUsername, token, tenantUid);
                return await AssertResponseContentAsync<EditUsernameResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new EditUsernameResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> EditCommunicationPreferencesAsync(EditCommunicationPreferences comm, string tenantUid, string origin, string token)
        {
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_CHANGE_COMM_PREFERENCES, origin, comm, token, tenantUid);
                return await AssertResponseContentAsync<EditCommunicationPreferencesResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new EditCommunicationPreferencesResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> RefreshTokenAsync(string tenantUid, string origin, string token)
        {
            try
            {
                IRestResponse response = await SubmitGetAsync(URL_REFRESH_TOKEN, origin, token, tenantUid);
                var responseResult = (LoginResponseContent)await AssertResponseContentAsync<LoginResponseContent>(response);
                var login = LoginSession.DecodeJwtToken(responseResult.Token);

                responseResult.Credential = login.Username;
                responseResult.LastLogin = DateTime.UtcNow;
                responseResult.Expires = DEFAULT_LOGIN_EXPIRATION;

                return responseResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IResponseContent RefreshToken(string tenantUid, string origin, string token)
        {
            try
            {
                IRestResponse response = SubmitGet(URL_CUSTOMER_LOGIN, origin, token, tenantUid);
                var responseResult = (LoginResponseContent)AssertResponseContent<LoginResponseContent>(response);
                var login = LoginSession.DecodeJwtToken(responseResult.Token);

                responseResult.Credential = login.Username;
                responseResult.LastLogin = DateTime.UtcNow;

                return responseResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IResponseContent> EditEmailAsync(EditEmail email, string tenantUid, string origin, string token)
        {
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_CHANGE_EMAIL, origin, email, token, tenantUid);
                return await AssertResponseContentAsync<EditEmailResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new EditEmailResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> EditMobileAsync(EditMobileNumber mobile, string tenantUid, string origin, string token)
        {
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_CHANGE_MOBILE, origin, mobile, token, tenantUid);
                return await AssertResponseContentAsync<EditMobileNumberResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new EditMobileNumberResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> EditPasswordAsync(EditPassword password, string tenantUid, string origin, string token)
        {
            try
            {
                IRestResponse response = await SubmitPostAsync(URL_CHANGE_PASSWORD, origin, password, token, tenantUid);
                return await AssertResponseContentAsync<EditPasswordResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new EditPasswordResponseContent
                {
                    Exception = ex
                };
            }
        }

        public async Task<IResponseContent> GetGameDataAsync(string tenantUid, string origin, string category = "", string subCategory = "", string provider = "", string keyword = "", string languageCode = "", string authorization = "")
        {
            try
            {
                var gameParameter = new GameData
                {
                    Category = category,
                    SubCategory = subCategory,
                    Provider = provider,
                    Keyword = keyword
                };

                IRestResponse response = await SubmitPostAsync(URL_GET_GAME_DATA, origin, gameParameter, authorization, tenantUid);
                return await AssertResponseContentAsync<GameDataResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new GameDataResponseContent
                {
                    Exception = ex
                };
            }
        }

        public List<GameDetails> GetGameDataAnonymousArray(string tenantUid, string origin, string category = "", string subCategory = "", string provider = "", string keyword = "", string languageCode = "", string authorization = "")
        {
            try
            {
                var gameParameter = new GameData
                {
                    Category = category,
                    SubCategory = subCategory,
                    Provider = provider,
                    Keyword = keyword
                };

                var result = CacheHelper.GetOrSetCache<List<GameDetails>>(
                                   new CacheInfo()
                                   {
                                       CacheName = "GetGameDataAnonymousArray",
                                       TenantUid = tenantUid,
                                       Tags = new CacheTagBuilder().Add(() => origin)
                                                                    .Add(() => category)
                                                                    .Add(() => subCategory)
                                                                    .Add(() => provider)
                                                                    .Add(() => keyword)
                                                                    .Add(() => languageCode)
                                                                    .Compile()
                                   }
                                   , () =>
                                   {
                                       IRestResponse response = SubmitPost(URL_GET_GAME_DATA, origin, gameParameter, authorization, tenantUid, locale: languageCode);
                                       var data = GameDataAnonymousArrayResponseContent(response);
                                       var res = data.OrderBy(x => x.Priority).ToList();
                                       return res;
                                   }, TimeSpan.FromSeconds(CacheSetting.TimeoutInSeconds));

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IResponseContent GetGameData(string tenantUid, string origin, string category = "", string subCategory = "", string provider = "", string keyword = "", string languageCode = "", string authorization = "")
        {
            try
            {
                var gameParameter = new GameData
                {
                    Category = category,
                    SubCategory = subCategory,
                    Provider = provider,
                    Keyword = keyword
                };

                IRestResponse response = SubmitPost(URL_GET_GAME_DATA, origin, gameParameter, authorization, tenantUid, locale: languageCode);
                return AssertResponseContent<GameDataResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new GameDataResponseContent
                {
                    Exception = ex
                };
            }
        }
        public IResponseContent GetGameById(string tenantUid, string origin, int gameId, string gameIdentifier = null, string authorization = "", string languageCode = "")
        {
            try
            {
                var gameParameter = new GameById
                {
                    GameId = gameId,
                    GameIdentifier = gameIdentifier
                };

                IRestResponse response = SubmitPost(URL_GET_GAME_BY_ID, origin, gameParameter, authorization, tenantUid, locale: languageCode);

                return AssertResponseContent<GameByIdResponseContent>(response);
            }
            catch (Exception ex)
            {
                return new GameByIdResponseContent
                {
                    Exception = ex
                };
            }
        }

        public IResponseContent GetPokerLobby(string tenantUid, string origin, string customerToken, string locale, string returnUrl = "")
        {
            try
            {
                IRestResponse response = SubmitPost(URL_GET_POKER_LOBBY, origin, null, customerToken, tenantUid, locale, returnUrl);
                var asserted = (PokerLobbyResponseContent)AssertResponseContent<PokerLobbyResponseContent>(response);
                asserted.Success = true;
                return asserted;
            }
            catch (Exception ex)
            {
                return new PokerLobbyResponseContent
                {
                    Exception = ex
                };
            }
        }
        public IResponseContent GetGameLobby(string lobby, string tenantUid, string origin, string customerToken, string locale = "", string returnUrl = "")
        {
            try
            {
                IRestResponse response = SubmitPost(lobby, origin, null, customerToken, tenantUid, locale, returnUrl);
                var asserted = (GameLobbyResponseContent)AssertResponseContent<GameLobbyResponseContent>(response);
                asserted.Success = true;
                return asserted;
            }
            catch (Exception ex)
            {
                return new GameLobbyResponseContent
                {
                    Exception = ex
                };
            }
        }
        public async Task<IResponseContent> GetHubConnectionAsync(string customerGuid, string origin, string customerToken)
        {
            try
            {
                var data = new HubUserId
                {
                    UserId = customerGuid
                };
                IRestResponse response = await SubmitPostAsync(URL_GET_HUB_COMMECTION_DETAILS, origin, data, authorization: customerToken);
                var asserted = (HubConnectionDetailsResponseContent)AssertResponseContent<HubConnectionDetailsResponseContent>(response);
                asserted.Success = true;
                return asserted;
            }
            catch (Exception ex)
            {
                return new HubConnectionDetailsResponseContent
                {
                    Exception = ex
                };
            }
        }
        public async Task<IResponseContent> GetAnonymousHubConnectionAsync(string origin,string tenantUid,string authorization)
        {
            try
            {
                var data = new AnonymousUser
                {
                    tenantUId = tenantUid
                };
                IRestResponse response = await SubmitPostAsync(URL_GET_ANONYMOUS_HUB_COMMECTION_DETAILS, origin, data, authorization, tenantUid);
                var asserted = (HubConnectionDetailsResponseContent)AssertResponseContent<HubConnectionDetailsResponseContent>(response);
                asserted.Success = true;
                return asserted;
            }
            catch (Exception ex)
            {
                return new HubConnectionDetailsResponseContent
                {
                    Exception = ex
                };
            }
        }

        public PaymentSystemsDetailsResponseContent CustomerPaymentSystems(string customerGuid, string token, string origin, string tenantUid, string type)
        {
            try
            {
                var data = new CustomerPaymentSystems
                {
                    CustomerGuid = customerGuid,
                    TransactionType = type
                };

                AppCaches _appCaches = new AppCaches();
                var cacheName = "CustomerPaymentSystems" + $"_customerGuid:{customerGuid}_token:{token}_origin:{origin}_tenantUid:{tenantUid}_type:{type}";
                var cacheData = _appCaches.RuntimeCache.GetCacheItem<PaymentSystemsDetailsResponseContent>(cacheName);

                if (cacheData == null)
                {
                    IRestResponse response = SubmitPost(URL_CUSTOMER_PAYMENT_SYSTEMS, origin, data, authorization: token, tenantUid: tenantUid);
                    var ids = JsonConvert.DeserializeObject<PaymentId[]>(response.Content);
                    PaymentSystemsDetailsResponseContent result = new PaymentSystemsDetailsResponseContent
                    {
                        PaymentIds = ids
                    };
                    _appCaches.RuntimeCache.InsertCacheItem<PaymentSystemsDetailsResponseContent>(cacheName, () => { return result; });

                    return result;
                }
                else
                {
                    return cacheData;
                }

            }
            catch (Exception ex)
            {
                return new PaymentSystemsDetailsResponseContent
                {
                    Exception = ex
                };
            }
        }
    }
}