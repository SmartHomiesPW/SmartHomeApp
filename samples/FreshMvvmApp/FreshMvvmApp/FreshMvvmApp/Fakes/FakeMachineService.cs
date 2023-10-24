using FreshMvvmApp.Models;
using FreshMvvmApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshMvvmApp.Fakes
{
    class FakeMachineService : IMachineService
    {
        private FakeSensorService _fakeSensorService { get; set; }

        public FakeMachineService() { 
            _fakeSensorService = new FakeSensorService();
        }

        public async Task<List<IDevice>> GetDevices(string machineId)
        {
            return new List<IDevice>()
            {
                new Sensor()
                {
                    Id = "1",
                    Type = SensorType.Temperature,
                    Name = "TestTemperatureSensor",
                    Logs = await _fakeSensorService.GetSensorLogs("1")
                }
            };
        }
    }
}
