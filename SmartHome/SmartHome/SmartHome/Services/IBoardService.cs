using SmartHome.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Services
{
    public interface IBoardService
    {
        Task<List<IBoardDevice>> GetDevices(string machineId);
    }
}
