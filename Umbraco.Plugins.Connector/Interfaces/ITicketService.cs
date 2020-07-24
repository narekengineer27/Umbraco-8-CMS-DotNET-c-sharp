namespace Umbraco.Plugins.Connector.Interfaces
{
    using System.Threading.Tasks;
    using Umbraco.Plugins.Connector.Models;
    public interface ITicketService
    {
        Task<IResponseContent> GetTicket(string tenantUid, string token, string origin, int ticketId);

        Task<IResponseContent> GetTickets(string tenantUid, string token, string origin);

        Task<IResponseContent> CreateTicket(string tenantUid, string token, string origin, CreateTicketModel ticket);

        Task<IResponseContent> CreateTicketAnonymous(string tenantUid, string token, string origin, CreateTicketAnonymousModel ticket);

        Task<IResponseContent> CloseTicket(string tenantUid, string token, string origin, CloseTicketModel ticket);

        Task<IResponseContent> CreateMessage(string tenantUid, string token, string origin, int ticketId, string email, string messageText, string attachment);
    }
}