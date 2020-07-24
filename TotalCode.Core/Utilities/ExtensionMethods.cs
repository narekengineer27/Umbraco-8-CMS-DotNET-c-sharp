using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCode.Core.Utilities
{
    public static class ExtensionMethods
    {
        public static string FormatDate(this DateTime date)
        {
            return date.ToString("MMM dd 'at' hh:mm tt");
        }

        public static string FormatDate(this DateTime? date)
        {
            return date?.ToString("MMM dd 'at' hh:mm tt");
        }
    }
}
