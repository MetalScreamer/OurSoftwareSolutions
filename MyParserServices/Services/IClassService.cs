using Oss.Common.ViewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.Services
{
    public interface IClassService
    {
        Task<IEnumerable<IClassViewDto>> GetClasses();
        Task<IClassViewDto> AddClass();
        Task UpdateClass(IClassViewDto cls);
        Task RemoveClass(Guid classId);

        Task<IEnumerable<IPropertyDefinition>> GetProperties(Guid classId);
        Task<IPropertyDefinition> AddProperty(Guid classId);
        Task UpdateProperty(IPropertyDefinition property);
        Task RemoveProperty(Guid classId, Guid propertyId);

        Task<IEnumerable<IType>> GetTypes();
        Task<IType> GetType(Guid id);

        bool IsDirty { get; }

        Task Refresh(bool ignoreChanges);
        Task SaveChanges();
    }
}
