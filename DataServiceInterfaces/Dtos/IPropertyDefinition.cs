using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Dtos
{
    public interface IPropertyDefinition
    {
        long Id { get; }
        string Name { get; set; }
        Guid TypeId { get; set; }
        bool IsReadOnly { get; set; }
        string ReadOnlyFormula { get; set; }
    }
}
