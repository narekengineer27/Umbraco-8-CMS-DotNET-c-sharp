namespace Umbraco.Plugins.Connector.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Web;
    public class TransactionController : BaseController
    {

        private readonly ITransactionService _transactionService;
        
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<JsonResult> Deposit(Deposit deposit, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            if(deposit.PaymentIdentifier == "BTC")
            {
                var response = (DepositBitcoinResponseContent)await _transactionService.DepositBitcoin(tenantUid, token, origin, deposit);
                return Json(response);
            }
            else
            {
                var response = (DepositPerfectMoneyResponseContent)await _transactionService.Deposit(tenantUid, token, origin, deposit);

                string value;
                var hasValue = deposit.Parameters.TryGetValue("Amount", out value);
                if (hasValue)
                {
                    Session["t_amount"] = value;
                }
                Session["t_name"] = deposit.PaymentSystemName;
                Session["t_id"] = response?.Payload?.TransactionId;
                Session["t_date"] = DateTime.Now.ToString();

                return Json(response);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Withdraw(Withdraw withdraw, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = (WithdrawResponseContent)await _transactionService.Withdraw(tenantUid, token, origin, withdraw);

            string value;
            var hasValue = withdraw.Parameters.TryGetValue("Amount", out value);
            if (hasValue)
            {
                Session["t_amount"] = value;
            }
            Session["t_name"] = withdraw.PaymentSystemName;
            Session["t_id"] = response?.Payload?.TransactionId;
            Session["t_date"] = DateTime.Now.ToString();

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> WithdrawBitcoin(WithdrawBitcoin withdrawBitcoin, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = (WithdrawResponseContent)await _transactionService.WithdrawBitcoin(tenantUid, token, origin, withdrawBitcoin);

            return Json(response);
        }
    }
}
