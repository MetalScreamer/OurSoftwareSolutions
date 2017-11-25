using Oss.Common.ViewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.BuisinessLayer.ViewDtos
{
    class PropertyType : IType
    {
        public Guid Id { get; }
        public string Name { get; }
        public Type DotNetType { get; }

        public static PropertyType Integer { get; } = new PropertyType("[bdb9ca0a-5e70-4277-ac0c-bcc02768f264]", "Integer", typeof(int));
        public static PropertyType String { get; } = new PropertyType("[ebff1268-8f17-4f21-a9ea-df3e40706fba]", "String", typeof(string));
        public static PropertyType Long { get; } = new PropertyType("[661cdc6e-1b88-4a25-9a84-0093179b4316]", "Long", typeof(long));
        public static PropertyType Double { get; } = new PropertyType("[0b7eb693-8cf8-463a-a7a0-e323ef359b8e]", "Double", typeof(double));
        public static PropertyType Boolean { get; } = new PropertyType("[441372ae-e667-40ba-b5e4-08ec722ced83]", "Boolean", typeof(bool));
        public static PropertyType  DateTime { get; } = new PropertyType("[94e76e23-f94b-45c7-ae7e-bc688f82e041]", "Date & Time", typeof(DateTime));
        
        public static IEnumerable<IType> Types { get; } =
            new PropertyType[]
            {
                Integer,
                String,
                Long,
                Double,
                Boolean,
                DateTime
            };

        private PropertyType(string id, string name, Type type)
        {
            Id = new Guid(id);
            Name = name;
            DotNetType = type;
        }
    }
}
