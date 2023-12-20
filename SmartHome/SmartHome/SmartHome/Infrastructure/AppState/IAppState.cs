using Microsoft.Extensions.Configuration;

namespace SmartHome.Infrastructure.AppState
{
    public interface IAppState
    {
        IConfiguration Configuration { get; set; }
    }
}
