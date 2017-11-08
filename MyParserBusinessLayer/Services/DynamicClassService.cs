using Oss.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oss.Common.ViewDtos;
using Oss.Dal.Repositories;

namespace Oss.BuisinessLayer.Services
{
    public class DynamicClassService : IDynamicClassService
    {
        public DynamicClassService(IDynamicClassRepository repo)
        {

        }

        public IDynamicClassDefinition AddClass()
        {
            throw new NotImplementedException();
        }

        public void RemoveClass(IDynamicClassDefinition cls)
        {
            throw new NotImplementedException();
        }
    }
}
