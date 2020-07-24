namespace Umbraco.Plugins.Connector.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;

    [TestClass]
    public class Connector
    {
        [TestMethod]
        public void Connector_GenerateSha256()
        {
            //Arrange

            //Act
            var sha256 = EncryptDecryptHelper.Sha256Encrypt("A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=");

            //Assert
            Assert.AreEqual("zYPSLb7uYA1NHFHtvZw+DGyqJr2P2/sT8puaAP+NFgs=", sha256);
        }

        [TestMethod]
        public void Connector_AddJsonRpcSection()
        {
            //Arrange
            ConfigurationService helper = new ConfigurationService();

            //Act
            var created = helper.AddJsonRpcHandler($"{Directory.GetCurrentDirectory()}\\web.config");

            //Assert
            Assert.IsTrue(created);
        }

        [TestMethod]
        public async Task Connector_ApiKeyLogin()
        {
            //Arrange
            TotalCodeApiService helper = new TotalCodeApiService();

            //Act
            var response = await helper.ApiKeyLoginAsync("api_key_customer_management", "CustomerManagementGuid");

            //Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task Connector_VerifyMobileSendSMS()
        {
            //Arrange
            TotalCodeApiService helper = new TotalCodeApiService();
            var authorization = (ApiKeyLoginResponseContent)await helper.ApiKeyLoginAsync("api_key_customer_management", "CustomerManagementGuid");

            //Act
            var response = await helper.VerifyMobileAsync("B3988460-F283-4D44-8A5E-58EB7C909B39", "http://customer-management-service-api.totalcoding-test1.com", "+441500000000", "PT", authorization.AccessToken);

            //Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void Connector_GenerateTenantPreferencesJson()
        {
            //Arrange
            Tenant tenant = new Tenant()
            {
                AppId = "614960f9-ee70-4c6a-b24d-5039573c871b",//Guid.NewGuid().ToString(),
                ApiKey = "79ee8b63-fa81-4c0f-b8a8-756877868971",//Guid.NewGuid().ToString(),
                TenantUId = "B3988460-F283-4D44-8A5E-58EB7C909B39",//Guid.NewGuid(),
                Name = "Testing User",
                Username = "test@email.com",
                Password = "Password!123",
                Email = "test@email.com",
                Group = "Test Group",
                BrandName = "Test Site",
                SubDomain = "customer-management-service-api",
                Domain = "totalcoding-test1.com",
                Languages = new Languages
                {
                    Default = "en-US",
                    Alternate = new string[]
                    {
                        "en-GB",
                        "es-ES",
                        "fr-FR"
                    }
                },
                Currencies = new CurrencyCodes
                {
                    Codes = new string[] { "USD", "EUR" }
                },
                TenantPreferences = new TenantPreferences
                {
                    Title = new TenantPreferences.CustomerTitle
                    {
                        IsUsed = false
                    },
                    FirstName = new TenantPreferences.CustomerFirstName
                    {
                        IsUsed = true,
                        IsMandatory = true
                    },
                    LastName = new TenantPreferences.CustomerLastName
                    {
                        IsUsed = true,
                        IsMandatory = true
                    },
                    DateOfBirth = new TenantPreferences.CustomerDateOfBirth
                    {
                        IsUsed = true,
                        IsMandatory = false,
                        Validation = "^(0[1-9]|1[012])[-/.](0[1-9]|[12][0-9]|3[01])[-/.](19|20)\\d\\d$",
                        AllowedValues = new string[] { "1980", "1981", "1982", "1983", "1984", "1985", "1986", "1987", "1988", "1989", "1990", "1991", "1992" }
                    },
                    Age = new TenantPreferences.AgeRestriction
                    {
                        IsUsed = true,
                        IsMandatory = true,
                        MinValue = 13,
                        MaxValue = 99
                    },
                    Gender = new TenantPreferences.CustomerGender
                    {
                        IsUsed = true,
                        IsMandatory = false,
                        AllowedValues = new string[] { "F", "M" }
                    },
                    Email = new TenantPreferences.EmailAddress
                    {
                        ValidationRequired = true,
                        Validation = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
                        IsMandatory = true,
                        IsUsed = true
                    },
                    Mobile = new TenantPreferences.MobileNumber
                    {
                        IsUsed = true,
                        IsMandatory = true,
                        ValidationRequired = true,
                        Validation = "^[0-9]{10}$",
                        AllowedValues = new string[] { "+1", "+2", "+33", "+44" }
                    },
                    Address = new TenantPreferences.CustomerAddress
                    {
                        AddressLine1 = new TenantPreferences.CustomerAddress.CustomerAddressLine1
                        {
                            IsUsed = true,
                            IsMandatory = true,
                        },
                        AddressLine2 = new TenantPreferences.CustomerAddress.CustomerAddressLine2
                        {
                            IsUsed = true,
                            IsMandatory = false
                        },
                        AddressLine3 = new TenantPreferences.CustomerAddress.CustomerAddressLine3
                        {
                            IsUsed = true,
                            IsMandatory = false
                        },
                        CityOrTown = new TenantPreferences.CustomerAddress.CustomerCityOrTown
                        {
                            IsUsed = true,
                            IsMandatory = true,
                            AllowedValues = new[] { "New York", "Los Angeles" }
                        },
                        PostalCode = new TenantPreferences.CustomerAddress.CustomerPostalCode
                        {
                            IsUsed = true,
                            IsMandatory = true,
                            Validation = @"^[0-9]{5}(?:-[0-9]{4})?$"
                        }
                    },
                    Country = new TenantPreferences.CustomerCountry
                    {
                        IsUsed = true,
                        IsMandatory = true,
                        AllowedValues = new string[] { "USA", "GBR", "FRA" }
                    },
                    Preferences = new TenantPreferences.CustomerPreferences
                    {
                        DefaultCurrency = new TenantPreferences.CustomerPreferences.CustomerDefaultCurrency
                        {
                            IsUsed = true,
                            IsMandatory = false,
                            AllowedValues = new string[] { "USD" }
                        },
                        DefaultLanguage = new TenantPreferences.CustomerPreferences.CustomerDefaultLanguage
                        {
                            IsUsed = true,
                            IsMandatory = true,
                            AllowedValues = new string[] { "en-US", "en-GB" },
                            Validation = "/^(\\+?\\d{1,3}|\\d{1,4})$/"
                        },
                        OddsDisplay = new TenantPreferences.CustomerPreferences.CustomerOddsDisplay
                        {
                            IsUsed = true,
                            IsMandatory = false,
                            AllowedValues = new string[] { "Fractional" }
                        },
                        TimeZone = new TenantPreferences.CustomerPreferences.CustomerTimeZone
                        {
                            IsUsed = true,
                            IsMandatory = false,
                            AllowedValues = new string[] { "MST", "DST", "GMT" }
                        }
                    },
                    Security = new TenantPreferences.CustomerSecurity
                    {
                        Username = new TenantPreferences.CustomerSecurity.CustomerUsername
                        {
                            IsUsed = true,
                            IsMandatory = true
                        },
                        Password = new TenantPreferences.CustomerSecurity.CustomerPassword
                        {
                            MinValue = 10,
                            MaxValue = 12,
                            Validation = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\\$%\\^&\\*])(?=.{10,12})"
                        }
                    },
                    ReceiveNotifications = new TenantPreferences.CustomerReceiveNotifications { IsUsed = true, IsMandatory = false },
                    ReceiveNotificationsViaInPlatformMessages = new TenantPreferences.CustomerReceiveNotificationsViaInPlatformMessages { IsUsed = true, IsMandatory = false },
                    ReceiveNotificationsViaEmail = new TenantPreferences.CustomerReceiveNotificationViaEmail { IsUsed = true, IsMandatory = false },
                    ReceiveNotificationsViaSMS = new TenantPreferences.CustomerReceiveNotificationViaSMS { IsUsed = true, IsMandatory = false },
                    Referrer = new TenantPreferences.CustomerReferrer
                    {
                        IsUsed = true,
                        IsMandatory = false,
                        AllowedValues = new object[] { 1, 2, 3, 4, 5 }
                    },
                    BonusCode = new TenantPreferences.CustomerBonusCode
                    {
                        IsUsed = true,
                        IsMandatory = false,
                        AllowedValues = new string[] { "BONUS123", "BONUS12323" }
                    },
                    PaymentSettings = new PaymentSettings
                    {
                        PaymentMethods = new PaymentMethod[]
                    {
                        new PaymentMethod
                        {
                            PaymentIdentifier = "",
                            PaymentSystemName = "",
                            PaymentSystemCurrency = "",
                            Icon = "",
                            DepositFields = new CustomField[]
                            {
                                new CustomField
                                {
                                     Name = "",
                                     Label = "",
                                     Type = "",
                                     Value = "",
                                     IsReadonly = false,
                                     Required = true
                                },
                                new CustomField
                                {
                                     Name = "",
                                     Label = "",
                                     Type = "",
                                     Value = "",
                                     IsReadonly = false,
                                     Required = true
                                }
                            },
                            WithdrawalFields = new CustomField[]
                            {
                                new CustomField
                                {
                                     Name = "",
                                     Label = "",
                                     Type = "",
                                     Value = "",
                                     IsReadonly = false,
                                     Required = true
                                },
                                new CustomField
                                {
                                     Name = "",
                                     Label = "",
                                     Type = "",
                                     Value = "",
                                     IsReadonly = false,
                                     Required = true
                                }
                            }
                    }
                }
                    }
                }
            };

            //Act
            var json = JsonConvert.SerializeObject(tenant);

            //Assert
            Assert.IsNotNull(json);
        }

        [TestMethod]
        public void Connector_GenerateDefaultYears()
        {
            // arrange
            var thisYear = DateTime.Now.Year;
            var startYear = 1900;
            var difference = thisYear - startYear;

            // act
            var years = DefaultAllowedValues.GenerateDefaultYears(startYear);

            // assert
            Assert.AreEqual(difference, years.Length);
        }

        [TestMethod]
        public void Connector_GetDefaultTimeZones()
        {
            // arrange

            // act
            var timeZone = DefaultAllowedValues.GetAllDefaultTimeZones();

            // assert
            Assert.IsNotNull(timeZone);
        }

        [TestMethod]
        public void Connector_GetDefaultPhoneCountryCodes()
        {
            // arrange

            // act
            var countryCodes = DefaultAllowedValues.DefaultPhoneCountryCodes;

            // assert
            Assert.IsNotNull(countryCodes);
        }

        [TestMethod]
        public void Connector_GetDefaultCountries()
        {
            // arrange

            // act
            var languageCodes = DefaultAllowedValues.DefaultLanguageCodes;

            // assert
            Assert.IsNotNull(languageCodes);
        }

        [TestMethod]
        public void Connector_GetAllTimeZones()
        {
            //Arrange

            //Act
            var timeZones = DefaultAllowedValues.GetAllDefaultTimeZones();

            //Assert
            Assert.IsNotNull(timeZones);
        }

        [TestMethod]
        public void Connector_GetTimeZonesByCode()
        {
            //Arrange
            var zones = new string[] { "IRST", "Dateline Standard Time", "MST", "GMT" };

            //Act
            var timeZones = DefaultAllowedValues.GetTenantTimeZones(zones);

            //Assert
            Assert.AreEqual(4, timeZones.Count);
        }
    }
}
