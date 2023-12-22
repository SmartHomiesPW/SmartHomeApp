using System.Threading.Tasks;

namespace SmartHome.Infrastructure.AppService
{
    public interface IAppService
    {
        Task ShowMenu(bool showMenu);
    }
}
