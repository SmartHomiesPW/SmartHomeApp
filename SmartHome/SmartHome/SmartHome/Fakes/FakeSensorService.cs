using SmartHome.Models;
using SmartHome.Services.SensorService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    class FakeSensorService : ISensorService
    {
        public async Task<ObservableCollection<SensorLog>> GetSensorLogs(Sensor sensor)
        {
            ObservableCollection<SensorLog> sensorLogs = null;

            switch (sensor.Id)
            {
                case "1":
                    sensorLogs = new ObservableCollection<SensorLog>() {
                                    new SensorLog
                                    {
                                        SensorId = "1",
                                        Value = 25,
                                        Time = DateTime.Now
                                    },
                                    new SensorLog
                                    {
                                        SensorId = "1",
                                        Value = 25,
                                        Time = DateTime.Now.AddMinutes(-3)
                                    }
                    };
                    break;
                case "2":
                    sensorLogs = new ObservableCollection<SensorLog>() {
                                    new SensorLog
                                    {
                                        SensorId = "2",
                                        Value = 70,
                                        Time = DateTime.Now
                                    },
                                    new SensorLog
                                    {
                                        SensorId = "2",
                                        Value = 69,
                                        Time = DateTime.Now.AddMinutes(-3)
                                    }
                    };
                    break;
                case "3":
                    sensorLogs = new ObservableCollection<SensorLog>() {
                                    new SensorLog
                                    {
                                        SensorId = "3",
                                        Value = 99,
                                        Time = DateTime.Now
                                    },
                    };
                    break;
                default:
                    break;
            };

            return await Task.FromResult(sensorLogs);
        }

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
                },
                new Sensor()
                {
                    Id = "2",
                    BoardId = "1",
                    SensorType = SensorType.Humidity,
                    Status = DeviceStatus.On,
                    Name = "TestHumiditySensor",
                    Command = sensorCommand,
                },
                            new Sensor()
                {
                    Id = "3",
                    BoardId = "1",
                    SensorType = SensorType.Sunlight,
                    Status = DeviceStatus.On,
                    Name = "TestSunlightSensor",
                    Command = sensorCommand,
                },};

            foreach (var sensor in sensorList)
            {
                sensor.Logs = await GetSensorLogs(sensor);
            }

            return await Task.FromResult(sensorList);
        }
    }
}
