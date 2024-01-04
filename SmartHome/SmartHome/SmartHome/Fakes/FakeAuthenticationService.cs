using SmartHome.Infrastructure.AppState;
using SmartHome.Models;
using SmartHome.Services;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    public class FakeAuthenticationService : IAuthenticationService
    {
        private IAppState _appState;
        private User _user;

        public FakeAuthenticationService(IAppState appState)
        {
            _appState = appState;

            //_user = new User()
            //{
            //    Id = "1234567890",
            //    Email = "johnsmith@johnsmith.com",
            //};

            _appState.UserData = _user = null;
        }

        public Task<User> Register(string email, string password)
        {
            if (email == null || email?.Length == 0)
            {
                return Task.FromResult(new User());
            }

            _user = new User()
            {
                Id = "1234567890",
                Email = email,
            };
            _appState.UserData = _user;
            return Task.FromResult(_user);
        }

        public Task<User> LogIn(string email, string password)
        {
            if (email == null || email.Length == 0)
            {
                return Task.FromResult(new User());
            }

            _user = new User()
            {
                Id = "1234567890",
                Email = "johnsmith@johnsmith.com",
            };
            _appState.UserData = _user;
            return Task.FromResult(_user);
        }
        public Task<bool> LogOut()
        {
            _user = null;

            return Task.FromResult(true);
        }
    }
}
