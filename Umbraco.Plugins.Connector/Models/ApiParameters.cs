namespace Umbraco.Plugins.Connector.Models
{
    using System;
    using System.Collections.Generic;
    using Umbraco.Plugins.Connector.Interfaces;

    #region Parameters
    public class ApiKeyLogin : IParameter
    {
        public string ApiKey { get; set; }
        public string PlatformGuid { get; set; }
    }
    public class ConfirmEmail : IParameter
    {
        public string Email { get; set; }
        //public string TenantPlatformMapGuid { get; set; }
        //public string VerificationCode { get; set; }
        public string VerificationDate { get; set; }
        public string VerificationUrl { get; set; }
    }
    public class ChangeEmail : IParameter
    {
        public string Email { get; set; }
        public string Username { get; set; }

    }
    public class CustomerRegister : IParameter, ICustomerFields
    {
        public string County { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string PostCode { get; set; }
        public string Title { get; set; }
        public string Town { get; set; }
        public string Username { get; set; }
        public string BonusCode { get; set; }
        public string Referrer { get; set; }
        public string OddsDisplay { get; set; }
        public string NotificationComPref { get; set; }
        public string EmailComPref { get; set; }
        public string TextMessageComPref { get; set; }
        public string InPlatformComPref { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public string LanguageCode { get; set; }
        public string TimeZoneCode { get; set; }
        public string Country { get; set; }
    }
    public class Login : IParameter
    {
        public string Credential { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        // public string TenantPlatformMapGuid { get; set; }
    }
    public class LoginForm : IParameter
    {
        public string Credential { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string authType { get; set; }
    }
    public class ResetPasswordEmail : IParameter
    {
        public string NewPassword { get; set; }
        //public string TenantPlatformMapGuid { get; set; }
        public string Username { get; set; }
        public string VerificationUrl { get; set; }
    }
    public class ResetPasswordSms : IParameter
    {
        public string NewPassword { get; set; }
        //public string TenantPlatformMapGuid { get; set; }
        public string Username { get; set; }
        public string VerificationCode { get; set; }
    }
    public class ResetPasswordVerificationEmail : IParameter
    {
        public string Email { get; set; }
        public string LanguageCode { get; set; }
        //public string TenantPlatformMapGuid { get; set; }
        public string Username { get; set; }
        public string VerificationUrl { get; set; }

    }
    public class ResetPasswordVerificationSms : IParameter
    {
        public string LanguageCode { get; set; }
        public string MobileNumber { get; set; }
        //public string TenantPlatformMapGuid { get; set; }
        public string Username { get; set; }
    }
    public class RetrieveUsernameViaEmail : IParameter
    {
        public string Email { get; set; }
        public string LanguageCode { get; set; }
        //public string TenantPlatformMapGuid { get; set; }
    }
    public class RetrieveUsernameViaSms : IParameter
    {
        public string LanguageCode { get; set; }
        public string MobileNumber { get; set; }
        //public string TenantPlatformMapGuid { get; set; }
    }
    public class ValidateVerificationCode : IParameter
    {
        public string Mobile { get; set; }
        //public string TenantPlatformMapGuid { get; set; }
        public string VerificationCode { get; set; }
        public string VerificationDate { get; set; }
    }
    public class VerifyEmail : IParameter
    {
        public string Email { get; set; }
        //public string TenantPlatformMapGuid { get; set; }
        public string VerificationUrl { get; set; }
    }
    public class VerifyMobile : IParameter
    {
        public string LanguageCode { get; set; }
        public string Mobile { get; set; }
        public string RequestDate { get; set; }
        //public string TenantPlatformMapGuid { get; set; }
    }
    public class CustomerInfo : IParameter
    {
        public string Username { get; set; }
    }
    public class CustomerAccountBalance : IParameter
    {
        public string CustomerGuid { get; set; }
    }
    public class CustomerPaymentSystems : IParameter
    {
        public string CustomerGuid { get; set; }
        public string TransactionType { get; set; }
    }
    public class ActiveCardParams : IParameter
    {
        public string CustomerGuid { get; set; }
        public string Action { get; set; }
    }
    public class ExchangeRequestParams : IParameter
    {
        public string PaymentSystemName { get; set; }
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
    }
    public class AddIban : IParameter
    {
        public string CustomerGuid { get; set; }
        public string CardNumber { get; set; }
        public string Iban { get; set; }
    }
    public class AddCard : IParameter
    {
        public string AccountHolderName { get; set; }
        public string CardNumber { get; set; }
        public string Iban { get; set; }
        public string CustomerGuid { get; set; }
    }
    public class UpdateCard : IParameter
    {
        public string CardNumber { get; set; }
        public string CustomerGuid { get; set; }
        public string Iban { get; set; }
        public UpdateCardResponse Response { get; set; }

        public class UpdateCardResponse
        {
            public string Action { get; set; }
            public string Comment { get; set; }
            public int OperationUserId { get; set; }
        }
    }
    public class DeleteCard : IParameter
    {
        public string CustomerGuid { get; set; }
        public string CardNumber { get; set; }
    }
    public class Deposit : IParameter
    {
        public string PaymentIdentifier { get; set; }
        public string PaymentSystemName { get; set; }
        public string CustomerGuid { get; set; }
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
        public string RedirectUrl { get; set; }
    }

    public class Withdraw : IParameter
    {
        public string CustomerGuid { get; set; }
        public string PaymentIdentifier { get; set; }
        public string PaymentSystemName { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }

    public class WithdrawBitcoin : IParameter
    {
        public string CustomerGuid { get; set; }
        public string PaymentIdentifier { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }

    public class DepositTransaction: IParameter
    {
        public Guid? CustomerGuid { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
    }
    public class WithdrawTransaction : IParameter
    {
        public Guid? CustomerGuid { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
    }
    public class BonusTransaction : IParameter
    {
        public Guid? CustomerGuid { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
    public class EditMobileNumber : IParameter
    {
        public string Username { get; set; }
        public string Mobile { get; set; }
    }
    public class EditEmail : IParameter
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public string VerificationUrl { get; set; }
    }
    public class EditCustomer : IParameter
    {
        public string Username { get; set; }
        public EditFields Fields { get; set; }

        public class EditFields : ICustomerFields
        {
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string AddressLine3 { get; set; }
            public string CountryCode { get; set; }
            public string County { get; set; }
            public string CurrencyCode { get; set; }
            public string DOB { get; set; }
            public string FirstName { get; set; }
            public string Gender { get; set; }
            public string LanguageCode { get; set; }
            public string LastName { get; set; }
            public string TimeZoneCode { get; set; }
            public string Title { get; set; }
            public string Town { get; set; }
            public string OddsDisplay { get; set; }
            public string NotificationComPref { get; set; }
            public string PostCode { get; set; }
            public string Country { get; set; }
        }
    }
    public class EditCommunicationPreferences : IParameter
    {
        public string Username { get; set; }
        public CommunicationPreferenceParameter[] CommunicationPreferences { get; set; }
    }
    public class EditPassword : IParameter
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class GameData : IParameter{
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Provider { get; set; }
        public string Keyword { get; set; }
    }
    public class GameById : IParameter
    {
        public string GameIdentifier { get; set; }
        public int GameId { get; set; }
    }

    public class PokerLobby : IParameter
    {
        public string Origin { get; set; }
        public string CustomerToken { get; set; }
    }

    public class HubUserId : IParameter
    {
        public string UserId { get; set; }
    }
    public class EditUsername : IParameter
    {
        public string CustomerGuid { get; set; }
        public string Username { get; set; }
    }
    public class AnonymousUser : IParameter
    {
        public string tenantUId { get; set; }
    }
    #endregion

    #region Content Parameters
    public class CommunicationPreferenceParameter
    {
        public string Communication { get; set; }
        public bool IsSelected { get; set; }
    } 
    #endregion
}
