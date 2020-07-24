namespace Umbraco.Plugins.Connector.Interfaces
{
    using System.Threading.Tasks;
    using Umbraco.Plugins.Connector.Models;
    public interface ITransactionHistoryService
    {
        Task<IResponseContent> DepositTransaction(string tenantUid, string token, string origin, DepositTransaction depositTransaction);
        Task<IResponseContent> WithdrawTransaction(string tenantUid, string token, string origin, WithdrawTransaction withdrawTransaction);
        Task<IResponseContent> CancelWithdrawal(string tenantUid, string token, string origin, string transactionGuid);
        Task<IResponseContent> BonusTransaction(string tenantUid, string token, string origin, BonusTransaction bonusTransaction);
    }
}
