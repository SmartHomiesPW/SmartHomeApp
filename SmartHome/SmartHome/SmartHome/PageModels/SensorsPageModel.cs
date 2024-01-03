using MvvmHelpers;
using SmartHome.Models;
using SmartHome.Services.SensorService;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHome.PageModels
{
    public class SensorsPageModel : BasePageModel
    {
        private ISensorService _sensorService;

        private ObservableRangeCollection<IBoardDevice> _devices = new ObservableRangeCollection<IBoardDevice>();
        public ObservableRangeCollection<IBoardDevice> Devices
        {
            get => _devices;
            set
            {
                SetProperty(ref _devices, value);
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing { get => _isRefreshing; set { SetProperty(ref _isRefreshing, value); } }
        public ICommand RefreshCommand { get; }


        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            UpdateDevices();
        }

        private async void UpdateDevices()
        {
            IsRefreshing = true;
            var devices = await _sensorService.GetSensors();
            Devices.ReplaceRange(devices);
            IsRefreshing = false;
        }

        public SensorsPageModel(ISensorService sensorService)
        {
            _sensorService = sensorService;
            RefreshCommand = new Command(UpdateDevices);
        }
    }
}
