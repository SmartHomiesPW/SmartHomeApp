using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Converters;
using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Tests.Converters
{
    [TestClass]
    public class BoardDevicesNumberConverterTests
    {
        private BoardDevicesNumberConverter _converter = new BoardDevicesNumberConverter();


        [TestMethod]
        public void ShouldReturnNoDevicesWhenNoDevices()
        {
            var board = new Board();

            string text = (string)_converter.Convert(board, typeof(string), "-", CultureInfo.CurrentCulture);
            text.Should().Be("No devices");
        }
        [TestMethod]
        public void ShouldReturn1DeviceWhen1Device()
        {
            var board = new Board();
            board.Devices.Add(new Sensor() { SensorType = SensorType.Temperature });

            string text = (string)_converter.Convert(board, typeof(string), "-", CultureInfo.CurrentCulture);
            text.Should().Be("1 device");
        }
        [TestMethod]
        public void ShouldReturnXDevicesWhenMoreThan1Device()
        {
            var board = new Board();
            board.Devices.Add(new Sensor() { SensorType = SensorType.Temperature });
            board.Devices.Add(new Sensor() { SensorType = SensorType.Temperature });

            string text = (string)_converter.Convert(board, typeof(string), "-", CultureInfo.CurrentCulture);
            text.Should().Be("2 devices");

            board.Devices.Add(new Sensor() { SensorType = SensorType.Temperature });

            text = (string)_converter.Convert(board, typeof(string), "-", CultureInfo.CurrentCulture);
            text.Should().Be("3 devices");
        }
    }
}
