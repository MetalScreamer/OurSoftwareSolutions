using Oss.Dal.Dtos;
using System.Collections.Generic;

namespace Oss.Dal.Repositories
{
    public interface IDynamicClassRepository
    {
        IEnumerable<IDynamicClassDefinition> Get();
        IDynamicClassDefinition Get(long id);
        IDynamicClassDefinition Find(string nameIsLike);
        void Save(IDynamicClassDefinition cls);
        void Delete(IDynamicClassDefinition cls);
    }
}
