using FreshMvvm;
using MvvmHelpers;
using SmartHome.Models;
using SmartHome.Services;
using SmartHome.Services.LightSwitchService;
using SmartHome.Services.SensorService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartHome.PageModels
{
    public class AllDevicesPageModel : BasePageModel
    {
        private ISensorService _sensorService;
        private ILightSwitchService _lightSwitchService;
        private IAlarmService _alarmService;
        private ICameraService _cameraService;

        private IBoardDevice _selectedDevice = null;
        private ObservableRangeCollection<IBoardDevice> _devices = new ObservableRangeCollection<IBoardDevice>();
        public ObservableRangeCollection<IBoardDevice> Devices
        {
            get => _devices;
            set
            {
                SetProperty(ref _devices, value);
            }
        }

        public IBoardDevice SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                SetProperty(ref _selectedDevice, value);
            }
        }

        public ICommand SelectionChangedCommand { get; }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            UpdateDevices();
        }

        private async Task<List<IBoardDevice>> GetDevices()
        {
            List<IBoardDevice> devices = new List<IBoardDevice>();

            devices.AddRange(await _sensorService.GetSensors());
            devices.AddRange(await _lightSwitchService.GetLightSwitches());
            devices.AddRange(await _alarmService.GetAlarmSensors());
            devices.AddRange(await _cameraService.GetCameras());

            //var sensors = await _sensorService.GetSensors();
            //var lightSwitches = await _lightSwitchService.GetLightSwitches();
            //var alarmSensors = await _alarmService.GetAlarmSensors();
            //var cameras = await _cameraService.GetCameras();
            //devices.AddRange(sensors);
            //devices.AddRange(lightSwitches);
            //devices.AddRange(alarmSensors);
            //devices.AddRange(cameras);

            return devices;
        }

        private async void UpdateDevices()
        {
            var devices = await GetDevices();
            Devices.ReplaceRange(devices);
        }

        public AllDevicesPageModel(
            ISensorService sensorService,
            ILightSwitchService lightSwitchService,
            IAlarmService alarmService,
            ICameraService cameraService)
        {
            _sensorService = sensorService;
            _lightSwitchService = lightSwitchService;
            _alarmService = alarmService;
            _cameraService = cameraService;

            SelectionChangedCommand = new FreshAwaitCommand(async (param, task) =>
            {
                if (SelectedDevice != null)
                {
                    await _selectedDevice.Command.Invoke(SelectedDevice);
                    SelectedDevice = null;
                }
                task.SetResult(true);
            });
        }
    }
}
