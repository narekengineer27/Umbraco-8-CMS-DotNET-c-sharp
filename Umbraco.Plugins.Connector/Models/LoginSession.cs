namespace Umbraco.Plugins.Connector.Models
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using Umbraco.Plugins.Connector.Services;

    public static class LoginSession
    {
        public static string Username
        {
            get
            {
                return (string)HttpContext.Current.Session["username"];
            }
            set
            {
                HttpContext.Current.Session["username"] = value;
            }
        }

        public static string Token
        {
            get
            {
                return (string)HttpContext.Current.Session["customerToken"];
            }
            set
            {
                HttpContext.Current.Session["customerToken"] = value;
            }
        }

        public static bool LoggedIn
        {
            get
            {
                return (bool)HttpContext.Current.Session["login"];
            }
            set
            {
                HttpContext.Current.Session["login"] = value;
            }
        }

        public static DateTime Expires
        {
            get
            {
                return (DateTime)HttpContext.Current.Session["expires"];
            }
            set
            {
                HttpContext.Current.Session["expires"] = value;
            }
        }

        public static DateTime LastLogin
        {
            get
            {
                return (DateTime)HttpContext.Current.Session["lastLogin"];
            }
            set
            {
                HttpContext.Current.Session["lastLogin"] = value;
            }
        }

        public static CustomerSummary CustomerSummary
        {
            get
            {
                return (CustomerSummary)HttpContext.Current.Session["customerSummary"];
            }
            set
            {
                HttpContext.Current.Session["customerSummary"] = value;
            }
        }

        public static JwtCustomerDataResponseContent DecodedJwtToken
        {
            get
            {
                var decoded = new TotalCodeApiService().Decode(Token);
                decoded.EncodedToken = Token;
                return decoded;
            }
        }

        public static JwtCustomerDataResponseContent DecodeJwtToken(string token)
        {
            var decoded = new TotalCodeApiService().Decode(Token);
            decoded.EncodedToken = Token;
            return decoded;
        }
        public static bool? IsMobileBrowser
        {
            get
            {
                if (HttpContext.Current.Session["mobileBrowser"] == null) return null;
                else return bool.Parse(HttpContext.Current.Session["mobileBrowser"].ToString());
            }
            set
            {
                HttpContext.Current.Session["mobileBrowser"] = value;
            }
        }
        public static void Logout()
        {
            Username = null;
            Token = null;
            LoggedIn = false;
            Expires = DateTime.MinValue;
            LastLogin = DateTime.MinValue;

            CustomerSummary = null;
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.GetOwinContext().Authentication.SignOut();
            if (HttpContext.Current.Request.Cookies["username"] != null)
            {
                HttpContext.Current.Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies["token"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies["lastLogin"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        public static async Task LogoutAsync()
        {
            Logout();
            await Task.FromResult(true).ConfigureAwait(false);
        }
    }
}
