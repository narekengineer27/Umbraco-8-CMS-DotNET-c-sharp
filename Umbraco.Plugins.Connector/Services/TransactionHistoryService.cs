using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Plugins.Connector.Interfaces;
using Umbraco.Plugins.Connector.Models;

namespace Umbraco.Plugins.Connector.Services
{
    public class TransactionHistoryService : BaseService, ITransactionHistoryService
    {

        private string URL_API_DEPOSIT_TRANSACTION;
        private string URL_API_WITHDRAW_TRANSACTION;
        private string URL_API_CANCEL_WITHDRAWAL_TRANSACTION;
        private string URL_API_BONUS_TRANSACTION;

        public TransactionHistoryService()
        {
            URL_API_DEPOSIT_TRANSACTION = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/DepositTransaction/get-deposit-transactions";
            URL_API_WITHDRAW_TRANSACTION = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/WithdrawTransaction/get-withdrawal-transactions";
            URL_API_CANCEL_WITHDRAWAL_TRANSACTION = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/WithdrawTransaction/cancel-withdrawal/";
            URL_API_BONUS_TRANSACTION = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/bonushistory/get-bonus-history";
        }

        public async Task<IResponseContent> DepositTransaction(string tenantUid, string token, string origin, DepositTransaction depositTransaction)
        {

            var customerGuid = DecodeJwt(token).CustomerGuid;
            depositTransaction.CustomerGuid = customerGuid;
            
            var response = await SubmitPostAsync(URL_API_DEPOSIT_TRANSACTION, origin, depositTransaction, token, tenantUid);
            var responseContent = AssertResponseContent<DepositTransactionResponseContent>(response);

            return responseContent;
        }

        public async Task<IResponseContent> WithdrawTransaction(string tenantUid, string token, string origin, WithdrawTransaction withdrawTransaction)
        {

            var customerGuid = DecodeJwt(token).CustomerGuid;
            withdrawTransaction.CustomerGuid = customerGuid;

            var response = await SubmitPostAsync(URL_API_WITHDRAW_TRANSACTION, origin, withdrawTransaction, token, tenantUid);
            var responseContent = AssertResponseContent<WithdrawTransactionResponseContent>(response);

            return responseContent;
        }

        public async Task<IResponseContent> CancelWithdrawal(string tenantUid, string token, string origin, string transactionGuid)
        {
            var response = await SubmitPostAsync($"{URL_API_CANCEL_WITHDRAWAL_TRANSACTION}/{transactionGuid}", origin, null, token, tenantUid);
            var responseContent = AssertResponseContent<CancelWithdrawalResponseContent>(response);

            return responseContent;
        }

        public async Task<IResponseContent> BonusTransaction(string tenantUid, string token, string origin, BonusTransaction bonusTransaction)
        {
            var customerGuid = DecodeJwt(token).CustomerGuid;
            bonusTransaction.CustomerGuid = customerGuid;

            var response = await SubmitPostAsync(URL_API_BONUS_TRANSACTION, origin, bonusTransaction, token, tenantUid);
            var responseContent = AssertResponseContent<BonusTransactionResponseContent>(response);

            return responseContent;
        }
    }
}
