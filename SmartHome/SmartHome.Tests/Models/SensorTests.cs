using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Models;
using SmartHome.Models.BackendModels;
using System.Collections.ObjectModel;

namespace SmartHome.Tests.Models
{
    [TestClass]
    public class SensorTests
    {
        [TestMethod]
        public void ConvertFromTemperatureBackendDtoCorrectly()
        {
            var temperatureSensorBackendDTO = new TemperatureSensorBackend()
            {
                sensor_Id = "1",
                system_Id = "1",
                value = 1,
                name = "Temperature sensor"
            };

            var sensor = new Sensor(temperatureSensorBackendDTO);

            sensor.SensorValue.Should().Be(1);
            sensor.Name.Should().Be(temperatureSensorBackendDTO.name);
        }

        [TestMethod]
        public void ConvertFromHumidityBackendDtoCorrectly()
        {
            var sensorValue = 51;

            var humiditySensorBackendDTO = new HumiditySensorBackend()
            {
                sensor_Id = "1",
                system_Id = "1",
                value = sensorValue,
                name = "Humidity sensor"
            };

            var sensor = new Sensor(humiditySensorBackendDTO);

            sensor.SensorValue.Should().Be(sensorValue);
            sensor.Name.Should().Be(humiditySensorBackendDTO.name);
        }

        [TestMethod]
        public void ConvertFromSunlightBackendDtoCorrectly()
        {
            var sensorValue = 50;

            var sunlightSensorBackendDTO = new SunlightSensorBackend()
            {
                sensor_Id = "1",
                system_Id = "1",
                value = sensorValue,
                name = "Sunlight sensor"
            };

            var sensor = new Sensor(sunlightSensorBackendDTO);

            sensor.SensorValue.Should().Be(sensorValue);
            sensor.Name.Should().Be(sunlightSensorBackendDTO.name);
        }
    }
}