using SmartHome.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHome.Converters
{
    public class DoorLockValueToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DoorLock doorLock = (DoorLock)value;

            return doorLock.Status.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
