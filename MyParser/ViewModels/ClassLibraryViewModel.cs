using Oss.Common.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Oss.Windows.ViewModels
{
    class ClassLibraryViewModel : ViewModelBase
    {
        private ClassDefinitionViewModel selectedClass;
        private PropertyDefinitionViewModel selectedProperty;
        private IClassService classLibraryService;

        public ObservableCollection<ClassDefinitionViewModel> Classes { get; } = new ObservableCollection<ClassDefinitionViewModel>();
        public ClassDefinitionViewModel SelectedClass
        {
            get { return selectedClass; }
            set
            {
                if(SetProperty(value, ref selectedClass))
                {
                    SelectedProperty = null;
                }
            }
        }

        public PropertyDefinitionViewModel SelectedProperty
        {
            get { return selectedProperty; }
            set { SetProperty(value, ref selectedProperty); }
        }

        public AsynchronousCommand AddClass { get; }
        public AsynchronousCommand RemoveClass { get; }
        public AsynchronousCommand AddProperty { get; }
        public AsynchronousCommand RemoveProperty { get; }

        public ClassLibraryViewModel(IClassService classLibraryService)
        {
            this.classLibraryService = classLibraryService;
            AddClass = new AsynchronousCommand(DoAddClassAsync);
            RemoveClass = new AsynchronousCommand(DoRemoveClass, CanRemoveClass);
            AddProperty = new AsynchronousCommand(DoAddProperty, CanAddProperty);
            RemoveProperty = new AsynchronousCommand(DoRemoveProperty, CanRemoveProperty);
        }

        private bool CanRemoveProperty()
        {
            return SelectedProperty != null;
        }

        private async Task DoRemoveProperty()
        {
            await classLibraryService.RemoveProperty(SelectedClass.Id, SelectedProperty.Id);
        }

        private bool CanAddProperty()
        {
            return SelectedClass != null;
        }

        private async Task DoAddProperty()
        {
            var property = await classLibraryService.AddProperty(SelectedClass.Id);
        }

        private bool CanRemoveClass()
        {
            return SelectedClass != null;
        }

        private async Task DoRemoveClass()
        {
            var selectedClass = SelectedClass;
            Classes.Remove(selectedClass);
            await classLibraryService.RemoveClass(selectedClass.Id);            
        }

        private async Task DoAddClassAsync()
        {            
            var classDef = new ClassDefinitionViewModel(await classLibraryService.AddClass());            

            Classes.Add(classDef);
            SelectedClass = classDef;
        }
    }
}
