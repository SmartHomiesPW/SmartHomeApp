using SmartHome.Models;
using System.Threading.Tasks;

namespace SmartHome.Services
{
    public interface IAuthenticationService
    {
        Task<User> Register(string email, string password);
        Task<User> LogIn(string email, string password);
        Task<bool> LogOut();
    }
}
