namespace TotalCode.Core.Controllers.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web;

    public class TotalCodeGenericPageController : BasePageController
    {
        private readonly TotalCodeApiService apiService;
        private readonly IContentService contentService;
        public TotalCodeGenericPageController(IContentService contentService)
        {
            this.contentService = contentService;
            this.apiService = new TotalCodeApiService();
            using (var scope = ConnectorContext.ScopeProvider.CreateScope(autoComplete: true))
            {
                ApiKeyCache.UpdateCache(scope.Database);
            }
        }

        [Obsolete]
        public ActionResult TotalCodeCasinoPageTemplate()
        {
            var model = GetModel<GamePageViewModel>(CurrentPage);

            if (Request.QueryString["gameId"] != null)
            {
                var origin = TenantHelper.GetCurrentTenantUrl(contentService, model.TenantUid);
                var gameId = int.Parse(Request.QueryString["gameId"].ToString());
                var key = ApiKeyCache.GetByTenantUid(model.TenantUid);
                var authorization = GetAuthorization(key);
                var games = apiService.GetGameDataAnonymousArray(model.TenantUid, origin, authorization: authorization.AccessToken);
                var game = games.SingleOrDefault(x => x.GameId == gameId);
                if (game != null)
                {
                    model.HasDemoMode = game.DemoEnabled;
                    model.GameUrl = game.Url;
                }
            }
            else
            {

                model.Slider = CurrentPage.Value<IEnumerable<IPublishedElement>>("gameSlider")
                        .Select(x => new SliderItem
                        {
                            Image = x.HasValue("sliderItemImage") ? x.GetProperty("sliderItemImage").Value<IPublishedContent>().Url : string.Empty,
                            ButtonLabel = x.HasValue("sliderItemButtonLabel") ? x.GetProperty("sliderItemButtonLabel").GetValue().ToString() : string.Empty,
                            Title = x.HasValue("sliderItemTitle") ? x.GetProperty("sliderItemTitle").GetValue().ToString() : string.Empty,
                            Subtitle = x.HasValue("sliderItemSubtitle") ? x.GetProperty("sliderItemSubtitle").GetValue().ToString() : string.Empty,
                            Url = x.HasValue("sliderItemUrl") ? x.GetProperty("sliderItemUrl").GetValue().ToString() : string.Empty,
                        })?.ToList();
            }

            model.Category = "casino";
            return CurrentTemplate(model);
        }

        public ActionResult TotalCodeSportsFeedPageTemplate()
        {
            var model = GetModel<GenericPageViewModel>(CurrentPage);
            var url_absolute = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var url = string.Empty;

            if (!String.IsNullOrEmpty(model.Subdomain))
            {
                var domain = url_absolute.Authority;
                int count = domain.Count(f => f == '.');
                if (count == 1)
                {
                    url = model.Subdomain + "." + domain;
                }
                if (count >= 2)
                {
                    string output = domain.Substring(domain.IndexOf('.') + 1);
                    url = model.Subdomain + "." + output;
                }
                if (domain.Contains(".co.uk"))
                {
                    if (count == 2)
                    {
                        url = model.Subdomain + "." + domain;
                    }
                    if (count >= 3)
                    {
                        string output = domain.Substring(domain.IndexOf('.') + 1);
                        url = model.Subdomain + "." + output;
                    }
                }
            }

            var gamesHelper = new GamesHelper(contentService, apiService);
            var gameDetails = gamesHelper.AssertGameType(model);
            model.Category = gameDetails.Item1;
            model.View = gameDetails.Item2;

            var homePage = CurrentPage.Root();
            var colorString = String.Empty;
            colorString += homePage.HasValue("sbColor1") ? "&color1=" + homePage.Value<string>("sbColor1") : string.Empty;
            colorString += homePage.HasValue("sbColor2") ? "&color2=" + homePage.Value<string>("sbColor2") : string.Empty;
            colorString += homePage.HasValue("sbColor3") ? "&color3=" + homePage.Value<string>("sbColor3") : string.Empty;
            colorString += homePage.HasValue("sbColor4") ? "&color4=" + homePage.Value<string>("sbColor4") : string.Empty;
            colorString += homePage.HasValue("sbColor5") ? "&color5=" + homePage.Value<string>("sbColor5") : string.Empty;
            colorString += homePage.HasValue("sbColor6") ? "&color6=" + homePage.Value<string>("sbColor6") : string.Empty;
            colorString += homePage.HasValue("sbColor7") ? "&color7=" + homePage.Value<string>("sbColor7") : string.Empty;
            colorString += homePage.HasValue("sbColor8") ? "&color8=" + homePage.Value<string>("sbColor8") : string.Empty;
            colorString += homePage.HasValue("sbColor9") ? "&color9=" + homePage.Value<string>("sbColor9") : string.Empty;
            model.SBColor = colorString.Replace("#", "");

            model.ServiceUrl = "sportsbook-api-" + url;
            model.IframeUrl = string.Format(IframeUrls.SportIframeUrl, url);
            return CurrentTemplate(model);
        }

        public ActionResult TotalCodeBettingHistoryPageTemplate()
        {
            var model = GetModel<GenericPageViewModel>(CurrentPage);
            var url_absolute = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var url = string.Empty;

            if (!String.IsNullOrEmpty(model.Subdomain))
            {
                var domain = url_absolute.Authority;
                int count = domain.Count(f => f == '.');
                if (count == 1)
                {
                    url = model.Subdomain + "." + domain;
                }
                if (count >= 2)
                {
                    string output = domain.Substring(domain.IndexOf('.') + 1);
                    url = model.Subdomain + "." + output;
                }
                if (domain.Contains(".co.uk"))
                {
                    if (count == 2)
                    {
                        url = model.Subdomain + "." + domain;
                    }
                    if (count >= 3)
                    {
                        string output = domain.Substring(domain.IndexOf('.') + 1);
                        url = model.Subdomain + "." + output;
                    }
                }
            }

            model.ServiceUrl = "sportsbook-api-" + url;
            model.IframeUrl = string.Format(IframeUrls.SportIframeUrl, url);
            return CurrentTemplate(model);
        }

        public ActionResult TotalCodeGenericPageTemplate()
        {
            var model = GetModel<GenericPageViewModel>(CurrentPage);

            model.Title = model.Content.Name;
            var iframeUrl = string.Empty;

            if (model.Content.GetProperty("gameType").HasValue())
            {
                GamePageType gameType = (GamePageType)Enum.Parse(typeof(GamePageType), model.Content.GetProperty("gameType").GetValue().ToString());
                switch (gameType)
                {
                    case GamePageType.InPlay:
                        iframeUrl = IframeUrls.InPlayIframeUrl;
                        break;
                    case GamePageType.Casino:
                        iframeUrl = IframeUrls.CasinoIframeUrl;
                        break;
                    case GamePageType.LiveCasino:
                        iframeUrl = IframeUrls.LiveCasinoIframeUrl;
                        break;
                    case GamePageType.Poker:
                        iframeUrl = IframeUrls.PokerframeUrl;
                        break;
                    case GamePageType.Lottery:
                        iframeUrl = IframeUrls.LotteryIframeUrl;
                        break;
                    case GamePageType.Vegas:
                        iframeUrl = IframeUrls.VegasIframeUrl;
                        break;
                    case GamePageType.Sport:
                        var url = $"{model.Subdomain}.{model.Domain}";
                        iframeUrl = string.Format(IframeUrls.SportIframeUrl, url);
                        break;
                    case GamePageType.Board:
                        iframeUrl = IframeUrls.BoardIframeUrl;
                        break;
                    default:
                        iframeUrl = "GameTypeNotDefined";
                        break;
                }
            }
            model.IframeUrl = iframeUrl;

            return CurrentTemplate(model);
        }

        public ActionResult TotalCodeGamePageTemplate()
        {
            var model = GetModel<GamePageViewModel>(CurrentPage);
            var gamesHelper = new GamesHelper(contentService, apiService);
            model = gamesHelper.Model<GamePageViewModel>(model, CurrentPage, Request);
            return CurrentTemplate(model);
        }

        #region Helpers
        private ApiKeyLoginResponseContent GetAuthorization(ApiKeys key)
        {
            DateTime now = DateTime.UtcNow;
            ApiKeyLoginResponseContent response = (ApiKeyLoginResponseContent)Session["authorization"];

            if (response == null || response.Expires <= now)
            {
                response = (ApiKeyLoginResponseContent)apiService.ApiKeyLogin(key.ApiKey, key.AppId);
                Session["authorization"] = (ApiKeyLoginResponseContent)response;
            }
            return response;
        }
        #endregion

        public static int CountStringOccurrences(string text, string pattern)
        {
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }
    }
}
