using MvvmHelpers;
using SmartHome.Infrastructure;
using SmartHome.Infrastructure.AppState;

namespace SmartHome.PageModels
{
    public class SideMenuPageModel : BasePageModel
    {
        private IAppState _appState;
        public IAppState AppState { get { return _appState; } }

        public ObservableRangeCollection<SideMenuFieldModel> PageFields { get; set; } = new ObservableRangeCollection<SideMenuFieldModel>();

        public SideMenuPageModel(IAppState appState)
        {
            _appState = appState;
        }
    }
}
