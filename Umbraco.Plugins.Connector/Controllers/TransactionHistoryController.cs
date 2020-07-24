namespace Umbraco.Plugins.Connector.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    public class TransactionHistoryController : BaseController
    {
        private readonly ITransactionHistoryService _transactionHistoryService;

        public TransactionHistoryController(ITransactionHistoryService transactionHistoryService)
        {
            _transactionHistoryService = transactionHistoryService;
        }

        [HttpPost]
        public async Task<JsonResult> DepositTransaction(DepositTransaction depositTransaction, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = (DepositTransactionResponseContent)await _transactionHistoryService.DepositTransaction(tenantUid, token, origin, depositTransaction);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> WithdrawTransaction(WithdrawTransaction withdrawTransaction, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = (WithdrawTransactionResponseContent)await _transactionHistoryService.WithdrawTransaction(tenantUid, token, origin, withdrawTransaction);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> CancelWithdrawal(string transactionGuid, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = (CancelWithdrawalResponseContent)await _transactionHistoryService.CancelWithdrawal(tenantUid, token, origin, transactionGuid);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> BonusTransaction(BonusTransaction bonusTransaction, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = (BonusTransactionResponseContent)await _transactionHistoryService.BonusTransaction(tenantUid, token, origin, bonusTransaction);

            return Json(response);
        }
    }
}
