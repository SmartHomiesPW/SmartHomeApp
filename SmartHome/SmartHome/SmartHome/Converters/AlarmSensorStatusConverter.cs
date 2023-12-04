using SmartHome.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHome.Converters
{
    public class AlarmSensorStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AlarmSensor alarmSensor = (AlarmSensor)value;

            if (alarmSensor.Status == DeviceStatus.On && alarmSensor.MovementDetected)
            {
                return "Movement";
            }
            else
            {
                return alarmSensor.Status.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
