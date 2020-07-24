namespace Umbraco.Plugins.Connector.Controllers
{
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web.Mvc;
    using System.Linq;
    using System.Reflection;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Core.Logging;
    
    using System.Web.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Helpers;

    public class UtilityController : BaseController
    {
        private readonly ILocalizationService _localizationService;
        private readonly IUtilityService _utilityService;
        private LanguageDictionaryService _languageDictionaryService;

        public UtilityController(ILocalizationService localizationService, IDomainService domainService, ILogger logger, IUtilityService utilityService, IContentService contentService)
        {
            _localizationService = localizationService;
            _languageDictionaryService = new LanguageDictionaryService(localizationService, domainService, logger);
            _utilityService = utilityService;
        }

        [HttpPost]
        public async Task<JsonResult> SendRequest(string url, Dictionary<string, object> headers, string body, Method method)
        {
            var client = new RestClient(url);
            var request = new RestRequest(method);

            foreach (var header in headers)
            {
                if (header.Value != null)
                {
                    request.AddHeader(header.Key, header.Value.ToString());
                }
            }

            request.AddParameter("body", body, ParameterType.RequestBody);

            var response = await client.ExecuteTaskAsync(request);

            return Json(new DebugResponse
            {
                StatusDescription = response.StatusDescription,
                Content = response.Content
            });
        }

        [HttpPost]
        public JsonResult UpdateDictionaries()
        {
            string nspace = "Umbraco.Plugins.Connector.Dictionaries";
            var idictionary = typeof(Umbraco.Plugins.Connector.Interfaces.IDictionaryItem);
            var dictionaryTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == nspace && idictionary.IsAssignableFrom(t)
                    select t;
            
            var updatedDictionaries = new List<string>();
            foreach (var dictionaryType in dictionaryTypes)
            {
                var keyString = dictionaryType.GetProperty("Key").Value<string>();
                if (!_localizationService.DictionaryItemExists(keyString))
                {
                    _languageDictionaryService.CreateDictionaryItem(dictionaryType);
                    updatedDictionaries.Add(keyString);
                }
            }

            return Json(updatedDictionaries);
        }

        [HttpPost]
        public async Task<JsonResult> Currencies(string tenantUid, string baseCurrency, string quoteCurrency, string paymentSystem)
        {
            var token = Request.Cookies["token"].Value;
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);

            var response = (CurrencyResponseContent) await _utilityService.PostAsync(token, origin, tenantUid, baseCurrency, quoteCurrency, paymentSystem);
            if (response.Value > 1000)
            {
                response.Value = decimal.Round(response.Value, 0);
            }
            else
            {
                response.Value = decimal.Round(response.Value, 2);
            }
            return Json(response, JsonRequestBehavior.DenyGet);
        }
    }


    public class DebugResponse
    {
        public string StatusDescription { get; set; }
        public string Content { get; set; }
    }
}
