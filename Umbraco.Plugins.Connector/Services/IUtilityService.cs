using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Plugins.Connector.Interfaces;

namespace Umbraco.Plugins.Connector.Services
{
    public interface IUtilityService
    {
        Task<IResponseContent> PostAsync(string token, string origin, string tenantUid, string baseCurrency, string quoteCurrency, string paymentSystem);
    }
}
