using Oss.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.BuisinessLayer.ViewDtos
{
    public class DynamicObject
    {
        private Dictionary<string, object> properties = new Dictionary<string, object>();
        
        public object this[string propertyName]
        {
            get { return GetValue(propertyName); }
            set { SetValue(propertyName, value); }
        }

        public DynamicClass Class { get; }

        public DynamicObject(DynamicClass _class)
        {
            Class = _class;
            foreach (var property in _class.Properties)
            {
                properties.Add(property.Name, property.DefaultValue);
            }
        }

        private void SetValue(string propertyName, object value)
        {
            properties[propertyName] = value;
        }

        private object GetValue(string propertyName) => properties.GetValueOrDefault(propertyName, Class.GetDefaultValue(propertyName));
        public T GetValue<T>(string propertyName) => (T)properties.GetValueOrDefault(propertyName, Class.GetDefaultValue(propertyName));
    }

}
