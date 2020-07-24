namespace TotalCode.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using TotalCode.Core.Models.Pages;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web;

    public class GamesHelper
    {
        private readonly TotalCodeApiService apiService;
        private readonly IContentService contentService;
        public GamesHelper(IContentService contentService, TotalCodeApiService apiService)
        {
            this.apiService = apiService;
            this.contentService = contentService;
        }

        public T Model<T>(T model, IPublishedContent CurrentPage, HttpRequestBase Request) where T : BasePageViewModel
        {
            #region Defaults
            model.ShowProvidersFilter = true;
            model.ShowSeachFilter = true;
            model.ShowSubCategoryFilter = true;
            #endregion

            model.Title = model.Content.Name;
            var gameUrl = string.Empty;
            var category = string.Empty;
            var subCategory = string.Empty;
            var gameType = GamePageType.none;
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, model.TenantUid);
            //var key = ApiKeyCache.GetByTenantUid(model.TenantUid);
            //var authorization = GetAuthorization(key);
            var authorization = ApiAuthorizationCache.GetOrSetAuthorization(model.TenantUid);
            var returnUrl = model.IsHomePage ? model.RootUrl : $"https://{model.Subdomain}.{model.Domain}";
            //var returnUrl = model.IsHomePage ? model.RootUrl : $"https://{model.Subdomain}.{model.Domain}{model.Content.Url}";

#if DEBUG
            returnUrl = model.IsHomePage ? $"https://{model.Subdomain}.{model.Domain}" : $"https://{model.Subdomain}.{model.Domain}{model.Content.Url}";
#endif
            model.ReturnUrl = returnUrl;

            var gameDetails = AssertGameType(model);
            category = gameDetails.Item1;
            gameType = gameDetails.Item3;

            var games = apiService.GetGameDataAnonymousArray(model.TenantUid, origin, category: category, languageCode: model.CurrentLanguage.TwoLetterISOLanguageName, authorization: authorization.AccessToken);

            if (games != null)
            {
                foreach (var game in games)
                {
                    if (model.Content.Descendant() != null)
                    {
                        game.GamePageUrl = model.Content.Descendants().SingleOrDefault(x => x.HasProperty("gameType") && x.Value<string>("gameType") == game.Category)?.Url;
                    }
                }
            }
            // make sure games is not null

            games = games ?? new List<GameDetails>();

            if (Request.QueryString["gameId"] != null)
            {
                var gameId = int.Parse(Request.QueryString["gameId"].ToString());
                var gameFromList = games.SingleOrDefault(x => x.GameId == gameId);
                var game = (GameByIdResponseContent)apiService.GetGameById(model.TenantUid, origin, gameId: gameId, gameIdentifier: gameFromList.GameIdentifier, authorization: authorization.AccessToken);
                if (game != null)
                {
                    model.HasDemoMode = game.DemoEnabled;
                    model.DemoUrl = game.DemoUrl;
                    model.UrlType = game.UrlTypeEnum;
                    model.GameMode = GameMode.Choose;
                    model.GameId = int.Parse(Request.QueryString["gameId"]);
                    model.GameImage = game.GameImages.SingleOrDefault(x => x.ImageTypeEnum == ImageType.Full).Url;

                    switch (model.UrlType)
                    {
                        case UrlType.iFrame:
                            model.GameUrl = $"{game.Url}?id={model.TenantUid}&langId={model.CurrentLanguage.TwoLetterISOLanguageName}&returnUrl={HttpUtility.UrlDecode(CurrentPage.UrlAbsolute())}";
                            model.OpenPopup = false;
                            model.GameMode = GameMode.Play;
                            model.GameLoadSuccess = true;
                            break;
                        case UrlType.Session:

                            if (game.DemoEnabled)
                            {
                                model.OpenPopup = true;
                                model.GameMode = GameMode.Choose;
                            }
                            else
                            {
                                model.GameMode = GameMode.Play;
                            }

                            if (Request.QueryString["mode"] != null)
                            {
                                var mode = Request.QueryString["mode"];
                                if (mode == "demo")
                                {
                                    model.GameMode = GameMode.Demo;
                                }
                                if (mode == "play")
                                {
                                    model.GameMode = GameMode.Play;
                                }
                                model.OpenPopup = false;
                            }

                            if (game.Category == "Casino" && game.UrlType == 2 && Request.QueryString["mode"] != null)
                            {
                                game.Url += "&ReturnUrl=" + HttpUtility.UrlDecode(CurrentPage.UrlAbsolute());
                                game.DemoUrl += "&ReturnUrl=" + HttpUtility.UrlDecode(CurrentPage.UrlAbsolute());
                            }
                            if (game.Category == "Casino" && Request.QueryString["mode"] == null)
                            {
                                game.Url += "?ReturnUrl=" + HttpUtility.UrlDecode(CurrentPage.UrlAbsolute());
                                game.DemoUrl += "?ReturnUrl=" + HttpUtility.UrlDecode(CurrentPage.UrlAbsolute());
                            }

                            string sessionUrl = game.Url;
                            var gameModeUrl = game.Url;
                            gameModeUrl = model.GameMode == GameMode.Demo ? game.DemoUrl : game.Url;

                            switch (gameType)
                            {
                                case GamePageType.InPlay:
                                    break;
                                case GamePageType.Casino:
                                    var gameLobbyUrl = (GameLobbyResponseContent)apiService.GetGameLobby(gameModeUrl, model.TenantUid, origin, LoginSession.Token ?? authorization.AccessToken, model.CurrentLanguage.TwoLetterISOLanguageName, returnUrl);
                                    //var gameLobbyUrl = (GameLobbyResponseContent)apiService.GetGameLobby($"{gameModeUrl}&returnUrl={ContentHelper.EncodeUrl(returnUrl)}", model.TenantUid, origin, LoginSession.Token ?? authorization.AccessToken, model.CurrentLanguage.TwoLetterISOLanguageName, returnUrl);
                                    if (gameLobbyUrl.Success)
                                    {
                                        //if (game.GameIdentifier == "Zeppelin")
                                        //{
                                            //HttpContext.Current.Response.Redirect(gameLobbyUrl.Link);
                                        //}
                                        sessionUrl = gameLobbyUrl.Link;
                                        model.GameLoadSuccess = true;
                                    }

                                    break;
                                case GamePageType.LiveCasino:
                                    var liveCasinoLobbyUrl = (GameLobbyResponseContent)apiService.GetGameLobby(gameModeUrl, model.TenantUid, origin, LoginSession.Token ?? authorization.AccessToken, model.CurrentLanguage.TwoLetterISOLanguageName, returnUrl);
                                    if (liveCasinoLobbyUrl.Success)
                                    {
                                        sessionUrl = liveCasinoLobbyUrl.Link;
                                        model.GameLoadSuccess = true;
                                    }

                                    break;
                                case GamePageType.Poker:
                                    var pokerLobbyUrl = (PokerLobbyResponseContent)apiService.GetPokerLobby(model.TenantUid, origin, LoginSession.Token ?? authorization.AccessToken, model.CurrentLanguage.TwoLetterISOLanguageName, returnUrl);
                                    if (pokerLobbyUrl.Success)
                                    {
                                        sessionUrl = pokerLobbyUrl.RedirectUrl;
                                        model.ReturnUrl = returnUrl;
                                        model.GameLoadSuccess = true;
                                    }
                                    break;
                                case GamePageType.Lottery:
                                    var lotteryLobbyUrl = (GameLobbyResponseContent)apiService.GetGameLobby(gameModeUrl, model.TenantUid, origin, LoginSession.Token ?? authorization.AccessToken, model.CurrentLanguage.TwoLetterISOLanguageName, returnUrl);
                                    if (lotteryLobbyUrl.Success)
                                    {
                                        sessionUrl = lotteryLobbyUrl.Link;
                                        model.GameLoadSuccess = true;
                                    }

                                    break;
                                case GamePageType.Vegas:
                                    break;
                                case GamePageType.Sport:
                                    break;
                                case GamePageType.Board:
                                    var boardLobbyUrl = (GameLobbyResponseContent)apiService.GetGameLobby(gameModeUrl, model.TenantUid, origin, LoginSession.Token ?? authorization.AccessToken, model.CurrentLanguage.TwoLetterISOLanguageName, returnUrl);
                                    if (boardLobbyUrl.Success)
                                    {
                                        sessionUrl = boardLobbyUrl.Link;
                                        model.GameLoadSuccess = true;
                                    }

                                    break;
                                default:
                                    break;
                            }
                            model.GameUrl = sessionUrl;
                            break;
                        default:
                            model.GameUrl = string.Empty;
                            break;
                    }

                }
            }

            if (CurrentPage.HasProperty("gameSlider"))
            {
                model.Slider = CurrentPage.HasValue("gameSlider") ? CurrentPage.Value<IEnumerable<IPublishedElement>>("gameSlider")
                        .Select(x => new SliderItem
                        {
                            Image = x.HasValue("sliderItemImage") ? x.GetProperty("sliderItemImage").Value<IPublishedContent>().Url : string.Empty,
                            ButtonLabel = x.HasValue("sliderItemButtonLabel") ? x.GetProperty("sliderItemButtonLabel").GetValue().ToString() : string.Empty,
                            Title = x.HasValue("sliderItemTitle") ? x.GetProperty("sliderItemTitle").GetValue().ToString() : string.Empty,
                            Subtitle = x.HasValue("sliderItemSubtitle") ? x.GetProperty("sliderItemSubtitle").GetValue().ToString() : string.Empty,
                            Url = x.HasValue("sliderItemUrl") ? x.GetProperty("sliderItemUrl").GetValue().ToString() : string.Empty,
                        })?.ToList() : new List<SliderItem>();
            }

            if (CurrentPage.HasProperty("bannerSlider"))
            {
                model.Slider = CurrentPage.HasValue("bannerSlider") ? CurrentPage.Value<IEnumerable<IPublishedElement>>("bannerSlider")
                .Select(x => new SliderItem
                {
                    Image = x.Value("sliderItemImage") != null ? x.GetProperty("sliderItemImage").Value<IPublishedContent>().Url : string.Empty,
                    ButtonLabel = x.HasValue("sliderItemButtonLabel") ? x.GetProperty("sliderItemButtonLabel").GetValue().ToString() : string.Empty,
                    Title = x.HasValue("sliderItemTitle") ? x.GetProperty("sliderItemTitle").GetValue().ToString() : string.Empty,
                    Subtitle = x.HasValue("sliderItemSubtitle") ? x.GetProperty("sliderItemSubtitle").GetValue().ToString() : string.Empty,
                    Url = x.HasValue("sliderItemUrl") ? x.GetProperty("sliderItemUrl").GetValue().ToString() : string.Empty,
                })?.ToList() : new List<SliderItem>();
            }

            var menuItems = CurrentPage.Children.Where(x => x.GetProperty("pageCustomSvg") != null && x.GetProperty("pageCustomSvg").HasValue())
                    .Select(z => new MenuItem
                    {
                        Label = z.Name,
                        SvgIcon = "/img/svg/" + z.GetProperty("pageCustomSvg").GetValue().ToString() + ".svg",
                        Url = z.Url
                    });

            var orderedMenuIcons = new List<MenuItem>();
            int order = 0;
            foreach (var item in model.TopMenu)
            {
                var orderedMenuItem = menuItems.SingleOrDefault(x => x.Url.Equals(item.Url));
                if (orderedMenuItem != null)
                {
                    orderedMenuItem.Order = order;
                    orderedMenuIcons.Add(orderedMenuItem);
                    order++;
                }
            }
            model.MenuItems = orderedMenuIcons?.OrderBy(x => x.Order).ToList();

            model.Games = games;
            model.Category = category;

            var home = TenantHelper.GetCurrentTenantHome(contentService, model.TenantUid);

            model.PlayButtonText = home.GetValue<string>("playButtonText", model.CurrentLanguage.TwoLetterISOLanguageName) ?? string.Empty;
            model.DemoButtonText = home.GetValue<string>("demoButtonText", model.CurrentLanguage.TwoLetterISOLanguageName) ?? string.Empty;
            model.GameMessage = home.GetValue<string>("gameMessage", model.CurrentLanguage.TwoLetterISOLanguageName) ?? string.Empty;
            model.GameAgreeText = home.GetValue<string>("gameAgreeText", model.CurrentLanguage.TwoLetterISOLanguageName) ?? string.Empty;
            model.PageImages = home.GetValue<IEnumerable<IPublishedElement>>("demoPageImages", model.CurrentLanguage.TwoLetterISOLanguageName) ?? Enumerable.Empty<IPublishedElement>();

            return model;
        }

        public async Task<T> ModelAsync<T>(T model, IPublishedContent CurrentPage, HttpRequestBase Request) where T : BasePageViewModel
        {
            return await Task.FromResult(Model<T>(model, CurrentPage, Request));
        }

        private ApiKeyLoginResponseContent GetAuthorization(ApiKeys key)
        {
            DateTime now = DateTime.UtcNow;
            ApiKeyLoginResponseContent response = (ApiKeyLoginResponseContent)HttpContext.Current.Session["authorization"];

            if (response == null || response.Expires <= now)
            {
                response = (ApiKeyLoginResponseContent)apiService.ApiKeyLogin(key.ApiKey, key.AppId);
                HttpContext.Current.Session["authorization"] = (ApiKeyLoginResponseContent)response;
            }
            return response;
        }

        public (string, string, GamePageType) AssertGameType<T>(T model) where T : BasePageViewModel
        {
            GamePageType gameType = GamePageType.none;
            var category = string.Empty;
            var view = string.Empty;

            if (model.Content.HasProperty("gameType") && model.Content.GetProperty("gameType").HasValue())
            {
                gameType = (GamePageType)Enum.Parse(typeof(GamePageType), model.Content.GetProperty("gameType").GetValue().ToString());
                switch (gameType)
                {
                    case GamePageType.InPlay:
                        category = "in-play";
                        view = "in-play";
                        break;
                    case GamePageType.Casino:
                        category = "casino";
                        break;
                    case GamePageType.LiveCasino:
                        category = "Live Casino";
                        break;
                    case GamePageType.Poker:
                        category = "poker";
                        model.ShowProvidersFilter = false;
                        model.ShowProvidersFilter = false;
                        model.ShowSeachFilter = false;
                        model.SingleIconCentered = true;
                        break;
                    case GamePageType.Lottery:
                        category = "lottery";
                        break;
                    case GamePageType.Vegas:
                        category = "vegas";
                        break;
                    case GamePageType.Sport:
                        category = "sport";
                        view = "main";
                        break;
                    case GamePageType.Board:
                        category = "Board Games";
                        model.ShowSubCategoryFilter = false;
                        break;
                    default:
                        category = "";
                        break;
                }
            }
            else
            {
                category = model.IsHomePage ? "casino" : string.Empty;
            }
            return (category, view, gameType);
        }
    }
}
