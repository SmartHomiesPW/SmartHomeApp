using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Converters;
using SmartHome.Models;
using System.Globalization;

namespace SmartHome.Tests.Converters
{
    [TestClass]
    public class LightSwitchValueToStringConverterTests
    {
        private LightSwitchValueToStringConverter _converter = new LightSwitchValueToStringConverter();

        [TestMethod]
        public void ReturnOnWhenStatusIsOn()
        {
            var lightSwitch = new LightSwitch()
            {
                Status = DeviceStatus.On
            };

            string deviceStatus = (string)_converter.Convert(lightSwitch, typeof(string), "-", CultureInfo.CurrentCulture);
            deviceStatus.Should().Be("On");
        }
        [TestMethod]
        public void ReturnOffWhenStatusIsOff()
        {
            var lightSwitch = new LightSwitch()
            {
                Status = DeviceStatus.Off
            };

            string deviceStatus = (string)_converter.Convert(lightSwitch, typeof(string), "-", CultureInfo.CurrentCulture);
            deviceStatus.Should().Be("Off");
        }
        [TestMethod]
        public void ReturnUnknownWhenStatusIsUnknown()
        {
            var lightSwitch = new LightSwitch()
            {
                Status = DeviceStatus.Unknown
            };

            string deviceStatus = (string)_converter.Convert(lightSwitch, typeof(string), "-", CultureInfo.CurrentCulture);
            deviceStatus.Should().Be("Unknown");
        }
    }
}
