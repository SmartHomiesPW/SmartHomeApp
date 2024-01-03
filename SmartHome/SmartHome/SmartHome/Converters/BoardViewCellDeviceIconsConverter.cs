using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SmartHome.Converters
{
    public class BoardViewCellDeviceIconsConverter : IValueConverter
    {
        private DeviceTypeToAsciiConverter _converter = new DeviceTypeToAsciiConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Board board = (Board)value;

            string deviceIcons = "";
            for (int i = 0; i < board.Devices.Count; i++)
            {
                deviceIcons += (string)_converter.Convert(board.Devices[i], typeof(string), "-", CultureInfo.CurrentCulture);
            }

            return deviceIcons;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
