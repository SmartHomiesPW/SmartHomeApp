using FreshMvvm;
using MvvmHelpers;
using SmartHome.Models;
using SmartHome.Services;
using SmartHome.Services.LightSwitchService;
using SmartHome.Services.SensorService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHome.PageModels
{
    public class BoardDevicesPageModel : BasePageModel
    {
        private ISensorService _sensorService;
        private ILightSwitchService _lightSwitchService;
        private IAlarmService _alarmService;
        private ICameraService _cameraService;
        private Board _board;

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

        public Board Board { get => _board; set => SetProperty(ref _board, value); }

        private bool _isRefreshing;
        public bool IsRefreshing { get => _isRefreshing; set { SetProperty(ref _isRefreshing, value); } }

        public ICommand SelectionChangedCommand { get; }
        public ICommand RefreshCommand { get; }


        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData is Board board)
            {
                Board = board;
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            UpdateDevices();
        }

        private async Task<List<IBoardDevice>> GetDevices()
        {
            // Change to getting devices by BoardId when it's implemented
            var sensors = await _sensorService.GetSensors();
            var lightSwitches = await _lightSwitchService.GetLightSwitches();
            var alarmSensors = await _alarmService.GetAlarmSensors();
            var cameras = await _cameraService.GetCameras();

            List<IBoardDevice> devices = new List<IBoardDevice>();

            devices.AddRange(sensors);
            devices.AddRange(lightSwitches);
            devices.AddRange(alarmSensors);
            devices.AddRange(cameras);

            return devices;
        }

        private async void UpdateDevices()
        {
            IsRefreshing = true;
            var devices = await GetDevices();
            Devices.ReplaceRange(devices);
            IsRefreshing = false;
        }

        public BoardDevicesPageModel(
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

            RefreshCommand = new Command(UpdateDevices);
        }
    }
}
