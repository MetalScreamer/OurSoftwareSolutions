using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Windows.ViewModels
{
    class PropertyDefinitionViewModel : ViewModelBase
    {
        private string name;
        private TypeViewModel type;

        public Guid Id { get; }

        public PropertyDefinitionViewModel(Guid id) { Id = id; }

        public string Name
        {
            get { return name; }
            set { SetProperty(value, ref name); }
        }

        public TypeViewModel Type
        {
            get { return type; }
            set { SetProperty(value, ref type); }
        }
    }
}
