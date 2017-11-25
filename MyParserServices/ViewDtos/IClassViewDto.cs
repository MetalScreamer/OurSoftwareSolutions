using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.ViewDtos
{
    public interface IClassViewDto
    {
        Guid Id { get; }
        string Name { get; set; }
        
        bool IsDirty { get; }
    }
}
