using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.BuisinessLayer.Models
{
    public class DynamicClass
    {
        private List<DynamicClassPropertyDefinition> properties = new List<DynamicClassPropertyDefinition>();

        public string Name { get; set; }
        public IEnumerable<DynamicClassPropertyDefinition> Properties { get { return properties.AsEnumerable(); } }

        public object GetDefaultValue(string propertyName)
            => this.properties.FirstOrDefault(p => p.Name == propertyName)?.DefaultValue;

        public DynamicObject CreateInstance() => new DynamicObject(this);

        public DynamicClassPropertyDefinition AddProperty(string name, Type type)
        {
            var result =
                new DynamicClassPropertyDefinition()
                {
                    Name = name,
                    Type = type
                };

            properties.Add(result);

            return result;
        }
    }
}