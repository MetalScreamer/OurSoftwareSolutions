using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.ViewDtos
{
    public interface IDynamicClassDefinition
    {
        Guid Id { get; }
        string Name { get; set; }
        IEnumerable<IPropertyDefinition> Properties { get; }
        bool IsDirty { get; }
    }
}
