#define FAKES

using FreshMvvm;
using Microsoft.Extensions.Configuration;
using SmartHome.Fakes;
using SmartHome.Infrastructure;
using SmartHome.Infrastructure.AppState;
using SmartHome.PageModels;
using SmartHome.Services;
using SmartHome.Services.LightSwitchService;
using SmartHome.Services.SensorService;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace SmartHome
{
    public partial class App : Application
    {
        private CustomFreshMasterDetailNavigationContainer _mainNavigation;
        private FreshNavigationContainer _devicesNavigation;

        public App()
        {
            InitializeComponent();

            FreshIOC.Container.Register<IAppState, AppState>().AsSingleton();
            InitializeConfiguration();

#if FAKES
            FreshIOC.Container.Register<IAuthenticationService, FakeAuthenticationService>();
            FreshIOC.Container.Register<ISensorService, FakeSensorService>();
            FreshIOC.Container.Register<ILightSwitchService, FakeLightSwitchService>();
            FreshIOC.Container.Register<IAlarmService, FakeAlarmService>();
            FreshIOC.Container.Register<ICameraService, FakeCameraService>();
            FreshIOC.Container.Register<IBoardService, FakeBoardService>();
#else
            FreshIOC.Container.Register<IAuthenticationService, FakeAuthenticationService>();
            FreshIOC.Container.Register<ISensorService, SensorServiceClient>();
            FreshIOC.Container.Register<ILightSwitchService, LightSwitchServiceClient>();
            FreshIOC.Container.Register<IAlarmService, FakeAlarmService>();
            FreshIOC.Container.Register<ICameraService, FakeCameraService>();
            FreshIOC.Container.Register<IBoardService, FakeBoardService>();     
            
            var appState = FreshIOC.Container.Resolve<IAppState>();
            var authService = FreshIOC.Container.Resolve<IAuthenticationService>();
            authService.LogIn("", "").ContinueWith(async (user) => { appState.UserData = await user; });
#endif
            var appState = FreshIOC.Container.Resolve<IAppState>();
            var authService = FreshIOC.Container.Resolve<IAuthenticationService>();
            authService.LogIn("", "").ContinueWith(async (user) => { appState.UserData = await user; });


            //_mainNavigation = InitializeMainAppNavigation();
            //FreshIOC.Container.Register<IFreshNavigationService>(_mainNavigation, NavigationStacks.MainAppStack);
            //_mainNavigation.AddPage<AllDevicesPageModel>("DevicesPageModel", "");

            var navigationContainer = new CustomFreshMasterDetailNavigationContainer(NavigationStacks.MainAppStack);
            _mainNavigation = navigationContainer;
            _mainNavigation.Init(appState.UserData.Username);

            //_mainNavigation.Master = FreshPageModelResolver.ResolvePageModel<SideMenuPageModel>();
            //_mainNavigation.Detail = new NavigationPage(FreshPageModelResolver.ResolvePageModel<AllDevicesPageModel>("AllDevicesPageModel"));
            _mainNavigation.AddPage<AllDevicesPageModel>("AllDevicesPageModel", "All Devices");
            _mainNavigation.AddPage<SensorsPageModel>("SensorsPageModel", "🌡️ Sensors", isMainPage: false);
            _mainNavigation.AddPage<LightSwitchesPageModel>("LightsPageModel", "💡 Lights", isMainPage: false);
            _mainNavigation.AddPage<AlarmSensorsPageModel>("AlarmSensorsPageModel", "🚨 Alarm Sensors", isMainPage: false);
            _mainNavigation.AddPage<CamerasPageModel>("CamerasPageModel", "📹 Cameras", isMainPage: false);
            _mainNavigation.AddPage<AlarmPageModel>("AlarmPageModel", "Alarm");

            MainPage = _mainNavigation;

            //var devicesPage = FreshPageModelResolver.ResolvePageModel<AllDevicesPageModel>();
            //_devicesNavigation = new FreshNavigationContainer(devicesPage);

            //var tabbedNavigation = new FreshTabbedNavigationContainer();
            //tabbedNavigation.AddTab<AllDevicesPageModel>("All Devices", null);
            //tabbedNavigation.AddTab<SensorsPageModel>("Sensors", "🌡💧☀️");
            //tabbedNavigation.AddTab<LightSwitchesPageModel>("Light Switches", null);
            //tabbedNavigation.AddTab<AlarmSensorsPageModel>("Alarm Sensors", null);
            //tabbedNavigation.AddTab<CamerasPageModel>("Cameras", null);

            ////_devicesNavigation = new FreshNavigationContainer(devicesPage, NavigationStacks.MainAppStack);
            ////FreshIOC.Container.Unregister<IFreshNavigationService>(NavigationStacks.MainAppStack);
            ////FreshIOC.Container.Register<IFreshNavigationService>(_devicesNavigation, NavigationStacks.MainAppStack);
            //MainPage = tabbedNavigation;

            //MainPage = FreshPageModelResolver.ResolvePageModel<DevicesPageModel>();
        }

        private CustomFreshMasterDetailNavigationContainer InitializeMainAppNavigation()
        {
            var masterNavigation = new CustomFreshMasterDetailNavigationContainer(NavigationStacks.MainAppStack);
            masterNavigation.Init("Nigger");
            return masterNavigation;

            //var mainPage = FreshPageModelResolver.ResolvePageModel<DevicesPageModel>(); // will have to be changed when adding login
            //var mainPageStack = new FreshNavigationContainer(mainPage, NavigationStacks.MainAppStack);
            //return mainPageStack;
        }

        private void InitializeConfiguration()
        {
            Stream resourceStream = GetType().GetTypeInfo()
                .Assembly.GetManifestResourceStream("SmartHome.appsettings.json");

            var appSettings = new ConfigurationBuilder()
                .AddJsonStream(resourceStream).Build();
            var appState = FreshIOC.Container.Resolve<IAppState>();
            appState.Configuration = appSettings;
        }

        //private void UnregisterDependencies()
        //{
        //    FreshIOC.Container.Unregister<IBoardService>();
        //    FreshIOC.Container.Unregister<ICameraService>();
        //    FreshIOC.Container.Unregister<IAlarmService>();
        //    FreshIOC.Container.Unregister<ILightSwitchService>();
        //    FreshIOC.Container.Unregister<ISensorService>();
        //    FreshIOC.Container.Unregister<IAppState>();
        //}

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
