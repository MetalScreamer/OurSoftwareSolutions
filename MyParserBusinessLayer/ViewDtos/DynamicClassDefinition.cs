using Oss.BuisinessLayer.SyntaxHelpers;
using Oss.Common.ViewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.BuisinessLayer.ViewModels
{
    public class DynamicClassDefinition : IDynamicClassDefinition
    {
        private Guid id = Guid.NewGuid();

        private List<IDynamicClassPropertyDefinition> properties = new List<IDynamicClassPropertyDefinition>();

        public Guid Id { get; }
        public string Name { get; set; }
        public IEnumerable<IDynamicClassPropertyDefinition> Properties { get { return properties.AsEnumerable(); } }

        IDynamicClassPropertyDefinition AddProperty()
        {
            const string defaultPropertyName = "NewProperty";
            var counter = 0;
            while (properties.Any(p => string.Equals(defaultPropertyName + ++counter, p.Name)));
            return new DynamicClassPropertyDefinition()
            {
                Name = defaultPropertyName + counter,
                Type = DynamicClassPropertyType.String
            };
        }
    }
}
