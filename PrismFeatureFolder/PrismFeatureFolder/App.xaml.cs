using Prism;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using PrismFeatureFolder.Features.Dashboard.ViewModels;
using PrismFeatureFolder.Features.Dashboard.Views;
using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrismFeatureFolder
{
    public partial class App
    {
        public App(IPlatformInitializer initializer=null):base(initializer)
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage>();//規則に従ってViewModelと結び付けられる
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();//明示的に指定
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(nameof(MainPage));
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            
            //We set here the type resolver
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(GetViewModelName);
        }

        private Type GetViewModelName(Type viewType)
        {
            var viewModelName = viewType.FullName.Replace("Views", "ViewModels")+"ViewModel";
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            return Type.GetType($"{viewModelName}, {viewAssemblyName}");
        }
    }
}
