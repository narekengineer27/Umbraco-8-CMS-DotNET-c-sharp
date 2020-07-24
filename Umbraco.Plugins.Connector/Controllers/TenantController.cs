using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web;
using Newtonsoft.Json;
using Umbraco.Plugins.Connector.Models;
using Umbraco.Plugins.Connector.Cache;
using Umbraco.Plugins.Connector.Helpers;
using Umbraco.Plugins.Connector.Services;
using System.Web.Configuration;
using System.Globalization;

namespace Umbraco.Plugins.Connector.Controllers
{

    public class TenantController : BaseController
    {
        [HttpPost]
        public JsonResult GetCards(Guid rootGuid, Guid customerGuid, string token, string pageId, string lang)
        {
            var apiService = new TotalCodeApiService();

            var root = Umbraco.Content(rootGuid);
            var tenantUid = root.GetProperty("tenantUid").GetValue().ToString();

            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);

            string type = string.Empty;
            int page_id; Int32.TryParse(pageId, out page_id);
            var currentPage = Umbraco.Content(page_id);
            if (currentPage != null)
            {
                if (currentPage.ContentType.Alias == "totalCodeDepositPage")
                {
                    type = "2";
                }
                if (currentPage.ContentType.Alias == "totalCodeWithdrawPage")
                {
                    type = "1";
                }
            }

            var preferences = root.Value<string>("tenantPreferencesProperty");
            var preferencesJson = JsonConvert.DeserializeObject<TenantPreferences>(preferences);

            var paymentMethods = preferencesJson.PaymentSettings.PaymentMethods.ToList();
            List<PaymentMethod> availablePaymentMethods = new List<PaymentMethod>();

            var userPaymentMethods = apiService.CustomerPaymentSystems(customerGuid.ToString(), token, origin, tenantUid, type);

            CultureInfo newLanguage = new CultureInfo(lang);
            System.Threading.Thread.CurrentThread.CurrentCulture = newLanguage;
            System.Threading.Thread.CurrentThread.CurrentUICulture = newLanguage;

            foreach (var method in paymentMethods)
            {
                method.PaymentSystemNameOrig = method.PaymentSystemName;
                var name = "[PM]" + method.PaymentSystemName;
                method.PaymentSystemName = Umbraco.GetDictionaryValue(name, method.PaymentSystemName);

                if (userPaymentMethods != null)
                {
                    foreach (var paymentId in userPaymentMethods.PaymentIds)
                    {
                        if (paymentId.PaymentIdentifier == method.PaymentIdentifier)
                        {
                            method.Priority = paymentId.Priority;
                            if (paymentId.Priority == null)
                            {
                                method.Priority = 99;
                            }
                            if (paymentId.IsDefault)
                            {
                                method.isDefault = true;
                            }
                            availablePaymentMethods.Add(method);
                        }
                    }
                }
            }

            availablePaymentMethods = availablePaymentMethods.OrderBy(x => x.Priority).ToList();

            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["TestApi"]))
            {
                paymentMethods.Add(new PaymentMethod
                {
                    PaymentIdentifier = "Cartipay",
                    PaymentSystemName = "Cartipay",
                    WithdrawalFields = new List<CustomField> {
                    new CustomField { Name = "CardNumber", Type = "card", Label = "Card Number", Value = "", Required = true, IsReadonly = false },
                    new CustomField { Name = "WithdrawAmount", Type = "amount", Label = "Amount", Value = "", Required = true, IsReadonly = false },
                },
                    DepositFields = new List<CustomField> {
                    new CustomField { Name = "CardNumber", Type = "card", Label = "Card Number", Value = "", Required = true, IsReadonly = false },
                    new CustomField { Name = "DepositAmount", Type = "amount", Label = "Amount", Value = "", Required = true, IsReadonly = false },
                }
                });

                paymentMethods.Add(new PaymentMethod
                {
                    PaymentIdentifier = "Cartipal",
                    PaymentSystemName = "Cartipal"
                });

                paymentMethods.Add(new PaymentMethod
                {
                    PaymentIdentifier = "Bitcoin",
                    PaymentSystemName = "Bitcoin",
                    WithdrawalFields = new List<CustomField> {
                    new CustomField { Name = "Amount Details", Type = "amount", Label = "Amount", Alias = "withdrawAmount", Value = "", Required = true, IsReadonly = false },
                    new CustomField { Name = "Currency", Type = "text", Label = "Currency", Alias = "currency", Value = "", Required = true, IsReadonly = false },
                    new CustomField { Name = "Bitcoin Address", Type = "text", Label = "Bitcoin Address", Alias = "bitCoinAddress", Value = "", Required = true, IsReadonly = false },
                },
                });
                return Json(paymentMethods, JsonRequestBehavior.DenyGet);
            }

            return Json(availablePaymentMethods, JsonRequestBehavior.DenyGet);

        }
    }
}
