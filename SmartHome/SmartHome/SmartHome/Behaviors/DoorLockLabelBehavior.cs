﻿using SmartHome.Models;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace SmartHome.Behaviors
{
    internal class DoorLockLabelBehavior : Behavior<Label>
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
            if (sender is Label label && label.BindingContext is DoorLock doorLock)
            {
                doorLock.PropertyChanged += OnDoorLockPropertyChanged;
                UpdateLabel(doorLock);
            }
        }

        private void OnDoorLockPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is DoorLock && e.PropertyName == nameof(DoorLock.Status))
            {
                UpdateLabel(sender as DoorLock);
            }
        }

        private void UpdateLabel(DoorLock doorLock)
        {
            // (Grid)_associatedLabel.Parent.Parent is hardcoded to work specifically on the viewcell
            // Might need to be redone in the future, if need arises

            if (_associatedLabel == null) return;

            _associatedLabel.Text = doorLock.DoorStatus.ToString();
            object backgroundColor = Color.Transparent;
            if (doorLock.Status == DeviceStatus.On)
                Application.Current.Resources.TryGetValue("DeviceOnBackgroundColor", out backgroundColor);
            else
                Application.Current.Resources.TryGetValue("DeviceOffBackgroundColor", out backgroundColor);

            ((Grid)_associatedLabel.Parent.Parent).BackgroundColor = (Color)backgroundColor;
        }

    }
}
