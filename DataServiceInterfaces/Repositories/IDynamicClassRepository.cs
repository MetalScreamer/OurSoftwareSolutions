using Oss.Dal.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oss.Dal.Repositories
{
    public interface IDynamicClassRepository
    {
        Task<IEnumerable<IClassDalDto>> GetClasses(bool includeProperties = false);
        Task<IClassDalDto> Get(long id, bool includeProperties = false);
        Task<IEnumerable<IClassDalDto>> Find(string nameIsLike, bool includeProperties = false);

        Task<IEnumerable<IPropertyDalDto>> GetProperties(IClassDalDto classDto);
        Task<IPropertyDalDto> GetProperty(long id);
        
        Task Save(IEnumerable<IClassDalDto> classDtos = null, IEnumerable<IPropertyDalDto> propertyDtos = null);
        Task Delete(IEnumerable<IClassDalDto> classDtos = null, IEnumerable<IPropertyDalDto> propertyDtos = null);
    }
}
