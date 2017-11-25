using Oss.Common.ViewDtos;
using System;
using System.Collections.ObjectModel;

namespace Oss.Windows.ViewModels
{
    class ClassDefinitionViewModel : ViewModelBase//, IClassDefinitionViewModel
    {
        private string name;        

        public Guid Id { get; }
        public string Name
        {
            get { return name; }
            set { SetProperty(value, ref name); }
        }

        public ClassDefinitionViewModel(IClassViewDto classDef) : this(classDef, Guid.Empty) { }
        public ClassDefinitionViewModel(IClassViewDto classDef, Guid id)
        {
            Id = id;
            Name = classDef.Name;
        }        

        public ObservableCollection<PropertyDefinitionViewModel> Properties { get; } = new ObservableCollection<PropertyDefinitionViewModel>();
    }
}
