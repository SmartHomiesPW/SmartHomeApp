using FreshMvvm;
using SmartHome.Infrastructure.AppState;
using System.Windows.Input;

namespace SmartHome.PageModels
{
    public class SideMenuPageModel : BasePageModel
    {
        private IAppState _appState;
        public IAppState AppState { get { return _appState; } }

        public ICommand GoToAllDevicesPageCommand { get; set; }
        public ICommand GoToSensorsPageCommand { get; set; }
        public ICommand GoToLightSwitchesPageCommand { get; set; }
        public ICommand GoToAlarmSensorsPageCommand { get; set; }
        public ICommand GoToCamerasPageCommand { get; set; }


        public SideMenuPageModel(IAppState appState)
        {
            _appState = appState;

            GoToAllDevicesPageCommand = new FreshAwaitCommand(async (obj) =>
            {
                await CoreMethods.PushPageModel<AllDevicesPageModel>(obj);
                obj.SetResult(true);
            });

            GoToSensorsPageCommand = new FreshAwaitCommand(async (obj) =>
            {
                await CoreMethods.PushPageModel<SensorsPageModel>(obj);
                obj.SetResult(true);
            });

            GoToLightSwitchesPageCommand = new FreshAwaitCommand(async (obj) =>
            {
                await CoreMethods.PushPageModel<LightSwitchesPageModel>(obj);
                obj.SetResult(true);
            });

            GoToAlarmSensorsPageCommand = new FreshAwaitCommand(async (obj) =>
            {
                await CoreMethods.PushPageModel<AlarmSensorsPageModel>(obj);
                obj.SetResult(true);
            });

            GoToCamerasPageCommand = new FreshAwaitCommand(async (obj) =>
            {
                await CoreMethods.PushPageModel<CamerasPageModel>(obj);
                obj.SetResult(true);
            });
        }



    }
}
