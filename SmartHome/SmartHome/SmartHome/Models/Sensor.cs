using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class Sensor : ObservableObject, IBoardDevice
    {
        private string _name;
        private DeviceStatus _status;

        public string Id { get; set; }
        public string BoardId { get; set; }
        public BoardDeviceType DeviceType { get => BoardDeviceType.Sensor; }
        public SensorType SensorType { get; set; }

        public DeviceStatus Status { get => _status; set => SetProperty(ref _status, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        public ObservableCollection<SensorLog> Logs { get; set; }

        public double SensorValue
        {
            get
            {
                if (Logs.Count > 0)
                {
                    Logs.OrderByDescending(o => o.Time).ToList();
                    return Logs[0].Value;
                }

                return 0;
            }
        }

        public Func<object, Task<bool>> Command { get; set; }
    }
}
