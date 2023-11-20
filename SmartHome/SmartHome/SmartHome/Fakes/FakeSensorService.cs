using SmartHome.Models;
using SmartHome.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    class FakeSensorService : ISensorService
    {
        public async Task<List<SensorLog>> GetSensorLogs(string sensorId)
        {
            List<SensorLog> sensorLogs = null;

            switch (sensorId)
            {
                case "1":
                    sensorLogs = new List<SensorLog>() {
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
                    sensorLogs = new List<SensorLog>() {
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
                default:
                    break;
            };

            return await Task.FromResult(sensorLogs);
        }

        public async Task<List<Sensor>> GetSensors()
        {
            List<Sensor> sensorList = new List<Sensor> {
                new Sensor()
                {
                    Id = "1",
                    BoardId = "1",
                    SensorType = SensorType.Temperature,
                    Status = DeviceStatus.On,
                    Name = "TestTemperatureSensor",
                },
                new Sensor()
                {
                    Id = "2",
                    BoardId = "1",
                    SensorType = SensorType.Humidity,
                    Status = DeviceStatus.On,
                    Name = "TestHumiditySensor",
                },};

            return await Task.FromResult(sensorList);
        }
    }
}
