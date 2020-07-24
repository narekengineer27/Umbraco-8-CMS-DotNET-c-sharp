namespace TotalCode.Core.Controllers.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    public class TotalCodeDepositBankPageController : BasePageController
    {
        private readonly ICardService _cardService;
        private readonly IContentService _contentService;


        public TotalCodeDepositBankPageController(ICardService cardService, IContentService contentService)
        {
            _cardService = cardService;
            _contentService = contentService;
        }

        public async Task<ActionResult> Index()
        {
            var model = GetModel<DepositBankPageViewModel>(CurrentPage);

            if (string.IsNullOrEmpty(model.Token))
            {
                return Redirect("/?login=y");
            }

            List<Card> cards = new List<Card>();

            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, model.TenantUid);
            var activeCardsResponse = (ActiveCardResponseContent)await _cardService.ActiveCards(model.TenantUid, model.Token, origin, "Deposit");
            var activeCards = activeCardsResponse?.Payload?.ActiveCards.ToList();

            foreach (var activeCard in activeCards)
            {
                activeCard.Status = "Active";
                if (!string.IsNullOrEmpty(activeCard.Iban) && activeCard.Iban.Length > 8)
                {
                    var first4 = activeCard.Iban.Substring(0, 4);
                    var x = activeCard.Iban.Length - 8;
                    var xs = string.Empty;
                    for (int i = 0; i < x; i++)
                    {
                        xs += "X";
                    }
                    var last4 = activeCard.Iban.Substring(activeCard.Iban.Length - 4);
                    activeCard.Iban = first4 + xs + last4;
                }
            }

            cards.AddRange(activeCards);

            var allCardsResponse = (GetCardResponseContent)await _cardService.GetCards(model.TenantUid, model.Token, origin);
            var allCards = allCardsResponse?.Payload;
            if (allCards != null && allCards.Any())
            {
                foreach (var card in allCards.Where(card => card.Status != "Active"))
                {
                    cards.Add(new Card
                    {
                        CustomerGuid = card.CustomerGuid,
                        CardNumber = card.CardNumber,
                        Status = card.Status
                    });
                }
            }

            model.Cards = cards;

            return CurrentTemplate(model);
        }
    }
}
