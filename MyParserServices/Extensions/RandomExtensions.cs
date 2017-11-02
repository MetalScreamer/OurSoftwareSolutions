using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.Extensions
{
    public static class RandomExtensions
    {
        public static bool NextBool(this Random me)
        {
            return me.Next(0, 2) == 1;
        }
    }
}
