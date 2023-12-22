using SmartHome.Models;
using System.Threading.Tasks;

namespace SmartHome.Services
{
    public interface IAuthenticationService
    {
        Task<User> Register(string username, string password, string email, string name = "", string surname = "");
        Task<User> LogIn(string username, string password);
        Task<bool> LogOut();
    }
}
