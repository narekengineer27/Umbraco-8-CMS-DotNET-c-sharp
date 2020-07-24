using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Umbraco.Plugins.Connector.Interfaces;
using Umbraco.Plugins.Connector.Models;
using Umbraco.Core.Logging;

namespace Umbraco.Plugins.Connector.Services
{
    public class TransactionService : BaseService, ITransactionService
    {

        private string URL_API_DEPOSIT;
        private string URL_API_WITHDRAW;

        private readonly ILogger _logger;

        public TransactionService(ILogger logger)
        {
            URL_API_DEPOSIT = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/DepositTransaction";
            URL_API_WITHDRAW = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/WithdrawTransaction";

            _logger = logger;
        }

        public async Task<IResponseContent> Deposit(string tenantUid, string token, string origin, Deposit deposit) {

            var customerGuid = DecodeJwt(token).CustomerGuid;
            if(deposit.Parameters != null)
            {
                deposit.CustomerGuid = customerGuid.ToString();
            }

            var response = await SubmitPostAsync(URL_API_DEPOSIT, origin, deposit, token, tenantUid, _logger);
            var responseContent = AssertResponseContent<DepositPerfectMoneyResponseContent>(response);

            return responseContent;
        }

        public async Task<IResponseContent> DepositBitcoin(string tenantUid, string token, string origin, Deposit deposit)
        {
            var customerGuid = DecodeJwt(token).CustomerGuid;
            if (deposit.Parameters != null)
            {
                deposit.CustomerGuid = customerGuid.ToString();
            }

            var response = await SubmitPostAsync(URL_API_DEPOSIT, origin, deposit, token, tenantUid, _logger);
            var responseContent = AssertResponseContent<DepositBitcoinResponseContent>(response);

            return responseContent;
        }

        public async Task<IResponseContent> DepositPerfectMoney(string tenantUid, string token, string origin, Deposit deposit)
        {
            var customerGuid = DecodeJwt(token).CustomerGuid;
            if (deposit.Parameters != null)
            {
                deposit.CustomerGuid = customerGuid.ToString();
            }

            var response = await SubmitPostAsync(URL_API_DEPOSIT, origin, deposit, token, tenantUid, _logger);
            var responseContent = AssertResponseContent<DepositPerfectMoneyResponseContent>(response);

            return responseContent;
        }

        public async Task<IResponseContent> Withdraw(string tenantUid, string token, string origin, Withdraw withdraw)
        {
            var customerGuid = DecodeJwt(token).CustomerGuid;
            if (withdraw.Parameters != null)
            {
                withdraw.CustomerGuid = customerGuid.ToString();
            }

            var response = await SubmitPostAsync(URL_API_WITHDRAW, origin, withdraw, token, tenantUid);
            var responseContent = AssertResponseContent<WithdrawResponseContent>(response);

            return responseContent;
        }

        public async Task<IResponseContent> WithdrawBitcoin(string tenantUid, string token, string origin, WithdrawBitcoin withdrawBitcoin)
        {
            var response = await SubmitPostAsync(URL_API_WITHDRAW, origin, withdrawBitcoin, token, tenantUid);
            var responseContent = AssertResponseContent<WithdrawResponseContent>(response);

            return responseContent;
        }

        private new IResponseContent AssertResponseContent<T>(IRestResponse response) where T : IResponseContent
        {
            if (response.ContentType.Contains("\"errors\":{"))
            {
                var instance = Activator.CreateInstance<T>();
                instance.Message = response.Content;
                instance.Exception = new Exception(response.Content, response.ErrorMessage != null ? new Exception(response.ErrorMessage, response.ErrorException) : null);
                return instance;
            }
            else
            {
                _logger.Info<TransactionService>($"Response Content: {response.Content}");
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
        }
    }
}
