using SmartHome.Models;
using SmartHome.Services;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    public class FakeAuthenticationService : IAuthenticationService
    {
        private User _user;

        public FakeAuthenticationService()
        {
            _user = new User()
            {
                Id = "1234567890",
                Username = "JohnSmith01",
                Email = "johnsmith@johnsmith.com",
                FirstName = "John",
                LastName = "Smith"
            };
        }

        public Task<User> Register(string username, string password, string email, string name = "", string surname = "")
        {
            _user = new User()
            {
                Id = "1234567890",
                Username = username,
                Email = email,
                FirstName = name,
                LastName = surname
            };
            return Task.FromResult(_user);
        }

        public Task<User> LogIn(string username, string password)
        {
            _user = new User()
            {
                Id = "1234567890",
                Username = "JohnSmith01",
                Email = "johnsmith@johnsmith.com",
                FirstName = "John",
                LastName = "Smith"
            };
            return Task.FromResult(_user);
        }
        public Task<bool> LogOut()
        {
            _user = null;
            return Task.FromResult(true);
        }
    }
}
