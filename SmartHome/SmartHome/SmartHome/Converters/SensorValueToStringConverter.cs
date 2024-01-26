using SmartHome.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHome.Converters
{
    public class SensorValueToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Sensor sensor = (Sensor)value;

            switch (sensor.SensorType)
            {
                case SensorType.Temperature:
                    return sensor.SensorValue.ToString("0.##") + " °C";
                case SensorType.Humidity:
                    return sensor.SensorValue.ToString("0.##") + " %";
                case SensorType.Sunlight:
                    return sensor.SensorValue.ToString("0.##") + " %";
                default:
                    return sensor.SensorValue.ToString("0.##");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
