using SmartHome.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHome.Converters
{
    public class DeviceTypeToAsciiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IBoardDevice device = (IBoardDevice)value;

            switch (device.DeviceType)
            {
                case BoardDeviceType.Sensor:
                    Sensor sensor = (Sensor)value;
                    switch (sensor.SensorType)
                    {
                        case SensorType.Temperature:
                            return "🌡️";
                        case SensorType.Humidity:
                            return "💧";
                        case SensorType.Sunlight:
                            return "☀️";
                    }
                    break;
                case BoardDeviceType.LightSwitch:
                    return "💡";
                case BoardDeviceType.AlarmSensor:
                    return "🚨";
                case BoardDeviceType.Camera:
                    return "📹";
                case BoardDeviceType.DoorLock:
                    return "🔒";
                default:
                    return "";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
