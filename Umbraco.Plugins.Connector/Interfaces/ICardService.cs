namespace Umbraco.Plugins.Connector.Interfaces
{
    using System.Threading.Tasks;
    using Umbraco.Plugins.Connector.Models;
    public interface ICardService
    {
        Task<IResponseContent> AddCard(string tenantUid, string token, string origin, AddCard card);
        Task<IResponseContent> AddIban(string tenantUid, string token, string origin, AddIban card);
        Task<IResponseContent> UpdateCard(string tenantUid, string token, string origin, UpdateCard card);
        Task<IResponseContent> DeleteCard(string tenantUid, string token, string origin, string cardNumber);
        Task<IResponseContent> ActiveCards(string tenantUid, string token, string origin, string action = "");
        Task<IResponseContent> GetCards(string tenantUid, string token, string origin);
    }
}
