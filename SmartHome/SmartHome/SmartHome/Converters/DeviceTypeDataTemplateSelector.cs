using SmartHome.Models;
using Xamarin.Forms;

namespace SmartHome.Converters
{
    public class DeviceTypeDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UnknownDeviceTemplate { get; set; }
        public DataTemplate SensorTemplate { get; set; }
        public DataTemplate LightSwitchTemplate { get; set; }

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
                    default:
                        break;
                }
            }

            return UnknownDeviceTemplate;
        }
    }
}
