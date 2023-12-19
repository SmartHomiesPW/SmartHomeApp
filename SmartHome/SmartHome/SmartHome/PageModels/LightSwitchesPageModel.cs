using FreshMvvm;
using MvvmHelpers;
using SmartHome.Models;
using SmartHome.Services.LightSwitchService;
using System;
using System.Windows.Input;

namespace SmartHome.PageModels
{
    public class LightSwitchesPageModel : BasePageModel
    {
        private ILightSwitchService _lightSwitchService;
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
            var devices = await _lightSwitchService.GetLightSwitches();
            Devices.ReplaceRange(devices);
        }

        public LightSwitchesPageModel(ILightSwitchService lightSwitchService)
        {
            _lightSwitchService = lightSwitchService;

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
