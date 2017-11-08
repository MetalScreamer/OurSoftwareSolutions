﻿using System.Windows;
using Unity;
using Unity.Injection;
using Oss.Common.Interfaces;
using Oss.BuisinessLayer;
using Oss.Windows.Views;
using Oss.Windows.ViewModels;
using Oss.Common.Services;
using Oss.BuisinessLayer.Services;
using Oss.Dal.Repositories;

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
                .RegisterType<IDynamicClassRepository, DynamicClassRepository>()
                .RegisterType<IDynamicClassService, DynamicClassService>()
                .RegisterType<ClassLibraryViewModel>()
                .RegisterType<IExpressionService, JscExpresion>()
                .RegisterType<MainWindowViewModel>()
                .RegisterType<MainWindow>(new InjectionConstructor(container.Resolve<MainWindowViewModel>()));                
        }
    }
}
