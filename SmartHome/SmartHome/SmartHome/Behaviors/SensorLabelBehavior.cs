using SmartHome.Converters;
using SmartHome.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHome.Behaviors
{
    public class SensorLabelBehavior : Behavior<Label>
    {
        private SensorValueToStringConverter _sensorValueConverter;
        private Label _associatedLabel = null;

        protected override void OnAttachedTo(Label label)
        {
            label.BindingContextChanged += OnBindingContextChanged;
            _associatedLabel = label;
            _sensorValueConverter = new SensorValueToStringConverter();
        }

        protected override void OnDetachingFrom(Label label)
        {
            label.BindingContextChanged -= OnBindingContextChanged;
            _associatedLabel = null;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            if (sender is Label label && label.BindingContext is Sensor sensor)
            {
                sensor.PropertyChanged += OnSensorPropertyChanged;
                UpdateLabel(sensor);
            }
        }

        private void OnSensorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Sensor
                && (e.PropertyName == nameof(Sensor.Status) || e.PropertyName == nameof(Sensor.Logs)))
            {
                UpdateLabel(sender as Sensor);
            }
        }

        private void UpdateLabel(Sensor sensor)
        {
            // (Grid)_associatedLabel.Parent.Parent is hardcoded to work specifically on AlarmSensorViewCell
            // Might need to be redone in the future, if need arises

            if (_associatedLabel == null) return;

            object backgroundColor = Color.Transparent;
            if (sensor.Status == DeviceStatus.On)
            {
                Application.Current.Resources.TryGetValue("DeviceOnBackgroundColor", out backgroundColor);
                _associatedLabel.Text = (string)_sensorValueConverter.Convert(sensor, null, null, CultureInfo.InvariantCulture);
            }
            else
            {
                Application.Current.Resources.TryGetValue("DeviceOffBackgroundColor", out backgroundColor);
                _associatedLabel.Text = sensor.Status.ToString();
            }

            ((Grid)_associatedLabel.Parent.Parent).BackgroundColor = (Color)backgroundColor;
        }
    }


}
