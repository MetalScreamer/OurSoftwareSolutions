using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oss.Dal;
using Oss.Common;

namespace Oss.BuisinessLayer.Extentions
{
    static class DynamicClassExtensions
    {
        internal static Common.ViewDtos.IDynamicClassDefinition Map(this Dal.Dtos.IClassDefinition classDefinition)
        {
            throw new NotImplementedException();
        }

        internal static Dal.Dtos.IClassDefinition Map(this Common.ViewDtos.IDynamicClassDefinition classDefinition)
        {
            throw new NotImplementedException();
        }
    }
}
