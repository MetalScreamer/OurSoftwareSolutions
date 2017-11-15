using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Dtos
{
    public interface IClassDefinition
    {
        long Id { get; }
        string Name { get; set; }
        IEnumerable<IPropertyDefinition> Properties { get; }
    }
}
