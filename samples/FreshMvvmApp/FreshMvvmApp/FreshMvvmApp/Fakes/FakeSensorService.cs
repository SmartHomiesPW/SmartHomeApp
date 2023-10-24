using FreshMvvmApp.Models;
using FreshMvvmApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshMvvmApp.Fakes
{
    class FakeSensorService : ISensorService
    {
        public async Task<List<SensorLog>> GetSensorLogs(string sensorId)
        {
            return new List<SensorLog>()
            {
                new SensorLog
                {
                    SensorId = "1",
                    Value = 5,
                    Time = DateTime.Now
                }
            };
        }

        public Task<Dictionary<SensorType, Sensor>> GetSensors()
        {
            throw new NotImplementedException();
        }

        public Task<List<SensorLog>> RefreshSensorLogs(string sensorId)
        {
            throw new NotImplementedException();
        }
    }
}
