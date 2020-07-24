namespace Umbraco.Plugins.Connector.Models
{
    #region Api Response Error Codes
    public enum ApiPayloadErrorCodes
    {
        InvalidLogin = 1,
        AccountLocked = 2,
        Other = 4,
        UsernameCannotBeEmailAddress = 111,
        MissingField = 112,
        InvalidEmailFormat = 113,
        InvalidDate = 114,
        InvalidAge = 115,
        MobileOrEmailRequired = 116,
        InvalidCountry = 117,
        InvalidCurrency = 118,
        InvalidLanguage = 119,
        InvalidTimeZone = 120,
        ExistingCustomer = 121,
        EmailNotFound = 122,
        EmailSendFail = 123,
        EmailAlreadyVerified = 124,
        InvalidCustomer = 125,
        InvalidOldPassword = 126,
        MatchingOldAndNewPassword = 127,
        InvalidCustomerStatus = 128,
        CustomerNotFound = 129,
        VerificationRecordNotFound = 130,
        VerificationEmailExpired = 131,
        ValidationCodeExpired = 133,
        ValidationCodeSendFail = 134,
        SMSSendFail = 135,
        InvalidMobileNumber = 136,
        InvalidVerificationEmail = 137,
        VerificationCodeLimitExceeded = 138,
        MobileNumberNotFound = 139,
        ValidationCodeInvalid = 140,
        InvalidPassword = 141,
        ExceedsMaximalWithdrawal = 179,
        InvalidCard = 188,
        InvalidIBAN = 189,
        InsufficientBalance = 196,
        ConnectionTimeout = 999,
        InternalServerError = 1000,
        UnhandledError = 1001,
        UnAuthorized = 1002,
        BadRequest = 1003,
        UndefinedCustomer = 129,
        InvalidUsername= 111,
        MatchingUsername= 142
    }
    public enum ApiErrorCodes
    {
        UnableToCreateCustomer = 100,
        UnableToLoginCustomer = 101,
        UnableToEditCustomer = 102,
        UnableToDeleteCustomer = 103,
        ErrorSendindSms = 104,
        ErrorSendingEmail = 105,
        UnhandledError = 500
    }
    public enum ApiErrorReasonCodes
    {
        InvalidEmailFormat = 113,
        InvalidDate = 114,
        InvalidAge = 115,
        MobileOrEmailRequired = 116,
        InvalidCountry = 117,
        InvalidCurrency = 118,
        InvalidLanguage = 119,
        InvalidTimeZone = 120,
        ExistingCustomer = 121,
        PhoneAlreadyRegistered = 122,
        CannotLoginCustomerDoesNotExist = 123,
        CannotLoginCustomerLockedOut = 124,
        EmailAlreadyRegistered = 125,

        MissingFieldPhone = 201,
        MissingFieldEmail = 202,
        MissingFieldCountry = 203,
        MissingFieldLanguage = 204,
        MissingFieldDateOfBirth = 205,
        MissingFieldAddress = 206,
        MissingFieldOdds = 207,
        MissingFieldFractional = 208,
        MissingFieldPassword = 209,
    }
    #endregion
}
