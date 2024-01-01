using SmartHome.Converters;
using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SmartHome.Behaviors
{
    public class BoardViewCellDeviceIconsLabelBehavior : Behavior<Label>
    {
        private Label _associatedLabel = null;
        private DeviceTypeToAsciiConverter _converter = new DeviceTypeToAsciiConverter();

        protected override void OnAttachedTo(Label label)
        {
            label.BindingContextChanged += OnBindingContextChanged;
            _associatedLabel = label;
        }

        protected override void OnDetachingFrom(Label label)
        {
            label.BindingContextChanged -= OnBindingContextChanged;
            _associatedLabel = null;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            if (sender is Label label && label.BindingContext is Board board)
            {
                UpdateLabel(board);
            }
        }

        private void UpdateLabel(Board board)
        {
            if (_associatedLabel == null) return;

            //char[] deviceIcons = new char[board.Devices.Count];
            string deviceIcons = "";
            for (int i = 0; i < board.Devices.Count; i++)
            {
                deviceIcons += (string)_converter.Convert(board.Devices[i], typeof(string), "-", CultureInfo.CurrentCulture);
            }

            _associatedLabel.Text = deviceIcons.ToString();
        }
    }
}
