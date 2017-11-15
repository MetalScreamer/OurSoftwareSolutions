using Oss.Dal.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oss.Dal.Repositories
{
    public interface IDynamicClassRepository
    {
        Task<IEnumerable<IClassDefinition>> GetClasses(bool includeProperties = false);
        Task<IClassDefinition> Get(long id, bool includeProperties = false);
        Task<IEnumerable<IClassDefinition>> Find(string nameIsLike, bool includeProperties = false);

        Task<IEnumerable<IPropertyDefinition>> GetProperties(IClassDefinition classDto);
        Task<IPropertyDefinition> GetProperty(long id);
        
        Task Save(IEnumerable<IClassDefinition> classDtos = null, IEnumerable<IPropertyDefinition> propertyDtos = null);
        Task Delete(IEnumerable<IClassDefinition> classDtos = null, IEnumerable<IPropertyDefinition> propertyDtos = null);
    }
}
