using SmartHome.Models;
using Xamarin.Forms;

namespace SmartHome.Converters
{
    public class DeviceTypeDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UnknownDeviceTemplate { get; set; }
        public DataTemplate SensorTemplate { get; set; }
        public DataTemplate LightSwitchTemplate { get; set; }
        public DataTemplate AlarmSensorTemplate { get; set; }
        public DataTemplate DoorLockTemplate { get; set; }
        public DataTemplate CameraTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is IBoardDevice)
            {
                var device = item as IBoardDevice;
                switch (device.DeviceType)
                {
                    case BoardDeviceType.Sensor:
                        return SensorTemplate;
                    case BoardDeviceType.LightSwitch:
                        return LightSwitchTemplate;
                    case BoardDeviceType.AlarmSensor:
                        return AlarmSensorTemplate;
                    case BoardDeviceType.DoorLock:
                        return DoorLockTemplate;
                    case BoardDeviceType.Camera:
                        return CameraTemplate;
                    default:
                        return UnknownDeviceTemplate;
                }
            }

            return UnknownDeviceTemplate;
        }
    }
}
