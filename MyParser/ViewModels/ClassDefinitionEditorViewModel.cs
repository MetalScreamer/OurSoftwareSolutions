using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Windows.ViewModels
{
    public class ClassDefinitionEditorViewModel
    {
        public static IEnumerable<string> DataTypes { get; } = 
            new List<string>()
            {
                "Integer",
                "String",
                "DateTime",
                "Long",
                "Boolean",
                "Double"
            };
    }
}
