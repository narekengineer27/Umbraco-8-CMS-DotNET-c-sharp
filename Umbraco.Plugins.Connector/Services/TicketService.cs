using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;
using Umbraco.Plugins.Connector.Interfaces;
using Umbraco.Plugins.Connector.Models;

namespace Umbraco.Plugins.Connector.Services
{
    public class TicketService : BaseService, ITicketService
    {

        private string URL_API_TICKET;

        public TicketService()
        {
            URL_API_TICKET = URL_HELPDESK_MANAGEMENT_DOMAIN + "/api/ticket";
        }

        public async Task<IResponseContent> GetTicket(string tenantUid, string token, string origin, int ticketId)
        {
            var payload = new JsonRpcFormat<JsonRpcFormatParamsForGetDetails>()
            {
                Method = "GetDetails",
                Params = new JsonRpcFormatParamsForGetDetails
                {
                    TicketId = ticketId
                }
            };

            var response = await SubmitPostAsync(URL_API_TICKET, token, origin, payload, tenantUid);
            return await AssertResponseContent<TicketResponseContent>(response);
        }

        public async Task<IResponseContent> GetTickets(string tenantUid, string token, string origin)
        {
            var payload = new JsonRpcFormat<JsonRpcFormatParams<SearchTicketsModel>>()
            {
                Method = "Search",
                Params = new JsonRpcFormatParams<SearchTicketsModel>()
                {
                    Model = new SearchTicketsModel()
                    {
                        Term = "",
                        Statuses = new string[] { },
                        Expanded = true,
                        TicketId = 0
                    }
                }
            };

            var response = await SubmitPostAsync(URL_API_TICKET, token, origin, payload, tenantUid);
            return await AssertResponseContent<TicketSearchResponseContent>(response);
        }

        public async Task<IResponseContent> CreateTicket(string tenantUid, string token, string origin, CreateTicketModel ticket)
        {
            var payload = new JsonRpcFormat<JsonRpcFormatParams<CreateTicketModel>>()
            {
                Method = "CreateTicket",
                Params = new JsonRpcFormatParams<CreateTicketModel>()
                {
                    Model = ticket
                }
            };

            var response = await SubmitPostAsync(URL_API_TICKET, token, origin, payload, tenantUid);
            return await AssertResponseContent<TicketResponseContent>(response);
        }

        public async Task<IResponseContent> CreateTicketAnonymous(string tenantUid, string token, string origin, CreateTicketAnonymousModel ticket)
        {
            var payload = new JsonRpcFormat<JsonRpcFormatParams<CreateTicketAnonymousModel>>()
            {
                Method = "CreateTicketAnonymous",
                Params = new JsonRpcFormatParams<CreateTicketAnonymousModel>()
                {
                    Model = ticket
                }
            };

            var response = await SubmitPostAsync(URL_API_TICKET, token, origin, payload, tenantUid);
            return await AssertResponseContent<TicketAnonymousResponseContent>(response);
        }

        public async Task<IResponseContent> CloseTicket(string tenantUid, string token, string origin, CloseTicketModel ticket)
        {
            var payload = new JsonRpcFormat<JsonRpcFormatParams<CloseTicketModel>>()
            {
                Method = "CloseTicket",
                Params = new JsonRpcFormatParams<CloseTicketModel>()
                {
                    Model = ticket
                }
            };

            var response = await SubmitPostAsync(URL_API_TICKET, token, origin, payload, tenantUid);
            return await AssertResponseContent<TicketResponseContent>(response);
        }

        public async Task<IResponseContent> CreateMessage(string tenantUid, string token, string origin, int ticketId, string email, string messageText, string attachment)
        {
            var payload = new JsonRpcFormat<JsonRpcFormatParams<CreateMessageModel>>()
            {
                Method = "CreateTicketResponseMessageForCustomer",
                Params = new JsonRpcFormatParams<CreateMessageModel>()
                {
                    Model = new CreateMessageModel
                    {
                        TicketId = ticketId,
                        MessageText = messageText,
                        EmailAddress = email,
                        TicketResponseType = "Response",
                        Attachment = attachment
                    }
                }
            };
            
            var response = await SubmitPostAsync(URL_API_TICKET, token, origin, payload, tenantUid);
            return await AssertResponseContent<TicketMessageResponseContent>(response);
        }

        private new async Task<IResponseContent> AssertResponseContent<T>(IRestResponse response) where T : IResponseContent
        {
            if (response.ContentType.Equals("application/json"))
            {
                return await Task.FromResult(JsonConvert.DeserializeObject<T>(response.Content));
            }

            else
            {
                var instance = Activator.CreateInstance<T>();
                instance.Message = response.Content;
                instance.Exception = new Exception(response.Content, response.ErrorMessage != null ? new Exception(response.ErrorMessage, response.ErrorException) : null);
                return instance;
            }
        }
    }
}
