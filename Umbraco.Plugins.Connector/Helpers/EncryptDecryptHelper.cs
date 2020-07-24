namespace Umbraco.Plugins.Connector.Helpers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class EncryptDecryptHelper
    {
        public static string Sha256Encrypt(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(phrase));
            return Convert.ToBase64String(hashedDataBytes);
        }

        public static bool Sha256Matches(string phrase, string key)
        {
            return Sha256Encrypt(key).Equals(phrase);
        }
    }
}
