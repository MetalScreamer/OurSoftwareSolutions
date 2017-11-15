using Oss.Common.ViewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Common.Services
{
    public interface IDynamicClassPropertyService
    {
        IPropertyDefinition AddProperty(IDynamicClassDefinition cls);
        void RemoveProperty(IPropertyDefinition property, IDynamicClassDefinition cls);
    }
}
