using SmartHome.Models;
using SmartHome.PageModels;
using SmartHome.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace SmartHome.Behaviors
{
    public class AlarmSensorLabelBehaviour : Behavior<Label>
    {
        private Label _associatedLabel = null;

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
            if (sender is Label label && label.BindingContext is AlarmSensor alarmSensor)
            {
                alarmSensor.PropertyChanged += OnAlarmSensorPropertyChanged;
                UpdateLabel(alarmSensor);
            }
        }

        private void OnAlarmSensorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is AlarmSensor && e.PropertyName == nameof(AlarmSensor.Status))
            {
                UpdateLabel(sender as AlarmSensor);
            }
        }

        private void UpdateLabel(AlarmSensor alarmSensor)
        {
            // (Grid)_associatedLabel.Parent.Parent is hardcoded to work specifically on AlarmSensorViewCell
            // Might need to be redone in the future, if need arises

            if (_associatedLabel == null) return;

            if (alarmSensor.Status == DeviceStatus.On && alarmSensor.MovementDetected)
            {
                _associatedLabel.Text = "Movement";
                object backgroundColor = Color.Transparent;
                Application.Current.Resources.TryGetValue("AlarmMovementBackgroundColor", out backgroundColor);
                backgroundColor = ((Color)backgroundColor).WithLuminosity(0.5);
                ((Grid)_associatedLabel.Parent.Parent).BackgroundColor = (Color)backgroundColor;
            }
            else
            {
                _associatedLabel.Text = alarmSensor.Status.ToString();
                object backgroundColor = Color.Transparent;
                if (alarmSensor.Status == DeviceStatus.On)
                    Application.Current.Resources.TryGetValue("DeviceOnBackgroundColor", out backgroundColor);
                else
                    Application.Current.Resources.TryGetValue("DeviceOffBackgroundColor", out backgroundColor);

                ((Grid)_associatedLabel.Parent.Parent).BackgroundColor = (Color)backgroundColor;
            }
        }
    }
}
