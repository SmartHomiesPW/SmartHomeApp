using Microsoft.Extensions.Configuration;

namespace SmartHome.Infrastructure.AppState
{
    public class AppState : IAppState
    {
        public IConfiguration Configuration { get; set; } = null;
    }
}
