using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SmartHome.Models
{
    public class Sensor : IBoardDevice
    {
        public string Id { get; set; }
        public string BoardId { get; set; }
        public string Name { get; set; }
        public BoardDeviceType DeviceType { get => BoardDeviceType.Sensor; }
        public DeviceStatus Status { get; set; }
        public SensorType SensorType { get; set; }

        public List<SensorLog> Logs { get; set; }

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

        public ICommand Command { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
