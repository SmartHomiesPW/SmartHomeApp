using SmartHome.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Services.DoorLockService
{
    public interface IDoorLockService
    {
        Task<List<DoorLock>> GetDoorLocks();
        Task<bool> DoorLockSetOn(DoorLock doorLock);
        Task<bool> DoorLockSetOff(DoorLock doorLock);
    }
}
