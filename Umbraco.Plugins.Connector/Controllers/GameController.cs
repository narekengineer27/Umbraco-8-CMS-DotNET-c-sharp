namespace Umbraco.Plugins.Connector.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web;
    using Umbraco.Web.Mvc;
    using Newtonsoft.Json;
    using Umbraco.Plugins.Connector.Models;
    using System.Linq;
    using System.Globalization;
    using Umbraco.Plugins.Connector.Interfaces;

    public class GameController : SurfaceController
    {
        private readonly TotalCodeApiService apiService;
        private readonly IContentService contentService;
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
            this.apiService = new TotalCodeApiService();
            this.contentService = ConnectorContext.ContentService;
            using (var scope = ConnectorContext.ScopeProvider.CreateScope(autoComplete: true))
            {
                ApiKeyCache.UpdateCache(scope.Database);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> GetGameGrid(string tenantUid, string category = "", string subCategory = "", string provider = "", string keyword = "", string languageCode = "")
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);
            var response = await _gameService.GetGameDataAnonymousArrayAsync(tenantUid, origin, category, subCategory, provider, keyword, languageCode, authorization.AccessToken);

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        public async Task<JsonResult> GetLiveCasinoGrid(string tenantUid, string category = "", string subCategory = "", string provider = "", string keyword = "", string languageCode = "")
        {

            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            //var key = ApiKeyCache.GetByTenantUid(tenantUid);
            //var authorization = await new Authorization().GetAuthorizationAsync(key);
            var authorization = ApiAuthorizationCache.GetOrSetAuthorization(tenantUid);

            var response = await _gameService.GetGameDataAnonymousArrayAsync(tenantUid, origin, category, subCategory, provider, keyword, languageCode, authorization.AccessToken);

            CultureInfo newLanguage = new CultureInfo(languageCode);
            System.Threading.Thread.CurrentThread.CurrentCulture = newLanguage;
            System.Threading.Thread.CurrentThread.CurrentUICulture = newLanguage;

            foreach (var gameDetail in response)
            {
                var name = "[GameGridProvider]" + gameDetail.GameProvider;
                gameDetail.GameProvider = Umbraco.GetDictionaryValue(name, gameDetail.GameProvider);
            }

            if (category == "lottery" || category == "casino")
            {
                foreach (var game in response)
                {
                    if (game.GameImages[0].Url == null || game.GameImages[0].Url == "")
                    {
                        game.GameImages[0].Url = "https://via.placeholder.com/150x150";
                    }
                }
            }

            if (category == "Live Casino")
            {
                foreach (var game in response)
                {

                    if (game.GameImages[1].Url == null || game.GameImages[1].Url == "")
                    {
                        game.GameImages[1].Url = "https://via.placeholder.com/432x243";
                    }

                    CasinoProLobbyData config = new CasinoProLobbyData();
                    foreach (var configuration in game.Configurations)
                    {
                        config = JsonConvert.DeserializeObject<CasinoProLobbyData>(configuration.Value.ToString());
                    }

                    if (!String.IsNullOrEmpty(config.gameType))
                    {
                        game.GameType = config.gameType;
                    }
                    if (config.dealer != null)
                    {
                        if (!String.IsNullOrEmpty(config.dealer.name))
                        {
                            game.DealerName = config.dealer.name;
                        }
                    }


                    if (!String.IsNullOrEmpty(config.symbol))
                    {
                        game.Currency_symbol = config.symbol;
                    }
                    if (config.minAmount < 1)
                    {
                        game.Limit_min = Math.Round(config.minAmount, 2, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture);
                    }
                    if (config.minAmount >= 1)
                    {
                        game.Limit_min = Math.Round(config.minAmount, 0, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture);
                    }
                    if (config.maxAmount >= 1)
                    {
                        game.Limit_max += Math.Round(config.maxAmount, 0, MidpointRounding.AwayFromZero).ToString("N0", CultureInfo.InvariantCulture);
                    }

                    if (config.road != null && config.gameType == "Baccarat")
                    {
                        var BaccaratResults = JsonConvert.DeserializeObject<List<CasinoProLobbyRoad>>(config.road.ToString());
                        var road = new List<BaccaratResult>();
                        foreach (var res in BaccaratResults)
                        {
                            string color = "";
                            string X = "";
                            string Y = "";
                            if (res.color == "Red")
                            {
                                color = "#C52123";
                            }
                            if (res.color == "Blue")
                            {
                                color = "#185CC6";
                            }
                            foreach (var loc in res.location)
                            {
                                if (loc.Key == "column")
                                {
                                    X = loc.Value;
                                }
                                if (loc.Key == "row")
                                {
                                    Y = loc.Value;
                                }
                            }
                            road.Add(new BaccaratResult()
                            {
                                X = X,
                                Y = Y,
                                Color = color
                            });

                        }
                        game.Roads = road;

                    }

                    if (config.results != null && config.gameType == "Roulette")
                    {
                        var aa = JsonConvert.DeserializeObject<List<string>>(config.results.ToString());
                        var results = new List<GameResult>();
                        int counter = 0;
                        foreach (var a in aa)
                        {
                            var color = "";
                            int number = 0; Int32.TryParse(a, out number);
                            int[] reds = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
                            int[] blacks = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
                            if (reds.Contains<int>(number))
                            {
                                color = "red";
                            }
                            if (blacks.Contains<int>(number))
                            {
                                color = "black";
                            }
                            if (number == 0)
                            {
                                color = "green";
                            }
                            if (counter == 0)
                            {
                                color += "-first";
                            }
                            counter++;

                            results.Add(new GameResult()
                            {
                                Value = a,
                                Color = color
                            });

                        }

                        game.Results = results;
                    }

                    if (config.gameType == "Blackjack" && config.seatsTaken != null)
                    {
                        var seats = new List<Seat>();
                        int[] seatsTaken = config.seatsTaken;
                        for (int i = 1; i <= config.seats; i++)
                        {
                            var Availability = "free";
                            if (seatsTaken.Contains<int>(i - 1))
                            {
                                Availability = "taken";
                            }
                            seats.Add(new Seat()
                            {
                                Number = i.ToString(),
                                Status = Availability
                            });
                        }
                        game.Seats = seats;
                    }

                }
            }

            return Json(response, JsonRequestBehavior.DenyGet);
        }

        public ActionResult GameIframe()
        {
            string token = String.Empty;
            var apiService = new TotalCodeApiService();
            var page = Umbraco.Content(CurrentPage.Parent.Id);

            var homePage = CurrentPage.Root();
            var TenantUid = homePage.Value<string>("tenantUid");
            var currentCulture = Umbraco.CultureDictionary.Culture.TwoLetterISOLanguageName;

            int gameId; Int32.TryParse(CurrentPage.GetProperty("gameId").Value().ToString(), out gameId);

            HttpCookie tokenCookie = Request.Cookies["token"];
            if (tokenCookie != null)
            {
                token = tokenCookie.Value;
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect(page.GetUrl(currentCulture) + "?gameId=" + gameId + "#popup-login");
            }

            System.Web.HttpContext.Current.Response.Redirect(page.GetUrl(currentCulture) + "?gameId=" + gameId);

            string origin = Request.Url.ToString();

            string gameName = CurrentPage.GetProperty("gameName").Value().ToString();

            var game = (GameByIdResponseContent)apiService.GetGameById(TenantUid, origin, gameId, gameIdentifier: gameName, authorization: token, languageCode: currentCulture);
            var lobby = "";

            if (game != null)
            {
                var gameLobbyUrl = (GameLobbyResponseContent)apiService.GetGameLobby(game.Url, TenantUid, origin, customerToken: token, locale: currentCulture);
                if (gameLobbyUrl.Success)
                {
                    lobby = gameLobbyUrl.Link;
                }
            }

            var result = "<iframe src=\"" + lobby + "\" id=\"game-iframe\" frameborder=\"0\" allowfullscreen></iframe>";
            return Content(result);
        }

        public class CasinoProLobbyData
        {
            public int gameId { get; set; }
            public string tableId { get; set; }
            public string name { get; set; }
            public int playerCount { get; set; }
            public CasinoProLobbyDealer dealer { get; set; }
            public CasinoProLobbyVideoSnapshot videoSnapshots { get; set; }
            public bool open { get; set; }
            public int seats { get; set; }
            public int[] seatsTaken { get; set; }
            public object dealarHand { get; set; }
            public bool betBehind { get; set; }
            public string gameType { get; set; }
            public CasinoProLobbyHours operationHours { get; set; }
            public int seatsLimit { get; set; }
            public object results { get; set; }
            public object history { get; set; }
            public object road { get; set; }
            public object privateTableConfig { get; set; }
            public string currency { get; set; }
            public string symbol { get; set; }
            public decimal minAmount { get; set; }
            public decimal maxAmount { get; set; }
        }

        public class CasinoProLobbyRoad
        {
            public Dictionary<string, string> location { get; set; }
            public string color { get; set; }
            public string score { get; set; }
            public int ties { get; set; }
            public bool playerPair { get; set; }
            public bool bankerPair { get; set; }
            public bool natural { get; set; }
            public string isOdd { get; set; }
        }

        public class CasinoProLobbyHours
        {
            public string type { get; set; }
            public Dictionary<string, string> value { get; set; }
        }

        public class CasinoProLobbyDealer
        {
            public string dealerId { get; set; }
            public string name { get; set; }
        }

        public class CasinoProLobbyVideoSnapshot
        {
            public Dictionary<string, string> links { get; set; }
            public Dictionary<string, string> thumbnails { get; set; }
        }

    }
}
