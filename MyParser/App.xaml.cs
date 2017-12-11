using System.Windows;
using Unity;
using Unity.Injection;
using Oss.Common.Interfaces;
using Oss.BuisinessLayer;
using Oss.Windows.Views;
using Oss.Windows.ViewModels;
using Oss.Common.Services;
using Oss.BuisinessLayer.Services;
using Oss.Dal.Repositories;
using Oss.Common.ViewDtos;
using Oss.BuisinessLayer.ViewDtos;
using Unity.Resolution;
using Oss.Dal.Dtos;
using Oss.BuisinessLayer.Mappers;
using Oss.Dal.Utilities;
using Unity.Lifetime;
using Oss.Dal.Models;

namespace Oss.Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();

            SetupIocContainer(container);

            //var mainWindow = container.Resolve<MainWindow>();
            //mainWindow.Show();

            //var cls = new DynamicClass() { Name = "Person" };
            //cls.AddProperty("FirstName", typeof(long));
            //cls.AddProperty("LastName", typeof(string));
            //var obj = cls.CreateInstance();
            //var vm = new DynamicViewModel(obj);
            //var inputWin = new DynamicVMTesterInput() { DataContext = vm };
            //var viewWindow = new DynamicVMViewr() { DataContext = vm };
            //inputWin.Show();
            //viewWindow.Show();
            var window = new Window()
            {
                Content = new DynamicClassDefinitionView() { DataContext = container.Resolve<ClassLibraryViewModel>() }
            };
            window.Show();
        }

        private static void SetupIocContainer(IUnityContainer container)
        {
            container
                .RegisterType<ModelDtoMapper>(new ContainerControlledLifetimeManager())
                .RegisterInstance<IMapper<IClassDalDto, ClassDefinition>>(container.Resolve<ModelDtoMapper>())
                .RegisterInstance<IMapper<IPropertyDalDto, PropertyDefinition>>(container.Resolve<ModelDtoMapper>())
                .RegisterType<IClassDefinitionMapper, ClassDefinitionMapper>()
                .RegisterType<IDynamicClassRepository, DynamicClassRepository>()
                .RegisterType<IClassService, DynamicClassService>()
                .RegisterType<ClassLibraryViewModel>()
                .RegisterType<IExpressionService, JscExpresion>()
                .RegisterType<IClassViewDto, ClassViewDto>()
                .RegisterInstance<ClassViewDtoFactory>(id => container.Resolve<IClassViewDto>(new ParameterOverride("id", id)))
                .RegisterType<IClassDalDto, ClassDalDto>()
                .RegisterInstance<ClassDalDtoFactory>(id => container.Resolve<IClassDalDto>(new ParameterOverride("id", id)))
                .RegisterType<MainWindowViewModel>()
                .RegisterType<MainWindow>(new InjectionConstructor(container.Resolve<MainWindowViewModel>()));
        }
    }
}
