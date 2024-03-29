﻿#define FAKES

using FreshMvvm;
using Microsoft.Extensions.Configuration;
using SmartHome.Fakes;
using SmartHome.Infrastructure;
using SmartHome.Infrastructure.AppState;
using SmartHome.PageModels;
using SmartHome.Services.AlarmService;
using SmartHome.Services.AuthenticationService;
using SmartHome.Services.BoardService;
using SmartHome.Services.CameraService;
using SmartHome.Services.DoorLockService;
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
        private FreshNavigationContainer _loginNavigation;

        public App()
        {
            InitializeComponent();

            FreshIOC.Container.Register<IAppState, AppState>().AsSingleton();
            InitializeConfiguration();


            // Register services for dependency injection
            // Choose which services to use. If FAKES is defined, mock services
            // mock services will be used. Otherwise, services connecting
            // to cloud (or whatever is specified in 'appsettings.json')
            // will be used
#if FAKES
            FreshIOC.Container.Register<IAuthenticationService, FakeAuthenticationService>();
            FreshIOC.Container.Register<ISensorService, FakeSensorService>();
            FreshIOC.Container.Register<ILightSwitchService, FakeLightSwitchService>();
            FreshIOC.Container.Register<IAlarmService, FakeAlarmService>();
            FreshIOC.Container.Register<IDoorLockService, FakeDoorLockService>();
            FreshIOC.Container.Register<ICameraService, FakeCameraService>();
            FreshIOC.Container.Register<IBoardService, FakeBoardService>();
#else
            FreshIOC.Container.Register<IAuthenticationService, AuthenticationServiceClient>();
            FreshIOC.Container.Register<ISensorService, SensorServiceClient>();
            FreshIOC.Container.Register<ILightSwitchService, LightSwitchServiceClient>();
            FreshIOC.Container.Register<IAlarmService, AlarmServiceClient>();
            FreshIOC.Container.Register<IDoorLockService, DoorLockServiceClient>();
            FreshIOC.Container.Register<ICameraService, FakeCameraService>();
            FreshIOC.Container.Register<IBoardService, FakeBoardService>();
#endif

            MainPage = InitializeLogInAppNavigation();
            InitializeMainAppNavigation();
        }

        private FreshNavigationContainer InitializeLogInAppNavigation()
        {
            FreshIOC.Container.Unregister<IFreshNavigationService>(NavigationStacks.LogInStack);

            var loginPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var loginPageStack = new FreshNavigationContainer(loginPage, NavigationStacks.LogInStack);

            _loginNavigation = loginPageStack;
            FreshIOC.Container.Register<IFreshNavigationService>(loginPageStack, NavigationStacks.LogInStack);

            return loginPageStack;
        }


        /// <summary>
        /// Initializes and returns the main application stack with its side panel.        
        /// </summary>
        private CustomFreshMasterDetailNavigationContainer InitializeMainAppNavigation()
        {
            FreshIOC.Container.Unregister<IFreshNavigationService>(NavigationStacks.MainAppStack);

            var navigationContainer = new CustomFreshMasterDetailNavigationContainer(NavigationStacks.MainAppStack);
            navigationContainer.Init("Menu");
            navigationContainer.AddPage<AllDevicesPageModel>("AllDevicesPageModel", "All Devices");
            navigationContainer.AddPage<SensorsPageModel>("SensorsPageModel", "🌡️ Sensors", isMainPage: false);
            navigationContainer.AddPage<LightSwitchesPageModel>("LightsPageModel", "💡 Lights", isMainPage: false);
            navigationContainer.AddPage<AlarmSensorsPageModel>("AlarmSensorsPageModel", "🚨 Alarm Sensors", isMainPage: false);
            navigationContainer.AddPage<DoorLockPageModel>("DoorLockPageModel", "🔒 Door Locks", isMainPage: false);
            //navigationContainer.AddPage<CamerasPageModel>("CamerasPageModel", "📹 Cameras", isMainPage: false);
            navigationContainer.AddPage<BoardChoicePageModel>("BoardChoicePageModel", "Boards");
            navigationContainer.AddPage<AlarmPageModel>("AlarmPageModel", "Alarm");

            _mainNavigation = navigationContainer;
            FreshIOC.Container.Register<IFreshNavigationService>(navigationContainer, NavigationStacks.MainAppStack);

            return navigationContainer;
        }

        /// <summary>
        /// Switches the navigation stack to the MainAppStack      
        /// </summary>
        public void SwitchToMainPage()
        {
            MainPage = _mainNavigation;
        }

        /// <summary>
        /// Switches the navigation stack to the LoginStack      
        /// </summary>
        public void SwitchToLoginPage()
        {
            MainPage = _loginNavigation;
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
