namespace Umbraco.Plugins.Connector.Helpers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;
    using System.Xml;
    using System.Xml.Linq;
    using Umbraco.Plugins.Connector.Models;

    // edited from https://stackoverflow.com/questions/42691271/runtime-access-to-web-config-system-webserver-websocket-section
    public class ConfigurationService
    {
        private const string UMBRACO_SETTINGS_PATH = "~/config/umbracoSettings.config",
            CLIENT_DEPENDENCY_PATH = "~/config/ClientDependency.config";
        public Configuration Configuration { get; }
        public XDocument UmbracoSettings { get; }
        public XDocument ClientDependency { get; }

        public ConfigurationService()
        {
            Configuration = WebConfigurationManager.OpenWebConfiguration("~");
            UmbracoSettings = XDocument.Load(HttpContext.Current.Server.MapPath(UMBRACO_SETTINGS_PATH));
            ClientDependency = XDocument.Load(HttpContext.Current.Server.MapPath(CLIENT_DEPENDENCY_PATH));
        }
        public bool AddJsonRpcHandler(string path = "")
        {
            Configuration config;
            if (string.IsNullOrEmpty(path)) config = Configuration;
            else config = OpenConfigFile(path);

            ConfigurationSection section = Configuration.GetSection("system.webServer");
            XmlDocument webServerXml = new XmlDocument();
            webServerXml.LoadXml(section.SectionInformation.GetRawXml());
            XmlNode handlers = webServerXml.GetElementsByTagName("handlers")[0];

            bool jsonRpcExists = handlers.SelectSingleNode("add[@name='jsonrpc']") != null;
            if (!jsonRpcExists)
            {
                // <add name="jsonrpc" type="AustinHarris.JsonRpc.Handlers.AspNet.JsonRpcHandler" verb="*" path="*.rpc"/>
                XmlNode jsonrpc = webServerXml.CreateNode(XmlNodeType.Element, "add", null);
                XmlAttribute nameAttr = webServerXml.CreateAttribute("name");
                XmlAttribute typeAttr = webServerXml.CreateAttribute("type");
                XmlAttribute verbAttr = webServerXml.CreateAttribute("verb");
                XmlAttribute pathAttr = webServerXml.CreateAttribute("path");
                nameAttr.Value = "jsonrpc";
                typeAttr.Value = "AustinHarris.JsonRpc.Handlers.AspNet.JsonRpcHandler";
                verbAttr.Value = "*";
                pathAttr.Value = "*.rpc";

                jsonrpc.Attributes.Append(nameAttr);
                jsonrpc.Attributes.Append(typeAttr);
                jsonrpc.Attributes.Append(verbAttr);
                jsonrpc.Attributes.Append(pathAttr);

                handlers.AppendChild(jsonrpc);
                webServerXml.DocumentElement.AppendChild(handlers);

                section.SectionInformation.SetRawXml(webServerXml.OuterXml);
                config.Save();
                return true;
            }
            return false;
        }

        private static Configuration OpenConfigFile(string configPath)
        {
            var configFile = new FileInfo(configPath);
            var vdm = new VirtualDirectoryMapping(configFile.DirectoryName, true, configFile.Name);
            var wcfm = new WebConfigurationFileMap();
            wcfm.VirtualDirectories.Add("/", vdm);
            return WebConfigurationManager.OpenMappedWebConfiguration(wcfm, "/");
        }

        public void AddApiSettings()
        {
            bool changed = false;
            // add default values in case there are none set
            if (ConfigurationManager.AppSettings["TotalCode.Admin.AppId"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.AppId", "4d53bce03ec34c0a911182d4c228ee6c");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.ApiKey"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.ApiKey", "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.SportsPageIframeUrl"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.SportsPageIframeUrl", "https://sportsbook-customer-portal.totalcoding-test1.com");
                changed = true;
            }
            //if (ConfigurationManager.AppSettings["TotalCode.Admin.LiveCasinoPageIframeUrl"] == null)
            //{
            //    Configuration.AppSettings.Settings.Add("TotalCode.Admin.LiveCasinoPageIframeUrl", "https://sportsbook-customer-portal.totalcoding-test1.com");
            //    changed = true;
            //}
            //if (ConfigurationManager.AppSettings["TotalCode.Admin.InPlayPageIframeUrl"] == null)
            //{
            //    Configuration.AppSettings.Settings.Add("TotalCode.Admin.InPlayPageIframeUrl", "https://sportsbook-customer-portal.totalcoding-test1.com");
            //    changed = true;
            //}
            //if (ConfigurationManager.AppSettings["TotalCode.Admin.PokerPageIframeUrl"] == null)
            //{
            //    Configuration.AppSettings.Settings.Add("TotalCode.Admin.PokerPageIframeUrl", "https://sportsbook-customer-portal.totalcoding-test1.com");
            //    changed = true;
            //}
            //if (ConfigurationManager.AppSettings["TotalCode.Admin.VegasPageIframeUrl"] == null)
            //{
            //    Configuration.AppSettings.Settings.Add("TotalCode.Admin.VegasPageIframeUrl", "https://sportsbook-customer-portal.totalcoding-test1.com");
            //    changed = true;
            //}
            //if (ConfigurationManager.AppSettings["TotalCode.Admin.LotteryPageIframeUrl"] == null)
            //{
            //    Configuration.AppSettings.Settings.Add("TotalCode.Admin.LotteryPageIframeUrl", "https://sportsbook-customer-portal.totalcoding-test1.com");
            //    changed = true;
            //}
            //if (ConfigurationManager.AppSettings["TotalCode.Admin.CasinoPageIframeUrl"] == null)
            //{
            //    Configuration.AppSettings.Settings.Add("TotalCode.Admin.CasinoPageIframeUrl", "http://149.28.84.104:3000");
            //    changed = true;
            //}
            if (ConfigurationManager.AppSettings["TotalCode.Admin.CustomerManagementUrl"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.CustomerManagementUrl", "https://customer-management-service-api.totalcoding-test1.com");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.FinancialManagementUrl"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.FinancialManagementUrl", "https://sit-financial-management-service-api.totalcoding-test1.com");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.UserManagementUrl"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.UserManagementUrl", "https://sit-user-management-service-api.totalcoding-test1.com");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.GameManagementUrl"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.GameManagementUrl", "https://casino-service-api.totalcoding-test1.com");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.HelpdeskManagementUrl"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.HelpdeskManagementUrl", "https://helpdesk-service-api.totalcoding-test1.com");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.CurrencyManagementUrl"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.CurrencyManagementUrl", "https://sit-microservices-currency-service.totalcoding-test1.com");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.NotificationManagementUrl"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.NotificationManagementUrl", "https://sit-notification.nitrobet-dev.com");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.SaveAndPublish"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.SaveAndPublish", "true");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.SecureUrls"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.SecureUrls", "true");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.SetupLocalUrls"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.SetupLocalUrls", "false");
                changed = true;
            }
            if (ConfigurationManager.AppSettings["TotalCode.Admin.CacheTimeoutInSeconds"] == null)
            {
                Configuration.AppSettings.Settings.Add("TotalCode.Admin.CacheTimeoutInSeconds", "3600");
                changed = true;
            }

            if (changed)
                Configuration.Save(ConfigurationSaveMode.Modified);
        }

        //[Obsolete]
        //public (string, string) GetApiSettings()
        //{
        //    AddApiSettings();
        //    return (configuration.AppSettings.Settings["TotalCode.Admin.AppId"].Value, configuration.AppSettings.Settings["TotalCode.Admin.ApiKey"].Value);
        //}

        public ApiSettings GetApiSettings()
        {
            var settings = new ApiSettings();

            // web.config settings
            var keys = Configuration.AppSettings.Settings.AllKeys.Where(x => x.Contains("TotalCode.Admin."));
            foreach (var key in keys)
            {
                settings.Add(new ApiSetting
                {
                    Key = key,
                    Value = Configuration.AppSettings.Settings[key].Value,
                    SettingType = ApiSettingType.AppSettings
                });
            }

            var customErrorsSection =
            (CustomErrorsSection)Configuration.GetSection(
                "system.web/customErrors");

            var customErrorsMode = new ApiSetting
            {
                Key = "Mode",
                Value = customErrorsSection.Mode.ToString(),
                SettingType = ApiSettingType.CustomErrors
            };

            var customErrorsDefaultRedirect = new ApiSetting
            {
                Key = "DefaultRedirect",
                Value = customErrorsSection.DefaultRedirect,
                SettingType = ApiSettingType.CustomErrors
            };

            var customErrors500Error = new ApiSetting
            {
                Key = "500Error",
                Value = customErrorsSection.Errors["500"] != null ? customErrorsSection.Errors["500"].Redirect : "",
                SettingType = ApiSettingType.CustomErrors
            };

            settings.Add(customErrorsMode);
            settings.Add(customErrorsDefaultRedirect);
            settings.Add(customErrors500Error);

            // Get Umbraco settings
            var error404 = new ApiSetting
            {
                Key = "error404",
                Value = UmbracoSettings.Descendants("error404").First().Value,
                SettingType = ApiSettingType.UmbracoConfig
            };
            var notificationEmail = new ApiSetting
            {
                Key = "email",
                Value = UmbracoSettings.Descendants("email").First().Value,
                SettingType = ApiSettingType.UmbracoConfig
            };
            var loginBackgroundImage = new ApiSetting
            {
                Key = "loginBackgroundImage",
                Value = UmbracoSettings.Descendants("loginBackgroundImage").First().Value,
                SettingType = ApiSettingType.UmbracoConfig
            };

            // Client dependency Settings
            var clientDepenency = new ApiSetting
            {
                Key = "clientDependency",
                Value = ClientDependency.Descendants("clientDependency").Attributes().Single(x => x.Name == "version").Value,
                SettingType = ApiSettingType.ClientDependency
            };

            settings.Add(error404);
            settings.Add(notificationEmail);
            settings.Add(loginBackgroundImage);
            settings.Add(clientDepenency);
            return settings;
        }


        [Obsolete]
        public void EditApiInfo(string appId, string apiKey)
        {
            AddApiSettings();
            Configuration.AppSettings.Settings["TotalCode.Admin.AppId"].Value = appId;
            Configuration.AppSettings.Settings["TotalCode.Admin.ApiKey"].Value = apiKey;
            Configuration.Save(ConfigurationSaveMode.Modified);
        }

        public void EditApiSettings(ApiSettings settings)
        {
            foreach (var setting in settings)
            {
                switch (setting.SettingType)
                {
                    case ApiSettingType.AppSettings:
                        Configuration.AppSettings.Settings[setting.Key].Value = setting.Value;
                        Configuration.Save(ConfigurationSaveMode.Modified);
                        break;
                    case ApiSettingType.CustomErrors:
                        var customErrorsSection = (CustomErrorsSection)Configuration.GetSection("system.web/customErrors");
                        if (setting.Key == "Mode")
                        {
                            customErrorsSection.Mode = (CustomErrorsMode)Enum.Parse(typeof(CustomErrorsMode), setting.Value);
                        }
                        else if (setting.Key == "DefaultRedirect")
                        {
                            customErrorsSection.DefaultRedirect = setting.Value;
                        }
                        else if (setting.Key == "500Error")
                        {
                            if (string.IsNullOrEmpty(setting.Value))
                            {
                                customErrorsSection.Errors.Remove("500");
                            }
                            else
                            {
                                if (customErrorsSection.Errors["500"] == null)
                                {
                                    customErrorsSection.Errors.Add(new CustomError(500, setting.Value));
                                }
                                else
                                {
                                    customErrorsSection.Errors["500"].Redirect = setting.Value;
                                }
                            }
                        }
                        Configuration.Save(ConfigurationSaveMode.Modified);
                        break;
                    case ApiSettingType.ClientDependency:
                        ClientDependency.Descendants("clientDependency").Attributes().Single(x => x.Name == "version").Value = setting.Value;
                        ClientDependency.Save(HttpContext.Current.Server.MapPath(CLIENT_DEPENDENCY_PATH));
                        break;
                    case ApiSettingType.UmbracoConfig:
                        if (setting.Key == "error404")
                        {
                            UmbracoSettings.Descendants("error404").First().Value = setting.Value;
                        }
                        else if (setting.Key == "loginBackgroundImage")
                        {
                            UmbracoSettings.Descendants("loginBackgroundImage").First().Value = setting.Value;
                        }
                        else if (setting.Key == "email")
                        {
                            UmbracoSettings.Descendants("email").First().Value = setting.Value;
                        }
                        UmbracoSettings.Save(HttpContext.Current.Server.MapPath(UMBRACO_SETTINGS_PATH));
                        break;
                    default:
                        break;
                }
            }

            settings.LoadConfigurationsIntoMemory();
        }
    }
    public static class ConfigurationServiceHelper
    {
        public static void LoadConfigurationsIntoMemory(this ApiSettings settings)
        {
            ConnectorContext.MasterAppId = settings["TotalCode.Admin.AppId"].Value;
            ConnectorContext.MasterApiKey = settings["TotalCode.Admin.ApiKey"].Value;
            IframeUrls.SportIframeUrl = settings["TotalCode.Admin.SportsPageIframeUrl"].Value;
            //IframeUrls.CasinoIframeUrl                 = settings["TotalCode.Admin.CasinoPageIframeUrl"].Value;
            //IframeUrls.LiveCasinoIframeUrl             = settings["TotalCode.Admin.LiveCasinoPageIframeUrl"].Value;
            //IframeUrls.InPlayIframeUrl                 = settings["TotalCode.Admin.InPlayPageIframeUrl"].Value;
            //IframeUrls.PokerframeUrl                   = settings["TotalCode.Admin.PokerPageIframeUrl"].Value;
            //IframeUrls.VegasIframeUrl                  = settings["TotalCode.Admin.VegasPageIframeUrl"].Value;
            //IframeUrls.LotteryIframeUrl                = settings["TotalCode.Admin.LotteryPageIframeUrl"].Value;
            ApiUrls.CustomerManagementUrl = settings["TotalCode.Admin.CustomerManagementUrl"].Value;
            ApiUrls.UserManagementUrl = settings["TotalCode.Admin.UserManagementUrl"].Value;
            ApiUrls.FinancialManagementUrl = settings["TotalCode.Admin.FinancialManagementUrl"].Value;
            ApiUrls.GameManagementUrl = settings["TotalCode.Admin.GameManagementUrl"].Value;
            ApiUrls.HelpdeskManagementUrl = settings["TotalCode.Admin.HelpdeskManagementUrl"].Value;
            ApiUrls.HelpdeskManagementUploadUrl = settings["TotalCode.Admin.HelpdeskManagementUploadUrl"].Value;
            ApiUrls.CurrencyManagementUrl = settings["TotalCode.Admin.CurrencyManagementUrl"].Value;
            ApiUrls.NotificationManagementUrl = settings["TotalCode.Admin.NotificationManagementUrl"].Value;
            TenantGenerationOptions.SetupLocalUrls = settings["TotalCode.Admin.SetupLocalUrls"].Value;
            TenantGenerationOptions.SaveAndPublish = settings["TotalCode.Admin.SaveAndPublish"].Value;
            TenantGenerationOptions.SecureUrls = settings["TotalCode.Admin.SecureUrls"].Value;
            CacheSetting.TimeoutInSeconds = double.Parse(settings["TotalCode.Admin.CacheTimeoutInSeconds"].Value);
        }
    }
}
