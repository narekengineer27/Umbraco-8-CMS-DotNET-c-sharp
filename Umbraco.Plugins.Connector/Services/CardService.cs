namespace Umbraco.Plugins.Connector.Services
{
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Core.Logging;
    public class CardService : BaseService, ICardService
    {
        private string URL_API_ADD_CARD;
        private string URL_API_ADD_IBAN;
        private string URL_API_EDIT_CARD;
        private string URL_API_ACTIVE_CARDS;
        private string URL_API_GET_CARDS;
        private string URL_API_DELETE_CARD;

        private readonly ILogger _logger;

        public CardService(ILogger logger)
        {
            _logger = logger;

            URL_API_ADD_CARD = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/Card/add-card";
            URL_API_ADD_IBAN = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/Card/add-iban";
            URL_API_EDIT_CARD = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/Card/add-card";
            URL_API_ACTIVE_CARDS = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/Card/active-cards";
            URL_API_GET_CARDS = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/Card/get-cards";
            URL_API_DELETE_CARD = URL_FINANCIAL_MANAGEMENT_DOMAIN + "/api/Card/delete-card";
        }

        public async Task<IResponseContent> AddCard(string tenantUid, string token, string origin, AddCard card)
        {
            var customerGuid = DecodeJwt(token).CustomerGuid;
            card.CustomerGuid = customerGuid.ToString();

            var response = await SubmitPostAsync(URL_API_ADD_CARD, origin, card, token, tenantUid);
            var responseContent = AssertResponseContent<AddCardResponseContent>(response);

            return responseContent;
        }

        public async Task<IResponseContent> AddIban(string tenantUid, string token, string origin, AddIban card)
        {
            var customerGuid = DecodeJwt(token).CustomerGuid;
            card.CustomerGuid = customerGuid.ToString();

            var response = await SubmitPostAsync(URL_API_ADD_IBAN, origin, card, token, tenantUid);
            var responseContent = AssertResponseContent<AddCardResponseContent>(response);

            return responseContent;
        }

        public async Task<IResponseContent> UpdateCard(string tenantUid, string token, string origin, UpdateCard card)
        {
            var customerGuid = DecodeJwt(token).CustomerGuid;

            card.CustomerGuid = customerGuid.ToString();
            card.Response = new UpdateCard.UpdateCardResponse()
            {
                Action = "Approve",
                Comment = "approved by dilek",
                OperationUserId = 0
            };

            var response = await SubmitPostAsync(URL_API_EDIT_CARD, origin, card, token, tenantUid);
            var responseContent = AssertResponseContent<UpdateCardResponseContent>(response);

            return responseContent;
        }

        public async Task<IResponseContent> DeleteCard(string tenantUid, string token, string origin, string cardNumber)
        {
            var customerGuid = DecodeJwt(token).CustomerGuid;

            var deleteCard = new DeleteCard
            {
                CardNumber = cardNumber,
                CustomerGuid = customerGuid.ToString()
            };

            var response = await SubmitPostAsync(URL_API_DELETE_CARD, origin, deleteCard, authorization: token, tenantUid: tenantUid);
            var responseContent = AssertResponseContent<DeleteCardResponseContent>(response);

            return responseContent;
        }


        public async Task<IResponseContent> ActiveCards(string tenantUid, string token, string origin, string action = "")
        {

            var customerGuid = DecodeJwt(token).CustomerGuid;

            if (String.IsNullOrEmpty(action))
            {
                var emptyAction = new CustomerAccountBalance
                {
                    CustomerGuid = customerGuid.ToString(),
                };
                var emptyActionResponse = await SubmitPostAsync($"{URL_API_ACTIVE_CARDS}", origin, emptyAction, token, tenantUid, _logger);
                return AssertResponseContent<ActiveCardResponseContent>(emptyActionResponse);
            }

            var activeCard = new ActiveCardParams
            {
                CustomerGuid = customerGuid.ToString(),
                Action = action
            };

            var response = await SubmitPostAsync($"{URL_API_ACTIVE_CARDS}", origin, activeCard, token, tenantUid, _logger);
            return AssertResponseContent<ActiveCardResponseContent>(response);
        }

        public async Task<IResponseContent> GetCards(string tenantUid, string token, string origin)
        {

            var customerGuid = DecodeJwt(token).CustomerGuid;

            var response = await SubmitGetAsync($"{URL_API_GET_CARDS}/{customerGuid}", origin, null, token, tenantUid, _logger);
            return AssertResponseContent<GetCardResponseContent>(response);
        }


        private new IResponseContent AssertResponseContent<T>(IRestResponse response) where T : IResponseContent
        {
            if (response.ContentType.Contains("\"errors\":{") || response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var instance = Activator.CreateInstance<T>();
                instance.Message = response.Content;
                instance.Exception = new Exception(response.Content, response.ErrorMessage != null ? new Exception(response.ErrorMessage, response.ErrorException) : null);
                return instance;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
        }
    }
}
