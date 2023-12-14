using FreshMvvm;
using MvvmHelpers;
using SmartHome.Models;
using SmartHome.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SmartHome.PageModels
{
    public class AlarmSensorsPageModel : BasePageModel
    {
        private IAlarmService _alarmSensorsService;
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

        private async void UpdateDevices()
        {
            var devices = await _alarmSensorsService.GetAlarmSensors();
            Devices.ReplaceRange(devices);
        }

        public AlarmSensorsPageModel(IAlarmService alarmSensorsService)
        {
            _alarmSensorsService = alarmSensorsService;

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
