using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.Interfaces
{
    public interface IExpressionService
    {
        bool IsValid(string expression);
        IEnumerable<IToken> GetTokens(string expression);
    }
}
