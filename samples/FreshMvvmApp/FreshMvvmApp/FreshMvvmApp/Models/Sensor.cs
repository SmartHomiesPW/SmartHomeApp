using System;
using System.Collections.Generic;
using System.Text;

namespace FreshMvvmApp.Models
{
    public class Sensor : IDevice
    {
        public string Id { get; set; }
        public SensorType Type { get; set; }
        public string Name { get; set; }
        public List<SensorLog> Logs { get; set; }
    }
}
