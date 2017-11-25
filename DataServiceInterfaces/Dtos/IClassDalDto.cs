using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Dtos
{
    public interface IClassDalDto
    {
        long Id { get; }
        string Name { get; set; }
        IEnumerable<IPropertyDalDto> Properties { get; }
    }
}
