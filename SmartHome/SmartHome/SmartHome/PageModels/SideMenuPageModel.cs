using FreshMvvm;
using MvvmHelpers;
using SmartHome.Infrastructure;
using SmartHome.Infrastructure.AppState;
using SmartHome.Models;
using SmartHome.Services;
using System.Windows.Input;
using Xamarin.Essentials;

namespace SmartHome.PageModels
{
    public class SideMenuPageModel : BasePageModel
    {
        private IAppState _appState;
        private IAuthenticationService _authenticationService;
        public IAppState AppState { get => _appState; }

        public ObservableRangeCollection<SideMenuFieldModel> PageFields { get; set; } = new ObservableRangeCollection<SideMenuFieldModel>();

        public ICommand LogoutCommand { get; set; }

        public SideMenuPageModel(IAppState appState, IAuthenticationService authenticationService)
        {
            _appState = appState;
            _authenticationService = authenticationService;

            LogoutCommand = new FreshAwaitCommand(async (obj) =>
            {
                bool logoutSuccessful = await _authenticationService.LogOut();
                if (logoutSuccessful)
                {
                    ((App)App.Current).SwitchToLoginPage();
                }
                obj.SetResult(true);
            });
        }
    }
}
