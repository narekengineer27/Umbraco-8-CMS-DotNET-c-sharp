namespace Umbraco.Plugins.Connector.Interfaces
{
    using System.Threading.Tasks;
    using Umbraco.Plugins.Connector.Models;
    public interface ITransactionService
    {
        Task<IResponseContent> Deposit(string tenantUid, string token, string origin, Deposit deposit);
        Task<IResponseContent> Withdraw(string tenantUid, string token, string origin, Withdraw deposit);
        Task<IResponseContent> WithdrawBitcoin(string tenantUid, string token, string origin, WithdrawBitcoin withdrawBitcoin);
        Task<IResponseContent> DepositBitcoin(string tenantUid, string token, string origin, Deposit deposit);
        Task<IResponseContent> DepositPerfectMoney(string tenantUid, string token, string origin, Deposit deposit);
    }
}
