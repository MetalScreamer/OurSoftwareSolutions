using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.Extensions
{
    public static class DictionaryExtentions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> me, TKey key, TValue defaultValue = default(TValue))
        {
            return me.ContainsKey(key) ? me[key] : defaultValue;
        }
    }
}
