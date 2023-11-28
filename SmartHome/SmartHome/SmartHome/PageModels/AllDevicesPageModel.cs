using FreshMvvm;
using MvvmHelpers;
using SmartHome.Models;
using SmartHome.Services;
using System;
using System.Windows.Input;

namespace SmartHome.PageModels
{
    public class AllDevicesPageModel : FreshBasePageModel
    {
        private IBoardService _boardService;
        private IBoardDevice _selectedDevice = null;
        private ObservableRangeCollection<IBoardDevice> _devices = new ObservableRangeCollection<IBoardDevice>();
        public ObservableRangeCollection<IBoardDevice> Devices
        {
            get => _devices;
            set
            {
                _devices = value;
                RaisePropertyChanged(nameof(Devices));
            }
        }

        public IBoardDevice SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                _selectedDevice = value;
                RaisePropertyChanged(nameof(SelectedDevice));
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
            var devices = await _boardService.GetDevices("");
            Devices.ReplaceRange(devices);
        }

        public AllDevicesPageModel(IBoardService boardService)
        {
            _boardService = boardService;
            //Devices = new ObservableRangeCollection<IBoardDevice>();

            SelectionChangedCommand = new FreshAwaitCommand((param, task) =>
            {
                if (SelectedDevice != null)
                {
                    _selectedDevice.Command.Execute(this);
                }
                task.SetResult(true);
            });
        }
    }
}
