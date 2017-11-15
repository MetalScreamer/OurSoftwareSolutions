using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string me, string str, StringComparison comparisonType)
        {
            return me.IndexOf(str, comparisonType) >= 0;
        }
    }
}
