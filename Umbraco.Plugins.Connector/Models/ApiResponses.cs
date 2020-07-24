namespace Umbraco.Plugins.Connector.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Plugins.Connector.Interfaces;

    #region Responses
    public class ApiKeyLoginResponseContent : IResponseContent
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class ConfirmEmailResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public int? Customer { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class ChangeEmailResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public int? Customer { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class CustomerRegisterResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError[] Errors { get; set; }
        public PayloadContent Payload { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class LoginResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError Errors { get; set; }
        public string Credential { get; set; }
        public string Token { get; set; }
        public int Expires { get; set; }
        public int? Customer { get; set; }
        public DateTime? LastLogin { get; set; }
        public int ValidationCode { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class ResetPasswordEmailResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public int? Customer { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class ResetPasswordSmsResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public int? Customer { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class ResetPasswordVerificationEmailResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public int? Customer { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class ResetPasswordVerificationSmsResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public int? Customer { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class RetrieveUsernameResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public int? Customer { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class RetrieveUsernameViaEmailResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public int? Customer { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class ValidateVerificationCodeResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public int? Customer { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class VerifyEmailResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public int? Customer { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class VerifyMobileResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class ResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public int? Customer { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }

    public class GetCustomerEmailResponseCOntent :IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError[] Errors { get; set; }
        public PayloadEmail[] Payload { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class PayloadEmail
    {
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class GetCustomerInfoResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError[] Errors { get; set; }
        public PayloadContent Payload { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class GetCustomerAccountBalanceResponseContent : IResponseContent
    {
        // Sept 17, 2019 {"success":false,"message":"Fail","payload":null,"errors":{"errorCode":181,"errorMessage":"Undefined wallet!"}}
        public bool Success { get; set; }
        public ResponseContentError Errors { get; set; }
        public AccountBalanceContent Payload { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class GetTicketResponseContent : IResponseContent
    {

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
    }
    public class AddCardResponseContent : IResponseContent
    {
        public string Success { get; set; }
        public Error Errors { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
    }
    public class UpdateCardResponseContent : IResponseContent
    {
        public string Success { get; set; }
        public UpdateCardPayload Payload { get; set; }

        public Error Errors { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }

        public class UpdateCardPayload
        {
            public string CustomerGuid { get; set; }
            public string CardNumber { get; set; }
            public int CardStatus { get; set; }
        }
    }
    public class ActiveCardResponseContent : IResponseContent
    {
        public string Success { get; set; }
        public ActiveCardPayload Payload { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }

        public class ActiveCardPayload
        {
            public string CustomerGuid { get; set; }
            public Card[] ActiveCards { get; set; }
        }
    }

    public class GetCardResponseContent : IResponseContent
    {
        public string Success { get; set; }
        public Card[] Payload { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }

        public class Card
        {
            public int Id { get; set; }
            public string CustomerGuid { get; set; }
            public string CardNumber { get; set; }
            public string Status { get; set; }
        }
    }

    public class DeleteCardResponseContent : IResponseContent
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
    }
    public class DepositResponseContent : IResponseContent
    {
        public Error Errors { get; set; }
        public DepositPayload Payload { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }

        public class DepositPayload
        {
            public int TransactionId { get; set; }
            public string TransactionGuid { get; set; }
            public string PaymentUrl { get; set; }
            public string RedirectType { get; set; }
        }
    }

    public class CurrencyResponseContent : IResponseContent
    {
        public decimal Value { get; set; }
        public string PaymentSystemName { get; set; }
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
        public DateTime CreateDate { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }

    public class DepositPerfectMoneyResponseContent : IResponseContent
    {
        public Error Errors { get; set; }
        public DepositPerfectMoneyPayload Payload { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }

        public class DepositPerfectMoneyPayload
        {
            public int TransactionId { get; set; }
            public string TransactionGuid { get; set; }
            public string PaymentUrl { get; set; }
            public string RedirectType { get; set; }
            public DepositPayloadParameters Parameters { get; set; }

            public class DepositPayloadParameters
            {
                public string PAYEE_ACCOUNT { get; set; }
                public string PAYEE_NAME { get; set; }
                public string PAYMENT_ID { get; set; }
                public string PAYMENT_AMOUNT { get; set; }
                public string PAYMENT_UNITS { get; set; }
                public string STATUS_URL { get; set; }
                public string PAYMENT_URL { get; set; }
                public string PAYMENT_URL_METHOD { get; set; }
                public string NOPAYMENT_URL { get; set; }
                public string NOPAYMENT_URL_METHOD { get; set; }
                public string SUGGESTED_MEMO { get; set; }
                public string ev_number { get; set; }
                public string ev_code { get; set; }
                public string Token { get; set; }
                public string RedirectURL { get; set; }
            }

        }
    }

    public class DepositBitcoinResponseContent : IResponseContent
    {
        public Error Errors { get; set; }
        public DepositBitcoinPayload Payload { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }

        public class DepositBitcoinPayload
        {
            public int TransactionId { get; set; }
            public string TransactionGuid { get; set; }
            public string PaymentUrl { get; set; }
            public string RedirectType { get; set; }
            public DepositPayloadParameters Parameters { get; set; }

            public class DepositPayloadParameters
            {
                public string Address { get; set; }
            }

        }
    }

    public class WithdrawResponseContent : IResponseContent
    {
        public Error Errors { get; set; }
        public WithdrawPayload Payload { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }

        public class WithdrawPayload
        {
            public int TransactionId { get; set; }
            public string TransactionGuid { get; set; }
        }
    }
    public class DepositTransactionResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public DepositTransactionPayload[] Payload { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion

        public class DepositTransactionPayload
        {
            public int Id { get; set; }
            public string TransactionGuid { get; set; }
            public DateTime TransactionDate { get; set; }
            public decimal TransactionAmount { get; set; }
            public int TransactionStatus { get; set; }
            public string TransactionStatusStr { get; set; }
            public string PaymentSystem { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
    }
    public class WithdrawTransactionResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public WithdrawTransactionPayload[] Payload { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion

        public class WithdrawTransactionPayload
        {
            public int Id { get; set; }
            public string TransactionGuid { get; set; }
            public DateTime TransactionDate { get; set; }
            public decimal TransactionAmount { get; set; }
            public int TransactionStatus { get; set; }
            public string TransactionStatusStr { get; set; }
            public string PaymentSystem { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
    }
    public class BonusTransactionResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public BonusTransactionPayload[] Payload { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion

        public class BonusTransactionPayload
        {
            public string Name { get; set; }
            public string TypeStr { get; set; }
            public string StatusStr { get; set; }
            public DateTime? BonusApplied { get; set; }
            public string RedemptionStatusStr { get; set; }
            public DateTime? RedemptionDate { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public IList<BonusTransactionRedemptionData> RedemptionData { get; set; }
        }

        public class BonusTransactionRedemptionData
        {
            public string Name { get; set; }
            public string QualifierRequirement { get; set; }
            public string QualifierProgress { get; set; }
            public string QualifierDifference { get; set; }
        }
    }
    public class CancelWithdrawalResponseContent : IResponseContent
    {


        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion

        public CancelWithdrawalPayload Payload { get; set; }

        public class CancelWithdrawalPayload
        {
            public string TransactionGuid { get; set; }
            public int TransactionId { get; set; }
        }
    }
    public class EditCustomerResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError[] Errors { get; set; }
        public PayloadContent Payload { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class EditMobileNumberResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }

    public class EditUsernameResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class EditEmailResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class EditCommunicationPreferencesResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class EditPasswordResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError Errors { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }

    public class PokerLobbyResponseContent : IResponseContent
    {
        public bool Success { get;set; }
        public string RedirectUrl { get; set; }
        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; } 
        #endregion
    }

    public class GameLobbyResponseContent : IResponseContent
    {
        public bool Success { get;set; }
        public string Link { get; set; }
        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; } 
        #endregion
    }

    public class GameDataResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError Errors { get; set; }
        public GameDetails[] Payload { get; set; }

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class GameByIdResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public ResponseContentError Errors { get; set; }
        #region Game by Id Details
        public int GameId { get; set; }
        public string GameIdentifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public GameImage[] GameImages { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string GameProvider { get; set; }
        public string Url { get; set; }
        public int UrlType { get; set; }
        public UrlType UrlTypeEnum { get { return (UrlType)UrlType; } }
        public bool Favourite { get; set; }
        public bool NewGame { get; set; }
        public GameConfiguration[] Configurations { get; set; }
        public string ExternalId { get; set; }
        public bool DemoEnabled { get; set; }
        public string DemoUrl { get; set; }
        #endregion

        #region Interface Objects
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    public class HubConnectionDetailsResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }

        #region Interface Properties
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion

    }

    public class PaymentSystemsDetailsResponseContent : IResponseContent
    {
        public bool Success { get; set; }
        public PaymentId[] PaymentIds { get; set; }

        #region Interface Properties
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion

    }

    public class PaymentId
    {
        public string PaymentIdentifier { get; set; }
        public bool IsDefault { get; set; }
        public int? Priority { get; set; }
    }

    public struct GameDetailsArrayResponseContent : IResponseContent
    {
        public int GameId { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public GameImage[] GameImages { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Provider { get; set; }
        public string Url { get; set; }
        public bool Favourite { get; set; }
        public bool New { get; set; }
        public GameConfiguration[] Configurations { get; set; }
        #region Interface Properties
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IResponseContent RelatedResponse { get; set; }
        #endregion
    }
    #endregion

    #region Error Responses Objects
    public class ResponseContentError : IResponseContentError
    {
        // {"success":false,"message":"Fail","payload":null,"errors":{"errorCode":181,"errorMessage":"Undefined wallet!"}}
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
    #endregion

    #region Payload Object
    public class PayloadContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public PayloadPhoneNumber PhoneNumber { get; set; }
        public PayloadEmail Email { get; set; }
        public PayloadAddress Address { get; set; }
        //public PhoneNumber[] PhoneNumbers { get; set; }
        //public PhoneNumber PreferredNumber => PhoneNumbers.SingleOrDefault(x => x.IsPreferred);
        //public Email[] Emails { get; set; }
        //public Email PreferredEmail => Emails.SingleOrDefault(x => x.IsSelected);
        //public Address[] Addresses { get; set; }
        public CommunicationPreference[] CommunicationPreferences { get; set; }
        public CommunicationPreference NotificationPreferences => CommunicationPreferences.SingleOrDefault(x => x.Name.Equals("Notification"));
        public CommunicationPreference EmailNotificationPreferences => CommunicationPreferences.SingleOrDefault(x => x.Name.Equals("Email"));
        public CommunicationPreference SmsNotificationPreferences => CommunicationPreferences.SingleOrDefault(x => x.Name.Equals("TextMessage"));
        public CommunicationPreference InPlatformNotificationPreferences => CommunicationPreferences.SingleOrDefault(x => x.Name.Equals("InPlatformMessage"));
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? PasswordUpdatedAt { get; set; }
        public Guid? CustomerGuid { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public string LanguageCode { get; set; }
        public string TimeZoneCode { get; set; }
        public int OddsDisplay { get; set; }
        public string BonusCode { get; set; }
        public string Referrer { get; set; }
        public object CustomerCustomFieldDtos { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public class PayloadPhoneNumber
        {
            public int CustomerId { get; set; }
            public int Category { get; set; }
            public string Number { get; set; }
            public bool IsPreferred { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
        public class PayloadEmail
        {
            public int CustomerId { get; set; }
            public string EmailAddress { get; set; }
            public bool IsSelected { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
        public class PayloadAddress
        {
            public int CustomerId { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string AddressLine3 { get; set; }
            public string Town { get; set; }
            public string PostCode { get; set; }
            public string Country { get; set; }
            public bool IsSelected { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
        public class CommunicationPreference
        {
            public int CustomerId { get; set; }
            public int CommunicationType { get; set; }
            public string Name { get; set; }
            public bool IsSelected { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }

        }
    }

    public class AccountBalanceContent
    {
        public string TenantPlatformMapGuid { get; set; }
        public int TenantId { get; set; }
        public Guid? CustomerGuid { get; set; }
        public decimal TotalAccountBalance { get; set; }
        public decimal WithdrawableBalance { get; set; }
        public decimal BonusBalance { get; set; }
        public Wallet[] Wallets { get; set; }

    }
    public class Wallet
    {
        public int Id { get; set; }
        public string WalletGuid { get; set; }
        public Guid? CustomerGuid { get; set; }
        public string ProductCode { get; set; }
        public decimal TotalAccountBalance { get; set; }
        public decimal WithdrawableBalance { get; set; }
        public decimal BonusBalance { get; set; }
        public string CurrencyCode { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Customer { get; set; }
    }

    public class CustomerWallet
    {
        public Guid? CustomerGuid { get; set; }
        public string TotalAccountBalance { get; set; }
        public string WithdrawableBalance { get; set; }
        public string BonusBalance { get; set; }
        public string CurrencyCode { get; set; }
    }
    #endregion

    #region JWT Response
    public class JwtCustomerDataResponseContent
    {
        public string TenantPlatformMapGuid { get; set; }
        public string LanguageCode { get; set; }
        public string CurrencyCode { get; set; }
        public string OddsDisplay { get; set; }
        public string TimeZone { get; set; }
        public Guid? CustomerGuid { get; set; }
        public string Username { get; set; }
        public string EncodedToken { get; set; }
    }
    #endregion
}
