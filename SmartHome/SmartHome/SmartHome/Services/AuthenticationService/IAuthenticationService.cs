using SmartHome.Models;
using System.Threading.Tasks;

namespace SmartHome.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Tries to register a user with given credentials.
        /// If successful, sets the created user's data in app configuration     
        /// If fails, returns an empty user object
        /// </summary>
        Task<User> Register(string email, string password, string firstName = "", string lastName = "");

        /// <summary>
        /// Tries to login a user with given credentials.
        /// If successful, sets the created user's data in app configuration
        /// If fails, returns an empty user object
        /// </summary>
        Task<User> LogIn(string email, string password);

        /// <summary>
        /// Clears user data in app configuration       
        /// </summary>
        Task<bool> LogOut();
    }
}
