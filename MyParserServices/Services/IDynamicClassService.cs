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
        Task<IEnumerable<IClassDefinition>> GetClasses();
        Task<IClassDefinition> AddClass();
        Task UpdateClass(IClassDefinition cls);
        Task RemoveClass(Guid classId);

        Task<IEnumerable<IPropertyDefinition>> GetProperties(Guid classId);
        Task<IPropertyDefinition> AddProperty(Guid classId);
        Task UpdateProperty(IPropertyDefinition property);
        Task RemoveProperty(Guid classId, Guid propertyId);

        bool IsDirty { get; }

        Task Refresh(bool ignoreChanges);
        Task SaveChanges();
    }
}
