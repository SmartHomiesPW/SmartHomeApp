using Microsoft.Extensions.Configuration;
using SmartHome.Models;

namespace SmartHome.Infrastructure.AppState
{
    public interface IAppState
    {
        User UserData { get; set; }
        IConfiguration Configuration { get; set; }
    }
}
