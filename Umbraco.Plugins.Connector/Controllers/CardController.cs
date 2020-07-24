namespace Umbraco.Plugins.Connector.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Umbraco.Core.Cache;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    public class CardController : BaseController
    {
        private readonly ICardService _cardService;
        private readonly IContentService _contentService;

        public CardController(ICardService cardService, IConsentService consentService)
        {
            _cardService = cardService;
            _contentService = contentService;
        }

        [HttpPost]
        public async Task<JsonResult> AddCard(AddCard card, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = await _cardService.AddCard(tenantUid, token, origin, card);

            AppCaches _appCaches = new AppCaches();
            var cacheName = "CustomerActiveCards" + $"_tenantUid:{tenantUid}_token:{token}_origin:{origin}";
            _appCaches.RuntimeCache.ClearByKey(cacheName);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> AddIban(AddIban card, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = await _cardService.AddIban(tenantUid, token, origin, card);

            AppCaches _appCaches = new AppCaches();
            var cacheName = "CustomerActiveCards" + $"_tenantUid:{tenantUid}_token:{token}_origin:{origin}";
            _appCaches.RuntimeCache.ClearByKey(cacheName);

            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> ActiveCards(string tenantUid, string pageId = "")
        {
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
            var token = Request.Cookies["token"].Value;
            var currentPage = Umbraco.Content(pageId);
            string action = string.Empty;
            if (currentPage != null && currentPage.ContentType.Alias == "totalCodeDepositPage")
            {
                action = "Deposit";
            }
            if (currentPage != null && currentPage.ContentType.Alias == "totalCodeWithdrawPage")
            {
                action = "Withdraw";
            }

            AppCaches _appCaches = new AppCaches();
            var cacheName = "CustomerActiveCards" + $"_tenantUid:{tenantUid}_token:{token}_origin:{origin}_action:{action}";
            var cacheData = _appCaches.RuntimeCache.GetCacheItem<ActiveCardResponseContent>(cacheName);

            if (cacheData == null)
            {
                var response = (ActiveCardResponseContent)await _cardService.ActiveCards(tenantUid, token, origin, action);
                foreach (var card in response.Payload.ActiveCards)
                {
                    if (currentPage != null && currentPage.ContentType.Alias == "totalCodeWithdrawPage" && !string.IsNullOrEmpty(card.Iban))
                    {
                        if (card.Iban.Length > 8)
                        {
                            var first4 = card.Iban.Substring(0, 4);
                            var x = card.Iban.Length - 8;
                            var xs = string.Empty;
                            for (int i = 0; i < x; i++)
                            {
                                xs += "X";
                            }
                            var last4 = card.Iban.Substring(card.Iban.Length - 4);
                            card.Iban = first4 + xs + last4;
                        }
                    }
                }
                _appCaches.RuntimeCache.InsertCacheItem<ActiveCardResponseContent>(cacheName, () => { return response; });

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(cacheData, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetCards(string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = await _cardService.GetCards(tenantUid, token, origin);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateCard(UpdateCard card, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = await _cardService.UpdateCard(tenantUid, token, origin, card);

            AppCaches _appCaches = new AppCaches();
            var cacheName = "CustomerActiveCards" + $"_tenantUid:{tenantUid}_token:{token}_origin:{origin}";
            _appCaches.RuntimeCache.ClearByKey(cacheName);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCard(string cardNumber, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = await _cardService.DeleteCard(tenantUid, token, origin, cardNumber);

            AppCaches _appCaches = new AppCaches();
            var cacheName = "CustomerActiveCards" + $"_tenantUid:{tenantUid}_token:{token}_origin:{origin}";
            _appCaches.RuntimeCache.ClearByKey(cacheName);

            return Json(response);
        }
    }
}
