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

        //[TestInitialize]
        //public void Init()
        //{
        //    _converter = new SensorValueToStringConverter();
        //}

        [DataTestMethod]
        [DataRow(33, "33 °C")]
        public void ReturnDegreesCelsiusWhenSensorIsTemperature(double value, string result)
        {
            var sensorLogs = new ObservableCollection<SensorLog>() {
                                    new SensorLog
                                    {
                                        SensorId = "1",
                                        Value = value,
                                        Time = DateTime.Now
                                    },
                    };

            var sensor = new Sensor()
            {
                SensorType = SensorType.Temperature,
                Logs = sensorLogs
            };

            string sensorValue = (string)_converter.Convert(sensor, typeof(string), "-", CultureInfo.CurrentCulture);
            sensorValue.Should().Be(result);
        }

        [DataTestMethod]
        [DataRow(70, "70 %")]
        public void ReturnPercentWhenSensorIsHumidity(double value, string result)
        {
            var sensorLogs = new ObservableCollection<SensorLog>() {
                                    new SensorLog
                                    {
                                        SensorId = "1",
                                        Value = value,
                                        Time = DateTime.Now
                                    },
                    };

            var sensor = new Sensor()
            {
                SensorType = SensorType.Humidity,
                Logs = sensorLogs
            };

            string sensorValue = (string)_converter.Convert(sensor, typeof(string), "-", CultureInfo.CurrentCulture);
            sensorValue.Should().Be(result);
        }

        [DataTestMethod]
        [DataRow(70, "70 %")]
        [DataRow(23, "23 %")]
        public void ReturnPercentWhenSensorIsLight(double value, string result)
        {
            var sensorLogs = new ObservableCollection<SensorLog>() {
                                    new SensorLog
                                    {
                                        SensorId = "1",
                                        Value = value,
                                        Time = DateTime.Now
                                    },
                    };

            var sensor = new Sensor()
            {
                SensorType = SensorType.Light,
                Logs = sensorLogs
            };

            string sensorValue = (string)_converter.Convert(sensor, typeof(string), "-", CultureInfo.CurrentCulture);
            sensorValue.Should().Be(result);
        }
    }
}
