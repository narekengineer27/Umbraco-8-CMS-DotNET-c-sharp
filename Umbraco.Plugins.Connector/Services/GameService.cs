using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Core.Cache;
using Umbraco.Plugins.Connector.Interfaces;
using Umbraco.Plugins.Connector.Models;
using Umbraco.Core.Logging;
using Umbraco.Plugins.Connector.Helpers;

namespace Umbraco.Plugins.Connector.Services
{
    public class GameService : BaseService, IGameService
    {
        #region Api Urls
        private readonly string URL_GET_GAME_DATA;
        #endregion

        #region Cache Names
        private const string CACHE_GAME_DATA_ANONYMOUS_ARRAY = "GameDataAnonymousArray";
        #endregion

        private readonly AppCaches _appCaches;
        private readonly ILogger _logger;

        public GameService(AppCaches appCaches, ILogger logger)
        {
            _appCaches = appCaches;
            _logger = logger;

            URL_GET_GAME_DATA = URL_GAME_MANAGEMENT_DOMAIN + "/api/TenantGame/get-tenant-games";
        }

        public async Task<List<GameDetails>> GetGameDataAnonymousArrayAsync(string tenantUid, string origin, string category = "", string subCategory = "", string provider = "", string keyword = "", string languageCode = "", string authorization = "")
        {
            try
            {
                var gameParameter = new GameData
                {
                    Category = category,
                    SubCategory = subCategory,
                    Provider = provider,
                    Keyword = keyword
                };

                var result = await CacheHelper.GetOrSetCacheAsync<List<GameDetails>>(new CacheInfo()
                                        {
                                            CacheName = "GetGameDataAnonymousArrayAsync",
                                            TenantUid = tenantUid,
                                            Tags = new CacheTagBuilder()
                                                        .Add(()=> origin)
                                                        .Add(()=> category)
                                                        .Add(()=> subCategory)
                                                        .Add(()=> provider)
                                                        .Add(()=> keyword)
                                                        .Add(()=> languageCode)
                                                        .Compile()
                                        }, async () =>
                                        {
                                            IRestResponse response = await SubmitPostAsync(URL_GET_GAME_DATA, origin, gameParameter, authorization, tenantUid, locale: languageCode);
                                            var data = await GameDataAnonymousArrayResponseContentAsync(response);
                                            return data.OrderBy(x => x.Priority).ToList();
                                        }, TimeSpan.FromSeconds(CacheSetting.TimeoutInSeconds));

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error<GameService>(ex);
                return null;
            }
        }
    }
}
