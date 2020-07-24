using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Plugins.Connector.Models;

namespace Umbraco.Plugins.Connector.Interfaces
{
    public interface IGameService
    {
        Task<List<GameDetails>> GetGameDataAnonymousArrayAsync(string tenantUid, string origin, string category = "", string subCategory = "", string provider = "", string keyword = "", string languageCode = "", string authorization = "");
    }
}
