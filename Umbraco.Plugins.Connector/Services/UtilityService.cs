using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Plugins.Connector.Interfaces;
using Umbraco.Plugins.Connector.Models;

namespace Umbraco.Plugins.Connector.Services
{
    public class UtilityService : BaseService, IUtilityService
    {
        private string URL_CURRENCY;

        public UtilityService()
        {
            URL_CURRENCY = URL_CURRENCY_MANAGEMENT_DOMAIN + "/api/ExchangeCurrency/umbraco-exchange-rate";
        }

        public async Task<IResponseContent> PostAsync(string token, string origin, string tenantUid, string baseCurrency, string quoteCurrency, string paymentSystem)
        {

            var request = new ExchangeRequestParams
            {
                PaymentSystemName = paymentSystem,
                BaseCurrency = baseCurrency,
                QuoteCurrency = quoteCurrency
            };

            var response = await SubmitPostAsync($"{URL_CURRENCY}", origin, request, authorization: token, tenantUid: tenantUid);

            var responseContent = AssertResponseContent<CurrencyResponseContent>(response);

            return responseContent;
        }
    }
}
