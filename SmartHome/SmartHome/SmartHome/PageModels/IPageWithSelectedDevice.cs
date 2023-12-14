using MvvmHelpers;
using SmartHome.Models;

namespace SmartHome.PageModels
{
    public interface IPageWithSelectedDevice
    {
        IBoardDevice SelectedDevice { get; }
        ObservableRangeCollection<IBoardDevice> Devices { get; }
    }
}
