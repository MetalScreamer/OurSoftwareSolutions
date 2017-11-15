using Oss.Common.ViewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.BuisinessLayer.ViewDtos
{
    public class DynamicClassPropertyDefinitionDto : IPropertyDefinition
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Formula { get; set; }
        public bool IsReadonly { get; set; }
        public string Name { get; set; }
        public IType Type { get; set; }
    }
}
