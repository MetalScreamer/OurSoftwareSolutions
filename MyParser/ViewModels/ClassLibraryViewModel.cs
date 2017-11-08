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

        public ObservableCollection<ClassDefinitionViewModel> Classes { get; } = new ObservableCollection<ClassDefinitionViewModel>();
        public ClassDefinitionViewModel SelectedClass
        {
            get { return selectedClass; }
            set { SetProperty(value, ref selectedClass); }
        }

        public AsynchronousCommand AddClass { get; }
        public Command RemoveClass { get; }
        public AsynchronousCommand AddProperty { get; }
        public Command RemoveProperty { get; }

        public ClassLibraryViewModel(IDynamicClassService classLibraryService)
        {
            AddClass = new AsynchronousCommand(DoAddClassAsync);
            RemoveClass = new Command(p => DoRemoveClass(), p => CanRemoveClass());
            AddProperty = new AsynchronousCommand(DoAddProperty, CanAddProperty);
        }

        private bool CanAddProperty()
        {
            throw new NotImplementedException();
        }

        private async Task DoAddProperty()
        {
            throw new NotImplementedException();
        }

        private bool CanRemoveClass()
        {
            return SelectedClass != null;
        }

        private void DoRemoveClass()
        {
            Classes.Remove(SelectedClass);
        }

        private async Task DoAddClassAsync()
        {
            var classDef = new ClassDefinitionViewModel();

            await Task.Run(
                () =>
                {
                    var counter = 0;
                    while (Classes.Select(c => c.Name).Contains($"New Class {++counter}", StringComparer.OrdinalIgnoreCase)) ;
                    classDef.Name = $"New Class {counter}";
                });

            Classes.Add(classDef);
            SelectedClass = classDef;
        }
    }
}
