using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Converters;
using SmartHome.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SmartHome.Tests.Converters
{
    [TestClass]
    public class SensorValueToStringConverterTests
    {
        private SensorValueToStringConverter _converter = new SensorValueToStringConverter();

        [DataTestMethod]
        [DataRow(33, "33 °C")]
        public void ReturnDegreesCelsiusWhenSensorIsTemperature(double value, string result)
        {
            var sensor = new Sensor()
            {
                SensorType = SensorType.Temperature,
                SensorValue = value
            };

            string sensorValue = (string)_converter.Convert(sensor, typeof(string), "-", CultureInfo.CurrentCulture);
            sensorValue.Should().Be(result);
        }

        [DataTestMethod]
        [DataRow(70, "70 %")]
        public void ReturnPercentWhenSensorIsHumidity(double value, string result)
        {
            var sensor = new Sensor()
            {
                SensorType = SensorType.Humidity,
                SensorValue = value
            };

            string sensorValue = (string)_converter.Convert(sensor, typeof(string), "-", CultureInfo.CurrentCulture);
            sensorValue.Should().Be(result);
        }

        [DataTestMethod]
        [DataRow(70, "70 %")]
        [DataRow(23, "23 %")]
        public void ReturnPercentWhenSensorIsLight(double value, string result)
        {
            var sensor = new Sensor()
            {
                SensorType = SensorType.Sunlight,
                SensorValue = value
            };

            string sensorValue = (string)_converter.Convert(sensor, typeof(string), "-", CultureInfo.CurrentCulture);
            sensorValue.Should().Be(result);
        }
    }
}
