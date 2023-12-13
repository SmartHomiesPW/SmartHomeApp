using MvvmHelpers;
using SmartHome.Models;
using SmartHome.Services.SensorService;
using System;

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

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            UpdateDevices();
        }

        private async void UpdateDevices()
        {
            var devices = await _sensorService.GetSensors();
            Devices.ReplaceRange(devices);
        }

        public SensorsPageModel(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }
    }
}
