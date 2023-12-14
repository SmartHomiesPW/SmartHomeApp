using System;

namespace SmartHome.Models
{
    public class SensorLog
    {
        public string SensorId;
        public double Value { get; set; }
        public DateTime Time { get; set; }
    }
}
