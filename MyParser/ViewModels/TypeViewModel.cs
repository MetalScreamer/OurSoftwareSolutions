using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Windows.ViewModels
{
    class TypeViewModel
    {
        public static IEnumerable<TypeViewModel> Types { get; } =
            new TypeViewModel[]
            {
                new TypeViewModel("Integer", typeof(int)),
                new TypeViewModel("String", typeof(string)),
                new TypeViewModel("Long", typeof(long)),
                new TypeViewModel("Double",typeof(double)),
                new TypeViewModel("Boolean", typeof(bool)),
                new TypeViewModel("Date & Time", typeof(DateTime))
            };

        public string Name { get; private set; }
        public Type Type { get; private set; }

        private TypeViewModel(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}
