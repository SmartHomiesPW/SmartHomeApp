using FreshMvvm;
using MvvmHelpers;
using SmartHome.Models;
using SmartHome.Services;
using System;

namespace SmartHome.PageModels
{
    public class AllDevicesPageModel : FreshBasePageModel
    {
        private IBoardService _boardService;

        public ObservableRangeCollection<IBoardDevice> Devices { get; set; }

        public AllDevicesPageModel(IBoardService boardService)
        {
            _boardService = boardService;
            Devices = new ObservableRangeCollection<IBoardDevice>();
            //_deviceFilter = deviceFilter;

            //this.WhenAny(HandleContactChanged, o => o.Contact);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            Devices.ReplaceRange(_boardService.GetDevices("").Result);
        }

        void HandleContactChanged(string propertyName)
        {
            //handle the property changed, nice
        }
    }
}
