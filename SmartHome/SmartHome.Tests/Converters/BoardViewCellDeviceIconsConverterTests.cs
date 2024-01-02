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
    public class BoardViewCellDeviceIconsConverterTests
    {
        private BoardViewCellDeviceIconsConverter _iconConverter = new BoardViewCellDeviceIconsConverter();
        private DeviceTypeToAsciiConverter _deviceTypeConverter = new DeviceTypeToAsciiConverter();

        [TestMethod]
        public void ShouldReturnCorrectNumberOfIcons()
        {
            Board board = new Board();
            var iconString = "";

            IBoardDevice device = new Sensor() { SensorType = SensorType.Temperature };

            board.Devices.Add(device);
            iconString += (string)_deviceTypeConverter.Convert(device, typeof(string), "-", CultureInfo.CurrentCulture);
            CheckLength(board, iconString);

            device = new LightSwitch() { Status = DeviceStatus.On };
            board.Devices.Add(device);
            iconString += (string)_deviceTypeConverter.Convert(device, typeof(string), "-", CultureInfo.CurrentCulture);
            CheckLength(board, iconString);

            device = new Sensor() { SensorType = SensorType.Humidity };
            board.Devices.Add(device);
            iconString += (string)_deviceTypeConverter.Convert(device, typeof(string), "-", CultureInfo.CurrentCulture);
            CheckLength(board, iconString);
        }

        private void CheckLength(Board board, string checkString)
        {
            string deviceIcons = (string)_iconConverter.Convert(board, typeof(string), "-", CultureInfo.CurrentCulture);
            deviceIcons.Should().HaveLength(checkString.Length);
        }
    }
}
