using SmartHome.Models;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace SmartHome.Behaviors
{
    internal class LightSwitchLabelBehavior : Behavior<Label>
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
            if (sender is Label label && label.BindingContext is LightSwitch lightSwitch)
            {
                lightSwitch.PropertyChanged += OnAlarmSensorPropertyChanged;
                UpdateLabel(lightSwitch);
            }
        }

        private void OnAlarmSensorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is LightSwitch && e.PropertyName == nameof(LightSwitch.Status))
            {
                UpdateLabel(sender as LightSwitch);
            }
        }

        private void UpdateLabel(LightSwitch lightSwitch)
        {
            // (Grid)_associatedLabel.Parent.Parent is hardcoded to work specifically on the viewcell
            // Might need to be redone in the future, if need arises

            if (_associatedLabel == null) return;

            _associatedLabel.Text = lightSwitch.Status.ToString();
            object backgroundColor = Color.Transparent;
            if (lightSwitch.Status == DeviceStatus.On)
                Application.Current.Resources.TryGetValue("DeviceOnBackgroundColor", out backgroundColor);
            else
                Application.Current.Resources.TryGetValue("DeviceOffBackgroundColor", out backgroundColor);

            ((Grid)_associatedLabel.Parent.Parent).BackgroundColor = (Color)backgroundColor;
        }

    }
}
