using SmartHome.Models;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace SmartHome.Behaviors
{
    public class CameraLabelBehavior : Behavior<Label>
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
            if (sender is Label label && label.BindingContext is Camera camera)
            {
                camera.PropertyChanged += OnCameraPropertyChanged;
                UpdateLabel(camera);
            }
        }

        private void OnCameraPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Camera && e.PropertyName == nameof(Camera.Status))
            {
                UpdateLabel(sender as Camera);
            }
        }

        private void UpdateLabel(Camera camera)
        {
            // (Grid)_associatedLabel.Parent.Parent is hardcoded to work specifically on the viewcell
            // Might need to be redone in the future, if need arises

            if (_associatedLabel == null) return;

            _associatedLabel.Text = camera.Status.ToString();
            object backgroundColor = Color.Transparent;
            if (camera.Status == DeviceStatus.On)
                Application.Current.Resources.TryGetValue("DeviceOnBackgroundColor", out backgroundColor);
            else
                Application.Current.Resources.TryGetValue("DeviceOffBackgroundColor", out backgroundColor);

            ((Grid)_associatedLabel.Parent.Parent).BackgroundColor = (Color)backgroundColor;
        }
    }
}
