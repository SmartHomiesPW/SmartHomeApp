using Microsoft.Extensions.Configuration;
using SmartHome.Models;

namespace SmartHome.Infrastructure.AppState
{
    public class AppState : IAppState
    {
        public User UserData { get; set; }
        public IConfiguration Configuration { get; set; } = null;
    }
}
