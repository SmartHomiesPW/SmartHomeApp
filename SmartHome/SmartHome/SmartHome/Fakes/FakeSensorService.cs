using SmartHome.Models;
using SmartHome.Services.SensorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    class FakeSensorService : ISensorService
    {

        public async Task<List<Sensor>> GetSensors()
        {
            Func<object, Task<bool>> sensorCommand = new Func<object, Task<bool>>(async (param) =>
            {
                return await Task.FromResult(true);
            });

            List<Sensor> sensorList = new List<Sensor> {
                new Sensor()
                {
                    Id = "1",
                    BoardId = "1",
                    SensorType = SensorType.Temperature,
                    Status = DeviceStatus.On,
                    Name = "TestTemperatureSensor",
                    Command = sensorCommand,
                    SensorValue = 25.392013,
                },
                new Sensor()
                {
                    Id = "2",
                    BoardId = "1",
                    SensorType = SensorType.Humidity,
                    Status = DeviceStatus.On,
                    Name = "TestHumiditySensor",
                    Command = sensorCommand,
                    SensorValue= 70,
                },
                new Sensor()
                {
                    Id = "3",
                    BoardId = "1",
                    SensorType = SensorType.Sunlight,
                    Status = DeviceStatus.On,
                    Name = "TestSunlightSensor",
                    Command = sensorCommand,
                    SensorValue = 99,
                },
            };

            sensorList = sensorList.OrderBy(x => x.SensorType).ToList();

            return await Task.FromResult(sensorList);
        }
    }
}
