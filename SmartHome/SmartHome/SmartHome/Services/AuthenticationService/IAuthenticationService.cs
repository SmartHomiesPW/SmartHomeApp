using SmartHome.Models;
using System.Threading.Tasks;

namespace SmartHome.Services
{
    public interface IAuthenticationService
    {
        Task<User> Register(string email, string password, string firstName = "", string lastName = "");
        Task<User> LogIn(string email, string password);
        Task<bool> LogOut();
    }
}
