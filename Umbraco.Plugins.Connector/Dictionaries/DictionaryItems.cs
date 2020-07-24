namespace Umbraco.Plugins.Connector.Dictionaries
{
    using System.Collections.Generic;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Interfaces;
    #region Site and Registration
    public class Site_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Site";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Site_AlreadyHaveAccount : IDictionaryItem
    {
        public string ParentKey => "Site";

        public string Key => "[Site]AlreadyHaveAccount";

        public string Value => "Already have an account?";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Home_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Home Page";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Home_Register : IDictionaryItem
    {
        public string ParentKey => "Home Page";

        public string Key => "[Home Page]Register Link";

        public string Value => "Register";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            { Models.Country.EnglishUnitedKingdom.GetCountryLanguageIsoCode(), "Register" },
            { Models.Country.SpanishSpainInternationalSort.GetCountryLanguageIsoCode(), "Registrarse" }
        };
    }
    public class Register_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Register";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_RegisterTitle : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]RegisterTitle";

        public string Value => "Registration";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_RegisterStep : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]RegisterStep";

        public string Value => "Step";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_RegisterOf : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]RegisterOf";

        public string Value => "of";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_PhoneNumber : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]PhoneNumber";

        public string Value => "Phone Number";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_PhoneNumberPlaceholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]PhoneNumberPlaceholder";

        public string Value => "555-5555";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_IsMandatory : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]IsMandatory";

        public string Value => " is required";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Continue : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Continue";

        public string Value => "Continue";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_PhoneNumberInstructions : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]PhoneNumberInstructions";

        public string Value => "Enter your mobile phone number and we will send you a verification code";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_VerificationCode : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]VerificationCode";

        public string Value => "Verification Code";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_VerificationCodePlaceholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]VerificationCodePlaceholder";

        public string Value => "XXXXXX";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_EnterCode : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]EnterCode";

        public string Value => "Enter Code";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Enter6DigitVerificationCode : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Enter6DigitVerificationCode";

        public string Value => "Enter the 6-digit verification code sent to ";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_EnterVerificationCode : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]EnterVerificationCode";

        public string Value => "Enter the verification code sent to ";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_ResendCode : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]ResendCode";

        public string Value => "Resend Code";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Wait : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Wait";

        public string Value => "wait ";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_VerificationCodeInvalidOrExpired : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]VerificationCodeInvalidOrExpired";

        public string Value => "Verification code is invalid or expired";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Email : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Email";

        public string Value => "Email";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_EmailPlaceholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]EmailPlaceholder";

        public string Value => "johndoe@email.com";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Password : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Password";

        public string Value => "Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_PasswordPlaceholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]PasswordPlaceholder";

        public string Value => "***********";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_ConfirmPassword : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]ConfirmPassword";

        public string Value => "Confirm Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_FirstName : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]FirstName";

        public string Value => "First Name";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_FirstNamePlaceholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]FirstNamePlaceholder";

        public string Value => "John";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_LastName : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]LastName";

        public string Value => "Last Name";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_LastNamePlaceholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]LastNamePlaceholder";

        public string Value => "Doe";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Username : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Username";

        public string Value => "Username";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_UsernamePlaceholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]UsernamePlaceholder";

        public string Value => "johndoe";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Gender : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Gender";

        public string Value => "Gender";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirth : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirth";

        public string Value => "Date of Birth";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthDay : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthDay";

        public string Value => "Day";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonth : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonth";

        public string Value => "Month";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthJanuary : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthJanuary";

        public string Value => "January";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthFebruary : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthFebruary";

        public string Value => "February";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthMarch : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthMarch";

        public string Value => "March";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthApril : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthApril";

        public string Value => "April";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthMay : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthMay";

        public string Value => "May";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthJune : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthJune";

        public string Value => "June";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthJuly : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthJuly";

        public string Value => "July";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthAugust : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthAugust";

        public string Value => "August";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthSeptember : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthSeptember";

        public string Value => "September";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthOctober : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthOctober";

        public string Value => "October";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthNovember : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthNovember";

        public string Value => "November";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthMonthDecember : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthMonthDecember";

        public string Value => "December";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_DateOfBirthYear : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]DateOfBirthYear";

        public string Value => "Year";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_IAgreeWithThe : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]IAgreeWithThe";

        public string Value => "I agree with the";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_TermsAndConditions : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]TermsAndConditions";

        public string Value => "terms and conditions";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Finish : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Finish";

        public string Value => "Finish";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_IncorrectOrInvalid : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]IncorrectOrInvalid";

        public string Value => " is incorrect or invalid";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_VerifyEmailSentToTitle : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]VerifyEmailSentToTitle";

        public string Value => "Email verification has been sent to your email";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_VerifyEmailDone : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]VerifyEmailDone";

        public string Value => "Email verification completed!";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_VerifyEmailSentTo : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]VerifyEmailSentTo";

        public string Value => "We now need to verify your email address. We’ve sent an email to";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_VerifyEmailSentToPleaseClick : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]VerifyEmailSentToPleaseClick";

        public string Value => "to verify your address. Please click the link in that email to continue.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_ResendVerificationEmail : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]ResendVerificationEmail";

        public string Value => "Resend Verification Email";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_ResendVerificationEmailSent : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]ResendVerificationEmailSent";

        public string Value => "New verification email has been sent";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_ChangeEmail : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]ChangeEmail";

        public string Value => "Change Email Address";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_NewEmail : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]NewEmail";

        public string Value => "New Email Address";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_CurrentEmail : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]CurrentEmail";

        public string Value => "Your current email address is";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_IsInvalid : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]IsInvalid";

        public string Value => " is invalid";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Title : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Title";

        public string Value => "Title";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Address1 : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Address1";

        public string Value => "Address";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Address1Placeholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Address1Placeholder";

        public string Value => "123 Sesame St";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Address2 : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Address2";

        public string Value => "Address 2";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Address2Placeholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Address2Placeholder";

        public string Value => "Apt. 33";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Address3 : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Address3";

        public string Value => "Address 3";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Address3Placeholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Address3Placeholder";

        public string Value => "block b";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_CityOrTown : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]CityOrTown";

        public string Value => "Town";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_CityOrTownPlaceholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]CityOrTownPlaceholder";

        public string Value => "Miami";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_PostalCode : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]PostalCode";

        public string Value => "Postal Code";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_PostalCodePlaceholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]PostalCodePlaceholder";

        public string Value => "33324";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Country : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Country";

        public string Value => "Country";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Preferences : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Preferences";

        public string Value => "Preferences";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Language : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Language";

        public string Value => "Language";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Currency : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Currency";

        public string Value => "Currency";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_OddsDisplay : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]OddsDisplay";

        public string Value => "Odds";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_TimeZone : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]TimeZone";

        public string Value => "Time Zone";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_BonusCode : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]BonusCode";

        public string Value => "Bonus Code";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_BonusCodePlaceholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]BonusCodePlaceholder";

        public string Value => "Bonus Code";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Referrer : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Referrer";

        public string Value => "Referrer";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_ReferrerPlaceholder : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]ReferrerPlaceholder";

        public string Value => "Referrer";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_ReceiveNotifications : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]ReceiveNotifications";

        public string Value => "Receive Notifications";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_ReceiveNotificationsViaInPlatformMessages : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]ReceiveNotificationsViaInPlatformMessages";

        public string Value => "Receive Notifications via In-Platform";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_ReceiveNotificationsViaEmail : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]ReceiveNotificationsViaEmail";

        public string Value => "Receive Notifications via Email";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_ReceiveNotificationsViaSMS : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]ReceiveNotificationsViaSMS";

        public string Value => "Receive Notifications via SMS";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_CookiesPolicy : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]CookiesPolicy";

        public string Value => "Cookies Policy";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_PrivacyPolicy : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]PrivacyPolicy";

        public string Value => "Privacy Policy";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_MinimunAge : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]MinimunAge";

        public string Value => "I am at least";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_Age : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]Age";

        public string Value => "Minimum Age";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Register_AgeYearsOld : IDictionaryItem
    {
        public string ParentKey => "Register";

        public string Key => "[Register]AgeYearsOld";

        public string Value => " years old";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    #endregion

    #region Renante Website Keys
    public class Others_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Others";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Deposit : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Deposit";

        public string Value => "Deposit";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Login : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Login";

        public string Value => "Login";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Successful : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Successful";

        public string Value => " Successful";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Reset_ResetPasswordFailure : IDictionaryItem
    {
        public string ParentKey => "Reset";

        public string Key => "[Reset]ResetPasswordFailure";

        public string Value => "Error resetting your password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Register : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Register";

        public string Value => "Register";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Search : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Search";

        public string Value => "Search";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ForgotPassword : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ForgotPassword";

        public string Value => "Forgot Password?";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Balance : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Balance";

        public string Value => "Balance";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Cash : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Cash";

        public string Value => "Cash";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Withdrawable : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Withdrawable";

        public string Value => "Withdrawable";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_BetCredits : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]BetCredits";

        public string Value => "Bet Credits";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Change : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Change";

        public string Value => "Change";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Edit : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Edit";

        public string Value => "Edit";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_MarketingPreferences : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]MarketingPreferences";

        public string Value => "Marketing Preferences";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ChangePassword : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ChangePassword";

        public string Value => "Change Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_WhatCanWeHelp : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]WhatCanWeHelp";

        public string Value => "What can we help you with?";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Back : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Back";

        public string Value => "Back";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_CreditCard : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]CreditCard";

        public string Value => "Credit Card";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Delete : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Delete";

        public string Value => "Delete";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_AddNewPaymentMethod : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]AddNewPaymentMethod";

        public string Value => "Add New Payment Method";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_PaymentMethod : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]PaymentMethod";

        public string Value => "Payment Method";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Status : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Status";

        public string Value => "Status";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_StartDate : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]StartDate";

        public string Value => "Start Date";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_EndDate : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]EndDate";

        public string Value => "End Date";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ApplyFilters : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ApplyFilters";

        public string Value => "Apply Filters";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ResetFilters : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ResetFilters";

        public string Value => "Reset Filters";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Filter : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Filter";

        public string Value => "Filter";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Rejected : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Rejected";

        public string Value => "Rejected";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_Accepted : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Accepted";

        public string Value => "Accepted";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_IDCertificate : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]IDCertificate";

        public string Value => "ID Certificate";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_UploadedDocumentRules : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]UploadedDocumentRules";

        public string Value => "Your Uploaded ID document is obstructed or does not have all 4 edges visible. Please ensure the entire ID document is clear and visible for the best results.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Verified : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Verified";

        public string Value => "Verified";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Passport : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Passport";

        public string Value => "Passport";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Upload : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Upload";

        public string Value => "Upload";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_FrontBack : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]FrontBack";

        public string Value => "Front and Back";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Pending : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Pending";

        public string Value => "Pending";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_BankStatement : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]BankStatement";

        public string Value => "Bank Statement";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_UtilityBill : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]UtilityBill";

        public string Value => "Utility Bill";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Receipt : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Receipt";

        public string Value => "Receipt";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_FirstAndLastName : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]FirstAndLastName";

        public string Value => "First and Last Name";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Date : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Date";

        public string Value => "Date";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Amount : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Amount";

        public string Value => "Amount";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_TransactionNumber : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]TransactionNumber";

        public string Value => "Transaction Number";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_WithdrawCancelled : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]The withdrawal has been successfully cancelled";

        public string Value => "The withdrawal has been successfully cancelled";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_CancelWithdrawConfirm : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]CancelWithdrawConfirm";

        public string Value => "Are you sure you want to cancel this withdraw?";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_GotIt : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]GotIt";

        public string Value => "Got It";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_AreYouSureWant : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]AreYouSureWant";

        public string Value => "Are you sure you want to ";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Cancel : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Cancel";

        public string Value => "cancel";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_This : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]This";

        public string Value => " this ";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Withdraw : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Withdraw";

        public string Value => "Withdraw";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Yes : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Yes";

        public string Value => "Yes";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_No : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]No";

        public string Value => "No";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_PasswordChanged : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]PasswordChanged";

        public string Value => "Password has been changed successfully";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_GetStarted : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]GetStarted";

        public string Value => "Get Started";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_NewPaymentMethodAdded : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]NewPaymentMethodAdded";

        public string Value => "New payment method has been successfully added";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_PaymentMethodUpdated : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]PaymentMethodUpdated";

        public string Value => "Payment method has been successfully updated.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_PaymentMethodDeleted : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]PaymentMethodDeleted";

        public string Value => "Payment method has been successfully deleted.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_TicketHasBeenClosed : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]TicketHasBeenClosed";

        public string Value => "The ticket has been closed";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_PasswordResetLinkSent : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]PasswordResetLinkSent";

        public string Value => "Password reset link has been sent to your email";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_PasswordResetLinkSentTo : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]PasswordResetLinkSentTo";

        public string Value => "We’ve sent a password reset link to";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ProceedToLogin : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ProceedToLogin";

        public string Value => "Proceed To Login";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_MessageSent : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]MessageSent";

        public string Value => "Message Sent";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ResetPassword : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ResetPassword";

        public string Value => "Reset Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ConfirmNewPassword : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ConfirmNewPassword";

        public string Value => "Confirm New Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_SaveNewPassword : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]SaveNewPassword";

        public string Value => "Save New Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ForgotUsernameEmail : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ForgotUsernameEmail";

        public string Value => "Forgot username or email?";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_EnterMobileSendUsername : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]EnterMobileSendUsername";

        public string Value => "Enter your mobile phone number and we will send you username and email related to your account";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_EnterEmailToSendLink : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]EnterEmailToSendLink";

        public string Value => "Enter your email and we will send you password reset link";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_AccountHolderName : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]AccountHolderName";

        public string Value => "Account Holder Name";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_CardNumber16Digits : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]CardNumber16Digits";

        public string Value => "16 digit card number";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ChangePhone : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ChangePhone";

        public string Value => "Change Phone";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_EditUserInfo : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]EditUserInfo";

        public string Value => "Edit User Info";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Male : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Male";

        public string Value => "Male";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Female : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Female";

        public string Value => "Female";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_UserInfoUpdated : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]UserInfoUpdated";

        public string Value => "User info has been successfully updated";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_EnterPasswordToChange : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]EnterPasswordToChange";

        public string Value => "account";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_SuccessChanged : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]SuccessChanged";

        public string Value => " has been successfully changed";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_PleaseEnterPasswordToChange : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]PleaseEnterPasswordToChange";

        public string Value => "Please enter your password to change ";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ChangeEmailVerifyNewEmail : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ChangeEmailVerifyNewEmail";

        public string Value => "To change your email please verify your new email address";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_WillNotBeReplacedUntil : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]WillNotBeReplacedUntil";

        public string Value => "The current email will not replaced by the new email until verification.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_NewPassword : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]NewPassword";

        public string Value => "New Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_WeWillRespondToEmail : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]WeWillRespondToEmail";

        public string Value => "We will respond to your email";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Within24Hours : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Within24Hours";

        public string Value => "account";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ContactUs : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ContactUs";

        public string Value => "Contact Us";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Subject : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Subject";

        public string Value => "Subject";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Message : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Message";

        public string Value => "Message";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Uploading : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Uploading";

        public string Value => "Uploading";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_AttachFile : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]AttachFile";

        public string Value => "Attach a File";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Send : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Send";

        public string Value => "Send";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_NewTicket : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]NewTicket";

        public string Value => "New Ticket";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_NewMessage : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]NewMessage";

        public string Value => "New Message";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Close : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Close";

        public string Value => "Close";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Ticket : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Ticket";

        public string Value => "Ticket";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Created : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Created";

        public string Value => "Created";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_WeWillRespondTo : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]WeWillRespondTo";

        public string Value => "We Will Respond To ";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_TicketSuccessClose : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]TicketSuccessClose";

        public string Value => "Ticket has been successfully closed";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Settings : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Settings";

        public string Value => "Settings";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Fractional : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Fractional";

        public string Value => "Fractional";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_ChoosePaymentMethod : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ChoosePaymentMethod";

        public string Value => "Choose Payment Method";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Exclusive : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Exclusive";

        public string Value => "Exclusive";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_AccessToExclusivePoints : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]AccessToExclusivePoints";

        public string Value => "Access to exclusive points and bonus bet offers from";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Rewards : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Rewards";

        public string Value => "Rewards";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_OtherPartners : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]OtherPartners";

        public string Value => "Other Partners";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_MarketResearch : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]MarketResearch";

        public string Value => "Market Research";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Tipping : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Tipping";

        public string Value => "Tipping";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Reply : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Reply";

        public string Value => "Reply";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_Type : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Type";

        public string Value => "Type";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_AmountToWithdraw : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]AmountToWithdraw";

        public string Value => "Amount you want to withdraw";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Others_WithdrawWarning : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]WithdrawWarning";

        public string Value => "Withdraw Warning";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_DepositBonusCode : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]DepositBonusCode";

        public string Value => "Deposit Bonus Code";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_TicketSentToAdmin : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]TicketSentToAdmin";

        public string Value => "Your ticket was sent to administrator for review.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_CloseTicket : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]CloseTicket";

        public string Value => "Close Ticket";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_CloseTicketConfirm : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]CloseTicketConfirm";

        public string Value => "Are you sure you want to close this ticket?";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_ClosedTicket : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ClosedTicket";

        public string Value => "Closed Ticket";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_ActiveTicket : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ActiveTicket";

        public string Value => "Active Ticket";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_Iban : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Iban";

        public string Value => "IBAN";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_DateAndTime : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]DateAndTime";

        public string Value => "Date & Time";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_Method : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Method";

        public string Value => "Method";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_Action : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Action";

        public string Value => "Action";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_InProgress : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]InProgress";

        public string Value => "In Progress";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_Completed : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Completed";

        public string Value => "Completed";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_Cancelled : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Cancelled";

        public string Value => "Cancelled";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_Failed : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Failed";

        public string Value => "Failed";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_YesCancel : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]YesCancel";

        public string Value => "Yes, Cancel";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_BitCoinDepositAddress : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]BitCoinDepositAddress";

        public string Value => "BitCoin Deposit Address";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_Gaming : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Gaming";

        public string Value => "Gaming";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_Extras : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Extras";

        public string Value => "Extras";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_Help : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Help";

        public string Value => "Help";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Others_CloseTicketMessage : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]CloseTicketMessage";

        public string Value => "Closed by customer";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    #endregion

    #region Login
    public class Login_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Login";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Login_EmailUsernameOrPhone : IDictionaryItem
    {
        public string ParentKey => "Login";

        public string Key => "[Login]EmailUsernameOrPhone";

        public string Value => "Credential";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Login_EmailUsernameOrPhonePlaceholder : IDictionaryItem
    {
        public string ParentKey => "Login";

        public string Key => "[Login]EmailUsernameOrPhonePlaceholder";

        public string Value => "email, username or mobile phone";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Login_ForgotPassword : IDictionaryItem
    {
        public string ParentKey => "Login";

        public string Key => "[Login]ForgotPassword";

        public string Value => "Forgot Password?";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Login_Password : IDictionaryItem
    {
        public string ParentKey => "Login";

        public string Key => "[Login]Password";

        public string Value => "Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Login_RememberMe : IDictionaryItem
    {
        public string ParentKey => "Login";

        public string Key => "[Login]RememberMe";

        public string Value => "Remember Me";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Login_Login : IDictionaryItem
    {
        public string ParentKey => "Login";

        public string Key => "[Login]Login";

        public string Value => "Login";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Login_DontHaveAccount : IDictionaryItem
    {
        public string ParentKey => "Login";

        public string Key => "[Login]DontHaveAccount";

        public string Value => "Don’t have an account?";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Login_LoginSuccess : IDictionaryItem
    {
        public string ParentKey => "Login";

        public string Key => "[Login]LoginSuccess";

        public string Value => "Login successful, please wait.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Login_LoginFailure : IDictionaryItem
    {
        public string ParentKey => "Login";

        public string Key => "[Login]LoginFailure";

        public string Value => "Login failure, please check your credentials";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }


    #endregion

    #region Forgot Password and Username
    public class Forgot_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Forgot";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_EnterCaptcha : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]EnterCaptcha";

        public string Value => "Enter Captcha Code";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_Captcha : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]Captcha";

        public string Value => "Captcha";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ForgotUsernameSuccess : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ForgotUsernameSuccess";

        public string Value => "Your username has been sent.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ForgotUsernameFailure : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ForgotUsernameFailure";

        public string Value => "Error sending your username";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ChangePassword : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ChangePassword";

        public string Value => "Change Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ConfirmNewPassword : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ConfirmNewPassword";

        public string Value => "Confirm New Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_NewPassword : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]NewPassword";

        public string Value => "New Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ChangePasswordSuccess : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ChangePasswordSuccess";

        public string Value => "Your password has <br>been successfully changed";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ForgotPasswordEmailSentToTitle : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ForgotPasswordEmailSentToTitle";

        public string Value => "Password reset link has been sent to your email";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ForgotPasswordEmailSentTo : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ForgotPasswordEmailSentTo";

        public string Value => "We’ve sent an email to";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ResendResetPasswordEmail : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ResendResetPasswordEmail";

        public string Value => "Resend Reset Password Email";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ResendResetPasswordEmailSent : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ResendResetPasswordEmailSent";

        public string Value => "New password reset email has been sent";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ForgotPasswordInstructions : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ForgotPasswordInstructions";

        public string Value => "Enter either your email or mobile phone. <br/> If you enter an email, a reset link will be sent to your inbox. <br/> If you enter your mobile number, an SMS will be sent to your mobile device.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ForgotUsernameInstructions : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ForgotUsernameInstructions";

        public string Value => "Enter either your email or mobile phone. <br/> If you enter an email, your username will be sent to your inbox. <br/> If you enter your mobile number, an SMS will be sent to your mobile device.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_ForgotPassword : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]ForgotPassword";

        public string Value => "Forgot Password?";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Forgot_UsernameRequestSuccessful : IDictionaryItem
    {
        public string ParentKey => "Forgot";

        public string Key => "[Forgot]UsernameRequestSuccessful";

        public string Value => "Your username has been sent to you.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    #endregion

    #region Reset Password Via Email 
    public class Others_ConfirmEmail : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]ConfirmEmail";

        public string Value => "Confirm Email Address";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Reset_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Reset";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Reset_ChangePassword : IDictionaryItem
    {
        public string ParentKey => "Reset";

        public string Key => "[Reset]ChangePassword";

        public string Value => "Change Password";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Reset_ResetPasswordSuccess : IDictionaryItem
    {
        public string ParentKey => "Reset";

        public string Key => "[Reset]ResetPasswordSuccess";

        public string Value => "New password has been set, you may login.";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    #endregion

    #region Server Errors
    public class ServerErrors_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "ServerErrors";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class ServerErrors_MissingField : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]MissingField";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Missing Required Field (*)" } };
    }
    public class ServerErrors_InvalidEmailFormat : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidEmailFormat";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Invalid Email Format" } };
    }
    public class ServerErrors_InvalidDate : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidDate";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Invalid Email Format" } };
    }
    public class ServerErrors_InvalidAge : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidAge";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "CInvalid Age" } };
    }
    public class ServerErrors_MobileOrEmailRequired : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]MobileOrEmailRequired";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Mobile Or Email Required" } };
    }
    public class ServerErrors_InvalidCountry : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidCountry";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Invalid Country" } };
    }
    public class ServerErrors_InvalidCurrency : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidCurrency";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Invalid Currency" } };
    }
    public class ServerErrors_InvalidLanguage : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidLanguage";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Invalid Language" } };
    }
    public class ServerErrors_InvalidTimeZone : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidTimeZone";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Invalid Time Zone" } };
    }
    public class ServerErrors_ExistingCustomer : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]ExistingCustomer";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Existing Customer" } };
    }
    public class ServerErrors_EmailNotFound : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]EmailNotFound";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Email Not Found" } };
    }
    public class ServerErrors_EmailSendFail : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]EmailSendFail";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Email Send Fail" } };
    }
    public class ServerErrors_ChangePassword : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]EmailAlreadyVerified";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Email Already Verified" } };
    }
    public class ServerErrors_InvalidCustomer : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidCustomer";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Invalid Customer" } };
    }
    public class ServerErrors_InvalidOldPassword : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidOldPassword";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Invalid Old Password" } };
    }
    public class ServerErrors_MatchingOldAndNewPassword : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]MatchingOldAndNewPassword";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Matching Old And New Password" } };
    }
    public class ServerErrors_InvalidCustomerStatus : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidCustomerStatus";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Invalid Customer Status" } };
    }
    public class ServerErrors_CustomerNotFound : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]CustomerNotFound";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Customer Not Found" } };
    }
    public class ServerErrors_VerificationRecordNotFound : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]VerificationRecordNotFound";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Verification Record Not Found" } };
    }
    public class ServerErrors_VerificationEmailExpired : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]VerificationEmailExpired";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Verification Email Expired" } };
    }
    public class ServerErrors_ValidationCodeExpired : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]ValidationCodeExpired";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Validation Code Expired" } };
    }
    public class ServerErrors_ValidationCodeSendFail : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]ValidationCodeSendFail";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Validation Code Send Fail" } };
    }
    public class ServerErrors_SMSSendFail : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]SMSSendFail";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "SMS Send Fail" } };
    }
    public class ServerErrors_InvalidMobileNumber : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidMobileNumber";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Invalid Mobile Number" } };
    }
    public class ServerErrors_InvalidVerificationEmail : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidVerificationEmail";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Invalid Verification Email" } };
    }
    public class ServerErrors_VerificationCodeLimitExceeded : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]VerificationCodeLimitExceeded";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Verification Code Limit Exceeded" } };
    }
    public class ServerErrors_MobileNumberNotFound : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]MobileNumberNotFound";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Mobile Number Not Found" } };
    }
    public class ServerErrors_ValidationCodeInvalid : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]ValidationCodeInvalid";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Validation Code Invalid" } };
    }
    public class ServerErrors_UnhandledError : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]UnhandledError";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Unhandled Server Error" } };
    }
    public class ServerErrors_FieldRequired : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]FieldRequired";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "This field is required." } };
    }
    public class ServerErrors_BelowMinimumWithdrawalAmount : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]BelowMinimumWithdrawalAmount";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Below minimum withdraw amount" } };
    }
    public class ServerErrors_InsufficientBalance : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InsufficientBalance";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Insufficient Balance"
            }
        };
    }
    public class ServerErrors_UsernameCannotBeEmailAddress : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]UsernameCannotBeEmailAddress";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Username cannot be an email address!"
            }
        };
    }
    public class ServerErrors_ConnectionTimeout : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]UsernameCannotBeEmailAddress";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Connection Timed out"
            }
        };
    }
    public class ServerErrors_InvalidLogin : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidLogin";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Invalid Login"
            }
        };
    }
    public class ServerErrors_Other : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]Other";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Other"
            }
        };
    }
    public class ServerErrors_AccountLocked : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]AccountLocked";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Account Locked!"
            }
        };
    }
    public class ServerErrors_ExceedsMaximalWithdrawal : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]ExceedsMaximalWithdrawal";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Exceeds maximum withdrawal amount"
            }
        };
    }
    public class ServerErrors_InvalidCard : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidCard";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Invalid Card"
            }
        };
    }
    public class ServerErrors_InvalidIBAN : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidIBAN";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Invalid IBAN"
            }
        };
    }

    public class ServerErrors_UndefinedCustomer : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]UndefinedCustomer";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Undefined Customer"
            }
        };
    }

    public class ServerErrors_InvalidUsername : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]InvalidUsername";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Invalid Username"
            }
        };
    }

    public class ServerErrors_MatchingUsername : IDictionaryItem
    {
        public string ParentKey => "ServerErrors";

        public string Key => "[ServerErrors]MatchingUsername";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string>
        {
            {
            "en",
            "Matching Username"
            }
        };
    }
    #endregion

    #region Pages
    public class Pages_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Pages";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Pages_SportsPage : IDictionaryItem
    {
        public string ParentKey => "Pages";

        public string Key => "[Pages]SportsPage";

        public string Value => "";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Pages_SportEvents : IDictionaryItem
    {
        public string ParentKey => "Pages";

        public string Key => "[Pages]SportEvents";

        public string Value => "Sport Events";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Pages_SportEventsEventName : IDictionaryItem
    {
        public string ParentKey => "Pages";

        public string Key => "[Pages]SportEventsEventName";

        public string Value => "Name";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Pages_SportEventsEventScheduleTime : IDictionaryItem
    {
        public string ParentKey => "Pages";

        public string Key => "[Pages]SportEventsEventScheduleTime";

        public string Value => "Scheduled Time";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Pages_SportEventsEventStatusDescription : IDictionaryItem
    {
        public string ParentKey => "Pages";

        public string Key => "[Pages]SportEventsEventStatusDescription";

        public string Value => "Status";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Pages_SportEventsEventTournament : IDictionaryItem
    {
        public string ParentKey => "Pages";

        public string Key => "[Pages]SportEventsEventTournament";

        public string Value => "Tournament";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Pages_SportEventsEventCategory : IDictionaryItem
    {
        public string ParentKey => "Pages";

        public string Key => "[Pages]SportEventsEventCategory";

        public string Value => "Category";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Pages_SportEventsEventCategorySport : IDictionaryItem
    {
        public string ParentKey => "Pages";

        public string Key => "[Pages]SportEventsEventCategorySport";

        public string Value => "Sport";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Pages_SportEventsNoEvents : IDictionaryItem
    {
        public string ParentKey => "Pages";

        public string Key => "[Pages]SportEventsNoEvents";

        public string Value => "No Events";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    #endregion

    #region Edit My Account
    public class Others_Save : IDictionaryItem
    {
        public string ParentKey => "Others";

        public string Key => "[Others]Save";

        public string Value => "Save";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Account_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Account";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Account_AccountPageTitle : IDictionaryItem
    {
        public string ParentKey => "Account";

        public string Key => "[Account]AccountPageTitle";

        public string Value => "My Account";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Account_AccountEditPageTitle : IDictionaryItem
    {
        public string ParentKey => "Account";

        public string Key => "[Account]AccountEditPageTitle";

        public string Value => "Edit";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Account_UsernameChangedSucccess : IDictionaryItem
    {
        public string ParentKey => "Account";

        public string Key => "[Account]UsernameChangedSucccess";

        public string Value => "Username Changed Successfully";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Account_UsernameChangedSucccessNotice : IDictionaryItem
    {
        public string ParentKey => "Account";

        public string Key => "[Account]UsernameChangedSucccessNotice";

        public string Value => "Your username was changed!";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Account_ChangeUsernameFailure : IDictionaryItem
    {
        public string ParentKey => "Account";

        public string Key => "[Account]ChangeUsernameFailure";

        public string Value => "Error changing your username";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    #endregion

    #region Genders
    public class Genders_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Genders";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Genders_Male : IDictionaryItem
    {
        public string ParentKey => "Genders";

        public string Key => "[Genders]Male";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Male" } };
    }

    public class Genders_Female : IDictionaryItem
    {
        public string ParentKey => "Genders";

        public string Key => "[Genders]Female";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Female" } };
    }

    public class Genders_Unknown : IDictionaryItem
    {
        public string ParentKey => "Genders";

        public string Key => "[Genders]Unknown";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Unknown" } };
    }
    #endregion    
    
    #region Receipt Status
    public class ReceiptStatus_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "ReceiptStatus";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class ReceiptStatus_Success : IDictionaryItem
    {
        public string ParentKey => "ReceiptStatus";

        public string Key => "[ReceiptStatus]Success";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Success" } };
    }

    public class ReceiptStatus_Fail : IDictionaryItem
    {
        public string ParentKey => "ReceiptStatus";

        public string Key => "[ReceiptStatus]Fail";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Fail" } };
    }
    #endregion

    #region Common
    public class Common_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Common";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class Common_JoinNow : IDictionaryItem
    {
        public string ParentKey => "Common";

        public string Key => "[Common]JoinNow";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "en", "Join Now" } };
    }
    #endregion

    #region Bonus Transaction
    public class BonusTransaction_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Bonus Transaction History";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class BonusTransaction_Name : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Name";

        public string Value => "Name";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_Type : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Type";

        public string Value => "Type";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_Status : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Status";

        public string Value => "Status";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_RedemptionStatus : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Redemption Status";

        public string Value => "Redemption Status";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_BonusApplied : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Bonus Applied";

        public string Value => "Bonus Applied";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_DepositBonus : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Deposit Bonus";

        public string Value => "Deposit Bonus";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_Active : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Active";

        public string Value => "Active";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_Closed : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Closed";

        public string Value => "Closed";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_Completed : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Completed";

        public string Value => "Completed";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_Incomplete : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Incomplete";

        public string Value => "Incomplete";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_RedemptionName : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Redemption Name";

        public string Value => "Redemption Name";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_RedemptionQualifierRequirement : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Redemption Qualifier Requirement";

        public string Value => "Redemption Qualifier Requirement";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_RedemptionQualifierProgress : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Redemption Qualifier Progress";

        public string Value => "Redemption Qualifier Progress";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_RedemptionQualifierDifference : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]Redemption Qualifier Difference";

        public string Value => "Redemption Qualifier Difference";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_TotalBetAmount : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]TotalBetAmount";

        public string Value => "Total Bet Amount";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_TotalSportsbookAmount : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]TotalSportsbookAmount";

        public string Value => "Total Sportsbook Amount";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_TotalPokerAmount : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]TotalPokerAmount";

        public string Value => "Total Poker Amount";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_TotalDepositAmount : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]TotalDepositAmount";

        public string Value => "Total Deposit Amount";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_TotalCasinoAmount : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]TotalCasinoAmount";

        public string Value => "Total Casino Amount";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_TotalLiveCasinoAmount : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]TotalLiveCasinoAmount";

        public string Value => "Total Live Casino Amount";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class BonusTransaction_TotalLotteryAmount : IDictionaryItem
    {
        public string ParentKey => "Bonus Transaction History";

        public string Key => "[BonusTransactionHistory]TotalLotteryAmount";

        public string Value => "Total Lottery Amount";

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    #endregion

    #region Receipt
    public class Receipt_Gift_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "Receipt";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Receipt_Gift : IDictionaryItem
    {
        public string ParentKey => "Receipt";

        public string Key => "[Receipt]Gift";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    public class Receipt_Bonus : IDictionaryItem
    {
        public string ParentKey => "Receipt";

        public string Key => "[Receipt]Bonus";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.English.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }
    #endregion

    #region Account Page
    public class AccountPage_ParentKey : IDictionaryItem
    {
        public string ParentKey => string.Empty;

        public string Key => "AccountPage";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => null;
    }

    public class AccountPage_ChangeUsername : IDictionaryItem
    {
        public string ParentKey => "AccountPage";

        public string Key => "[AccountPage]ChangeUsername";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "fa", "تغییرایمیل" } };
    }

    public class AccountPage_NewUsername : IDictionaryItem
    {
        public string ParentKey => "AccountPage";

        public string Key => "[AccountPage]NewUsername";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "fa", "نام کاربریجدید" } };
    }
    public class AccountPage_CurrentUsername : IDictionaryItem
    {
        public string ParentKey => "AccountPage";

        public string Key => "[AccountPage]CurrentUsername";

        public string Value => string.Empty;

        public string LanguageCode => Models.Country.Persian.GetCountryLanguageIsoCode();

        public Dictionary<string, string> Translations => new Dictionary<string, string> { { "fa", "جاری نام کاربری" } };
    }
    #endregion
}
