using Prism;
using Prism.Ioc;
using Homework04.ViewModels;
using Homework04.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Homework04
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{Constants.Navigation}/{Constants.Login}");
            //await NavigationService.NavigateAsync(new Uri($"/{Constants.Navigation}/{Constants.TabbedPage}?selectedTab={Constants.Discovery}", UriKind.Absolute));

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainTabbedPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SignupPage, SignupPageViewModel>();
            containerRegistry.RegisterForNavigation<ContactPage, ContactPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditContactPage, AddEditContactPageViewModel>();
            containerRegistry.RegisterForNavigation<DiscoveryPage, DiscoveryPageViewModel>();

        }
    }
}
