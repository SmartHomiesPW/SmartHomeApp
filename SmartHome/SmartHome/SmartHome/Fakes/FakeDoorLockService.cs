using SmartHome.Models;
using SmartHome.Services.DoorLockService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    public class FakeDoorLockService : IDoorLockService
    {
        public async Task<List<DoorLock>> GetDoorLocks()
        {
            Func<object, Task<bool>> doorLockCommand = new Func<object, Task<bool>>(async (param) =>
            {
                bool result = false;
                if (param is DoorLock doorLock)
                {
                    result = (doorLock.Status == DeviceStatus.On) ? await DoorLockSetOff(doorLock) : await DoorLockSetOn(doorLock);
                }
                return result;
            });

            var doorLockList = new List<DoorLock>()
            {
                new DoorLock()
                {
                    Id= "79",
                    BoardId = "2",
                    Name = "Main door doorlock",
                    Status = DeviceStatus.On,
                    Command = doorLockCommand,
                },
                new DoorLock()
                {
                    Id= "78",
                    BoardId = "1",
                    Name = "Garage doorlock",
                    Status = DeviceStatus.Off,
                    Command = doorLockCommand,
                }
            };

            return await Task.FromResult(doorLockList);
        }

        public async Task<bool> DoorLockSetOff(DoorLock doorLock)
        {
            doorLock.Status = DeviceStatus.Off;

            return await Task.FromResult(true);
        }

        public async Task<bool> DoorLockSetOn(DoorLock doorLock)
        {
            doorLock.Status = DeviceStatus.On;

            return await Task.FromResult(true);
        }
    }
}
