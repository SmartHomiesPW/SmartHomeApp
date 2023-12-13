#define FAKES

using FreshMvvm;
using SmartHome.Fakes;
using SmartHome.Infrastructure;
using SmartHome.PageModels;
using SmartHome.Services;
using SmartHome.Services.SensorService;
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

#if FAKES           
            FreshIOC.Container.Register<ISensorService, FakeSensorService>();
            FreshIOC.Container.Register<ILightSwitchService, FakeLightSwitchService>();
            FreshIOC.Container.Register<IAlarmService, FakeAlarmService>();
            FreshIOC.Container.Register<ICameraService, FakeCameraService>();
            FreshIOC.Container.Register<IBoardService, FakeBoardService>();
#else
            FreshIOC.Container.Register<ISensorService, SensorServiceClient>();
            FreshIOC.Container.Register<ILightSwitchService, FakeLightSwitchService>();
            FreshIOC.Container.Register<IAlarmService, FakeAlarmService>();
            FreshIOC.Container.Register<ICameraService, FakeCameraService>();
            FreshIOC.Container.Register<IBoardService, FakeBoardService>();
#endif

            //_mainNavigation = InitializeMainAppNavigation();
            //FreshIOC.Container.Register<IFreshNavigationService>(_mainNavigation, NavigationStacks.MainAppStack);
            //_mainNavigation.AddPage<DevicesPageModel>("DevicesPageModel", "");

            var devicesPage = FreshPageModelResolver.ResolvePageModel<AllDevicesPageModel>();
            _devicesNavigation = new FreshNavigationContainer(devicesPage);

            var tabbedNavigation = new FreshTabbedNavigationContainer();
            tabbedNavigation.AddTab<AllDevicesPageModel>("All Devices", null);
            tabbedNavigation.AddTab<SensorsPageModel>("Sensors", "🌡💧☀️");
            tabbedNavigation.AddTab<LightSwitchesPageModel>("Light Switches", null);
            tabbedNavigation.AddTab<AlarmSensorsPageModel>("Alarm Sensors", null);
            tabbedNavigation.AddTab<CamerasPageModel>("Cameras", null);

            //_devicesNavigation = new FreshNavigationContainer(devicesPage, NavigationStacks.MainAppStack);
            //FreshIOC.Container.Unregister<IFreshNavigationService>(NavigationStacks.MainAppStack);
            //FreshIOC.Container.Register<IFreshNavigationService>(_devicesNavigation, NavigationStacks.MainAppStack);
            MainPage = tabbedNavigation;

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
