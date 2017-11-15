using Oss.Common.ViewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.Services
{
    public interface IDynamicClassService
    {
        IEnumerable<IDynamicClassDefinition> Classes { get; }
        Task<IDynamicClassDefinition> AddClass();
        Task RemoveClass(IDynamicClassDefinition cls);
        Task Refresh(bool ignoreChanges);
        Task SaveChanges();
    }
}
