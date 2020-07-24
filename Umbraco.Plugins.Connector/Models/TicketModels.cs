using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Plugins.Connector.Interfaces;

namespace Umbraco.Plugins.Connector.Models
{
    public class SearchTicketModel : IParameter
    {
        public IEnumerable<Ticket> Result { get; set; }
    }

    public class CreateTicketModel
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "attachment")]
        public string Attachment { get; set; }
    }

    public class CreateTicketAnonymousModel
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "attachment")]
        public string Attachment { get; set; }

        [JsonProperty(PropertyName = "EmailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }
    }

    public class CloseTicketModel
    {
        [JsonProperty(PropertyName = "ticketId")]
        public int TicketId { get; set; }

        [JsonProperty(PropertyName = "closeMessage")]
        public string CloseMessage { get; set; }
    }

    public class CreateMessageModel
    {
        [JsonProperty(PropertyName = "ticketId")]
        public int TicketId { get; set; }

        [JsonProperty(PropertyName = "ticketResponseType")]
        public string TicketResponseType { get; set; }

        [JsonProperty(PropertyName = "messageText")]
        public string MessageText { get; set; }

        [JsonProperty(PropertyName = "attachment")]
        public string Attachment { get; set; }

        [JsonProperty(PropertyName = "emailAddress")]
        public string EmailAddress { get; set; }
    }

    public class SearchTicketsModel
    {
        [JsonProperty(PropertyName = "term")]
        public string Term { get; set; }

        [JsonProperty(PropertyName = "statuses")]
        public string[] Statuses { get; set; }

        [JsonProperty(PropertyName = "expanded")]
        public bool Expanded { get; set; }

        [JsonProperty(PropertyName = "ticketId")]
        public int TicketId { get; set; }
    }

    public class GetTicketModel
    {
        [JsonProperty(PropertyName = "ticketId")]
        public int TicketId { get; set; }
    }

    public class TicketSearchResponseContent : IResponseContent
    {
        public int Id { get; set; }
        public string JsonRpc { get; set; }
        public List<TicketResult> Result { get;set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
    }

    public class TicketResponseContent : IResponseContent
    {
        public int Id { get; set; }
        public string JsonRpc { get; set; }
        public Ticket Result { get; set; }
        public Error Error { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
    }

    public class TicketAnonymousResponseContent: IResponseContent
    {
        public int Id { get; set; }
        public string JsonRpc { get; set; }
        public TicketResult Result { get; set; }
        public Error Error { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
    }

    public class TicketMessageResponseContent : IResponseContent
    {
        public int Id { get; set; }
        public string JsonRpc { get; set; }
        public TicketResponseMessage Result { get; set; }
        public Error Error { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
    }

    public class TicketFileResponseContent : IResponseContent
    {
        public string FileUrl { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
    }

    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

        /* additional redundant properties,
         * remote API has no uniform return data */

        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
