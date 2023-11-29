using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Models;
using System.Collections.ObjectModel;

namespace SmartHome.Tests.Models
{
    [TestClass]
    public class SensorTests
    {

        [TestMethod]
        public void SensorValueShouldBeFromTheMostRecentLog()
        {
            var sensorLogs = new ObservableCollection<SensorLog>() {
                                    new SensorLog
                                    {
                                        SensorId = "1",
                                        Value = 75,
                                        Time = DateTime.Now
                                    },
                                    new SensorLog
                                    {
                                        SensorId = "1",
                                        Value = 90,
                                        Time = DateTime.Now.AddMinutes(-3)
                                    }
                    };

            var sensor = new Sensor()
            {
                Id = "1",
                BoardId = "1",
                SensorType = SensorType.Humidity,
                Status = DeviceStatus.On,
                Name = "HumiditySensor",
                Logs = sensorLogs
            };

            sensor.SensorValue.Should().Be(75);
        }
    }
}