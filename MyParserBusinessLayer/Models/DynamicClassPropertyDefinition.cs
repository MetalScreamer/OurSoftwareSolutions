using Oss.BuisinessLayer.SyntaxHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.BuisinessLayer.ViewDtos
{
    public class DynamicClassPropertyDefinition
    {
        public string Name { get; set; }
        public DataTypeSyntax DataTypeSyntax { get { return DataTypeSyntax.GetSyntaxForType(Type); } }
        public object DefaultValue { get; set; }
        public Type Type { get; set; }
    }
}
