namespace Umbraco.Plugins.Connector.Models
{
    using System;
    using System.Collections.Generic;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Services;

    public class CustomField
    {
        public bool IsReadonly { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Alias { get; set; }
    }

    public class ExtendedTenant
    {
        public static List<string> DefaultPermissions = new List<string> { "I", "P", "K", "F", "ï", "D", "C", "U", "A", "O", "S" };
        public static List<string> DefaultManagerPermissions = new List<string> { "F", "C", "U", "A" };
        public IEnumerable<string> AllowedSectionAliases { get; set; }
        /// <summary>
        /// The set of default permissions
        /// </summary>
        /// <remarks>
        /// By default each permission is simply a single char but we've made this an enumerable{string} to support a more flexible permissions structure in the future.
        /// </remarks>
        public IEnumerable<string> Permissions { get; set; }

        public int StartContentId { get; set; }
        public int StartMediaId { get; set; }
        public Tenant Tenant { get; set; }
        public int UserId { get; set; }
    }

    public class PaymentMethod
    {
        public ICollection<CustomField> DepositFields { get; set; }
        public string Icon { get; set; }
        public string PaymentIdentifier { get; set; }
        public string PaymentSystemCurrency { get; set; }
        public string PaymentSystemName { get; set; }
        public ICollection<CustomField> WithdrawalFields { get; set; }
        public int? Priority { get; set; }
        public bool isDefault { get; set; }
        public string PaymentSystemNameOrig { get; set; }
    }

    public class PaymentSettings
    {
        public PaymentMethod[] PaymentMethods { get; set; }
    }

    public class SimpleTenant
    {
        public string Group { get; set; }
        public string TenantUId { get; set; }
        public string Username { get; set; }
    }

    public class Tenant
    {
        public string ApiKey { get; set; }
        public string AppId { get; set; }
        public string BrandName { get; set; }
        public CurrencyCodes Currencies { get; set; }
        public string Domain { get; set; }
        public string[] AlternateDomains { get; set; }
        public string Email { get; set; }
        public string Group { get; set; }
        public Languages Languages { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public PaymentSettings PaymentSettings { get; set; }
        public string SubDomain { get; set; }
        public TenantPreferences TenantPreferences { get; set; }
        public string TenantUId { get; set; }
        public string Username { get; set; }
        public string HelpdeskTelegramAccount { get; set; }

        public static implicit operator Tenant(TenantData data)
        {
            return new Tenant
            {
                ApiKey = data.ApiKey,
                AppId = data.AppId,
                BrandName = data.BrandName,
                Domain = data.Domain,
                SubDomain = data.SubDomain,
                Currencies = data.Currencies,
                Languages = data.Languages,
                TenantUId = data.TenantUId,
                TenantPreferences = data.TenantPreferences,
                Name = data.Name,
                HelpdeskTelegramAccount = data.HelpdeskTelegramAccount
            };
        }
    }

    public class TenantData
    {
        public string ApiKey { get; set; }
        public string AppId { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public CurrencyCodes Currencies { get; set; }
        public string Domain { get; set; }
        public Languages Languages { get; set; }
        public PaymentSettings PaymentSettings { get; set; }
        public string ReadOnlyGroupAlias { get; internal set; }
        public string SubDomain { get; set; }
        public TenantPreferences TenantPreferences { get; set; }
        public string TenantUId { get; set; }
        public string HelpdeskTelegramAccount { get; set; }
    }

    public class TenantDomain
    {
        public string Domain { get; set; }
        public string TenantUid { get; set; }
        public bool isPrimary { get; set; }
    }

    public class TenantDomains
    {
        public string Domain { get; set; }
        public string[] AlternateDomains { get; set; }
        public string SubDomain { get; set; }

    }

    public class TenantEdit
    {
        public TenantGroup Group { get; set; }
        public TenantData Tenant { get; set; }
        public TenantUser User { get; set; }
    }

    public class TenantGroup
    {
        public static List<string> DefaultPermissions = new List<string> { "I", "P", "K", "F", "ï", "D", "C", "U", "A", "O", "S" };
        public string Alias { get; set; }
        public IEnumerable<string> AllowedSectionAliases { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// The set of default permissions
        /// </summary>
        /// <remarks>
        /// By default each permission is simply a single char but we've made this an enumerable{string} to support a more flexible permissions structure in the future.
        /// </remarks>
        public IEnumerable<string> Permissions { get; set; }

        public string RenameGroupTo { get; set; }
        public int? StartContentId { get; set; }
        public int? StartMediaId { get; set; }
        public string TenantUid { get; set; }
        public static implicit operator TenantGroup(ExtendedTenant tenant)
        {
            return new TenantGroup
            {
                AllowedSectionAliases = tenant.AllowedSectionAliases,
                Permissions = tenant.Permissions,
                StartContentId = tenant.StartContentId,
                StartMediaId = tenant.StartMediaId,
                TenantUid = tenant.Tenant.TenantUId,
                Name = tenant.Tenant.Group
            };
        }

        public static implicit operator TenantGroup(string groupName)
        {
            return new TenantGroup
            {
                Name = groupName,
                Alias = groupName.Sanitize()
            };
        }
    }

    public class TenantPreferences
    {
        public CustomerAddress Address { get; set; }
        public AgeRestriction Age { get; set; }
        public CustomerBonusCode BonusCode { get; set; }
        public CustomerCountry Country { get; set; }
        public CustomerDateOfBirth DateOfBirth { get; set; }
        public EmailAddress Email { get; set; }
        public CustomerFirstName FirstName { get; set; }
        public CustomerGender Gender { get; set; }
        public CustomerLastName LastName { get; set; }
        public MobileNumber Mobile { get; set; }
        public PaymentSettings PaymentSettings { get; set; }
        public CustomerPreferences Preferences { get; set; }
        public CustomerReceiveNotifications ReceiveNotifications { get; set; }
        public CustomerReceiveNotificationViaEmail ReceiveNotificationsViaEmail { get; set; }
        public CustomerReceiveNotificationsViaInPlatformMessages ReceiveNotificationsViaInPlatformMessages { get; set; }
        public CustomerReceiveNotificationViaSMS ReceiveNotificationsViaSMS { get; set; }
        public CustomerReferrer Referrer { get; set; }
        public CustomerSecurity Security { get; set; }
        public CustomerTitle Title { get; set; }
        #region Nested Classes
        public class AgeRestriction : IField
        {
            public object[] AllowedValues { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Age";
            public string Validation { get; set; }
        }

        public class CustomerAddress
        {
            public CustomerAddressLine1 AddressLine1 { get; set; }
            public CustomerAddressLine2 AddressLine2 { get; set; }
            public CustomerAddressLine3 AddressLine3 { get; set; }
            public CustomerCityOrTown CityOrTown { get; set; }
            public CustomerPostalCode PostalCode { get; set; }

            public class CustomerAddressLine1 : IField
            {
                public object[] AllowedValues { get; set; }
                public bool IsMandatory { get; set; }
                public bool IsUsed { get; set; }
                public object MaxValue { get; set; }
                public object MinValue { get; set; }
                public string ObjectName => "Address Line 1";
                public string Validation { get; set; }
            }
            public class CustomerAddressLine2 : IField
            {
                public object[] AllowedValues { get; set; }
                public bool IsMandatory { get; set; }
                public bool IsUsed { get; set; }
                public object MaxValue { get; set; }
                public object MinValue { get; set; }
                public string ObjectName => "Address Line 2";
                public string Validation { get; set; }
            }
            public class CustomerAddressLine3 : IField
            {
                public object[] AllowedValues { get; set; }
                public bool IsMandatory { get; set; }
                public bool IsUsed { get; set; }
                public object MaxValue { get; set; }
                public object MinValue { get; set; }
                public string ObjectName => "Address Line 3";
                public string Validation { get; set; }
            }
            public class CustomerCityOrTown : IField
            {
                public object[] AllowedValues { get; set; }
                public bool IsMandatory { get; set; }
                public bool IsUsed { get; set; }
                public object MaxValue { get; set; }
                public object MinValue { get; set; }
                public string ObjectName => "City Or Town";
                public string Validation { get; set; }
            }
            public class CustomerPostalCode : IField
            {
                public object[] AllowedValues { get; set; }
                public bool IsMandatory { get; set; }
                public bool IsUsed { get; set; }
                public object MaxValue { get; set; }
                public object MinValue { get; set; }
                public string ObjectName => "Postal Code";
                public string Validation { get; set; }
            }
        }

        public class CustomerBonusCode : IField
        {
            public object[] AllowedValues { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Bonus Code";
            public string Validation { get; set; }
        }

        public class CustomerCountry : IField
        {
            public object[] AllowedValues { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Country";
            public string Validation { get; set; }
        }

        public class CustomerDateOfBirth : IField
        {
            public object[] AllowedValues { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Date of Birth";
            public string Validation { get; set; }
        }

        public class CustomerFirstName : IField
        {
            public object[] AllowedValues { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "First Name";
            public string Validation { get; set; }
        }

        public class CustomerGender : IField
        {
            public object[] AllowedValues { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Gender";
            public string Validation { get; set; }
        }

        public class CustomerLastName : IField
        {
            public object[] AllowedValues { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Last Name";
            public string Validation { get; set; }
        }

        public class CustomerPreferences
        {
            public CustomerDefaultCurrency DefaultCurrency { get; set; }
            public CustomerDefaultLanguage DefaultLanguage { get; set; }
            public CustomerOddsDisplay OddsDisplay { get; set; }
            public CustomerTimeZone TimeZone { get; set; }

            public class CustomerDefaultCurrency : IField
            {
                public object[] AllowedValues { get; set; }
                public bool IsMandatory { get; set; }
                public bool IsUsed { get; set; }
                public object MaxValue { get; set; }
                public object MinValue { get; set; }
                public string ObjectName => "Default Currency";
                public string Validation { get; set; }
            }
            public class CustomerDefaultLanguage : IField
            {
                public object[] AllowedValues { get; set; }
                public bool IsMandatory { get; set; }
                public bool IsUsed { get; set; }
                public object MaxValue { get; set; }
                public object MinValue { get; set; }
                public string ObjectName => "Default Language";
                public string Validation { get; set; }
            }
            public class CustomerOddsDisplay : IField
            {
                public object[] AllowedValues { get; set; }
                public bool IsMandatory { get; set; }
                public bool IsUsed { get; set; }
                public object MaxValue { get; set; }
                public object MinValue { get; set; }
                public string ObjectName => "Odds Display";
                public string Validation { get; set; }
            }

            public class CustomerTimeZone : IField
            {
                public object[] AllowedValues { get; set; }
                public bool IsMandatory { get; set; }
                public bool IsUsed { get; set; }
                public object MaxValue { get; set; }
                public object MinValue { get; set; }
                public string ObjectName => "Time Zone";
                public string Validation { get; set; }
            }
        }

        public class CustomerReceiveNotifications : IField
        {
            public object[] AllowedValues { get { return new object[] { true, false }; } set { } }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Receive Notifications";
            public string Validation { get; set; }
        }

        public class CustomerReceiveNotificationsViaInPlatformMessages : IField
        {
            public object[] AllowedValues { get { return new object[] { true, false }; } set { } }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Receive Notifications Via In-Platform Messages";
            public string Validation { get; set; }
        }

        public class CustomerReceiveNotificationViaEmail : IField
        {
            public object[] AllowedValues { get { return new object[] { true, false }; } set { } }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Receive Notification Via Email";
            public string Validation { get; set; }
        }

        public class CustomerReceiveNotificationViaSMS : IField
        {
            public object[] AllowedValues { get { return new object[] { true, false }; } set { } }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Receive Notification Via SMS";
            public string Validation { get; set; }
        }

        public class CustomerReferrer : IField
        {
            public object[] AllowedValues { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Referrer";
            public string Validation { get; set; }
        }

        public class CustomerSecurity
        {
            public CustomerPassword Password { get; set; }
            public CustomerUsername Username { get; set; }

            public class CustomerPassword : IFieldMandatory
            {
                public object[] AllowedValues { get; set; }
                public bool IsMandatory => true;
                bool IField.IsMandatory { get; set; }
                public bool IsUsed => true;
                bool IField.IsUsed { get; set; }
                public object MaxValue { get; set; }
                public object MinValue { get; set; }
                public string ObjectName => "Password";
                public string Validation { get; set; }
            }

            public class CustomerUsername : IField
            {
                public object[] AllowedValues { get; set; }
                public bool IsMandatory { get; set; }
                public bool IsUsed { get; set; }
                public object MaxValue { get; set; }
                public object MinValue { get; set; }
                public string ObjectName => "Username";
                public string Validation { get; set; }
            }
        }

        public class CustomerTitle : IField
        {
            public object[] AllowedValues { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Title";
            public string Validation { get; set; }
        }

        public class EmailAddress : IField
        {
            public object[] AllowedValues { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Email Address";
            public string Validation { get; set; }
            public bool ValidationRequired { get; set; }
        }

        public class MobileNumber : IField
        {
            public object[] AllowedValues { get; set; }
            public bool IsMandatory { get; set; }
            public bool IsUsed { get; set; }
            public object MaxValue { get; set; }
            public object MinValue { get; set; }
            public string ObjectName => "Mobile Number";
            public string Validation { get; set; }
            public bool ValidationRequired { get; set; }
        }
        #endregion
    }
    public class TenantUser
    {
        public int AssignedUmbracoUserId { get; set; }
        public string Email { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string TenantUId { get; set; }
        public string Username { get; set; }
        public int[] StartContentIds { get; set; }
        public int[] StartMediaIds { get; set; }
        public static implicit operator TenantUser(Tenant tenant)
        {
            return new TenantUser
            {
                Username = tenant.Username,
                Name = tenant.Name,
                Email = tenant.Email,
                Password = tenant.Password,
                Group = tenant.Group,
                TenantUId = tenant.TenantUId
            };
        }

        public static implicit operator TenantUser(ExtendedTenant tenant)
        {
            return new TenantUser
            {
                Username = tenant.Tenant.Username,
                Name = tenant.Tenant.Name,
                Email = tenant.Tenant.Email,
                Password = tenant.Tenant.Password,
                Group = tenant.Tenant.Group,
                TenantUId = tenant.Tenant.TenantUId,
                AssignedUmbracoUserId = tenant.UserId,
                StartContentIds = new int[1]{ tenant.StartContentId},
                StartMediaIds = new int[1] { tenant.StartMediaId },
            };
        }
    }
}
