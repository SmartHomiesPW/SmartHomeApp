using Microsoft.Extensions.Configuration;
using MvvmHelpers;
using SmartHome.Models;
using Xamarin.Essentials;

namespace SmartHome.Infrastructure.AppState
{
    public class AppState : ObservableObject, IAppState
    {
        private User _user;

        public User UserData { get => _user; set => SetProperty(ref _user, value); }
        public IConfiguration Configuration { get; set; } = null;
    }
}
