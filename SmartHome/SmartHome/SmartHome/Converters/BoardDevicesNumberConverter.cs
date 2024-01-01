using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SmartHome.Converters
{
    public class BoardDevicesNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Board board = (Board)value;

            if (board.Devices == null || board.Devices.Count <= 0)
            {
                return "No devices";
            }

            if (board.Devices.Count == 1) return "1 device";

            return board.Devices.Count.ToString() + " devices";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
