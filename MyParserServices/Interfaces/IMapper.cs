using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.Interfaces
{
    public interface IMapper<T1, T2>
    {
        T1 Map(T2 obj);
        T2 Map(T1 obj);
    }
}
