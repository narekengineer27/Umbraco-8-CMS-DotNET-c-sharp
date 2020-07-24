namespace Umbraco.Plugins.Connector.Exceptions
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    public enum ExceptionCode
    {
        Unhandled = -999,
        None = 0,
        NotAuthorized = 100,
        TenantApiIncorrect = 101,
        DefaultLanguageIsMandatory = 400,
        DuplicateTenantUid = 501,
        DuplicateTenantBrandName = 502,
        DuplicateTenantDomain = 503,
        TenantNotFound = 504,
        TenantAlreadyDisabled = 505,
        TenantAlreadyEnabled = 506,
        TenantAlreadyExists = 507,
        DomainAlreadyAssigned = 601,
        DomainMalformatted = 602,
        SubDomainIsRequired = 603,
        DomainDoesNotExist = 604,
        UsernameAlreadyExists = 701,
        UsernameMalformed = 702,
        UsernameAlreadyDisabled = 703,
        UsernameAlreadyEnabled = 704,
        UsernameDoesNotExist = 705,
        PasswordLenghtIncorrect = 706,
        MediaNodeAlreadyExists = 800,
        MediaNodeDoesNotExist = 801,
        GroupAlreadyExists = 900,
        GroupDoesNotExist = 901
    }

    public static class ExceptionHelper
    {
        public static string CodeToString(this ExceptionCode code)
        {
            switch (code)
            {
                case ExceptionCode.TenantApiIncorrect:
                    return "Tenant information for the Api incorrect";
                case ExceptionCode.MediaNodeDoesNotExist:
                    return "Media Node does not exist";
                case ExceptionCode.PasswordLenghtIncorrect:
                    return "Minimum Password length is 10 Characters";
                case ExceptionCode.NotAuthorized:
                    return "Not Authorized";
                case ExceptionCode.DefaultLanguageIsMandatory:
                    return "Default Language is Mandatory";
                case ExceptionCode.DuplicateTenantUid:
                    return "Duplicate Tenant Uid (Guid)";
                case ExceptionCode.DuplicateTenantBrandName:
                    return "Duplicate Tenant Brand name";
                case ExceptionCode.DuplicateTenantDomain:
                    return "Duplicate Tenant Domain";
                case ExceptionCode.TenantNotFound:
                    return "Tenant not found";
                case ExceptionCode.DomainAlreadyAssigned:
                    return "Domain already assigned";
                case ExceptionCode.DomainMalformatted:
                    return "Domain mal-formatted";
                case ExceptionCode.SubDomainIsRequired:
                    return "Sub domain is required";
                case ExceptionCode.DomainDoesNotExist:
                    return "Domain does not exist";
                case ExceptionCode.UsernameAlreadyExists:
                    return "Username already exists";
                case ExceptionCode.UsernameMalformed:
                    return "Username mal-formatted";
                case ExceptionCode.UsernameAlreadyDisabled:
                    return "Username already disabled";
                case ExceptionCode.UsernameAlreadyEnabled:
                    return "Username already enabled";
                case ExceptionCode.UsernameDoesNotExist:
                    return "Username does not exist";
                case ExceptionCode.TenantAlreadyEnabled:
                    return "Tenant already enabled";
                case ExceptionCode.TenantAlreadyDisabled:
                    return "Tenant already disabled";
                case ExceptionCode.TenantAlreadyExists:
                    return "Tenant already exists";
                case ExceptionCode.MediaNodeAlreadyExists:
                    return "Media node already exists";
                case ExceptionCode.GroupAlreadyExists:
                    return "Group already exists";
                case ExceptionCode.GroupDoesNotExist:
                    return "Group does not exist";
                case ExceptionCode.Unhandled:
                default:
                    return "Unhandled Error";
            }
        }

        public static string GetStackTrace()
        {
            StackTrace st = new StackTrace();
            return new StackTrace(new StackFrame(true)).ToString();
        }
    }

    [Serializable]
    public class TenantException : Exception, ITenantException
    {
        public TenantException() : base()
        {
            this.Code = ExceptionCode.Unhandled;
        }

        public TenantException(string message) : base(message)
        {
            this.Code = ExceptionCode.Unhandled;
        }

        public TenantException(string message, Exception innerException) : base(message, innerException)
        {
            this.Code = ExceptionCode.Unhandled;
        }

        public TenantException(string message, ExceptionCode code) : base(message)
        {
            this.Code = code;
        }

        public TenantException(string message, ExceptionCode code, Guid tenantUid) : base(message)
        {
            this.Code = code;
            this.TenantUid = tenantUid.ToString();
        }

        public TenantException(string message, ExceptionCode code, string info) : base(message)
        {
            this.Code = code;
            this.Info = info;
        }

        public TenantException(string message, ExceptionCode code, Guid tenantUid, string info) : base(message)
        {
            this.Code = code;
            this.TenantUid = tenantUid.ToString();
            this.Info = info;
        }

        public TenantException(string message, ExceptionCode code, string tenantUid, string info) : base(message)
        {
            this.Code = code;
            this.TenantUid = tenantUid;
            this.Info = info;
        }

        public TenantException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        public ExceptionCode Code { get; set; }
        public string ResourceReferenceProperty { get; }
        public string TenantUid { get; set; }
        public string Info { get; set; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            info.AddValue("ResourceReferenceProperty", ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }
    }

    public interface ITenantException
    {
        ExceptionCode Code { get; set; }
        string TenantUid { get; set; }
        string ResourceReferenceProperty { get; }
    }
}
