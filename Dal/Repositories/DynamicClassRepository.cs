using Oss.Dal.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Dal.Repositories
{
    public class DynamicClassRepository : IDynamicClassRepository
    {
        public void Delete(IDynamicClassDefinition cls)
        {
            throw new NotImplementedException();
        }

        public IDynamicClassDefinition Find(string nameIsLike)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDynamicClassDefinition> Get()
        {
            throw new NotImplementedException();
        }

        public IDynamicClassDefinition Get(long id)
        {
            throw new NotImplementedException();
        }

        public void Save(IDynamicClassDefinition cls)
        {
            throw new NotImplementedException();
        }
    }
}
