using System;
using System.Collections.Generic;
using System.Text;

namespace FreshMvvmApp.Models
{
    public class SensorLog
    {
        public string SensorId;
        public double Value { get; set; }
        public DateTime Time { get; set; }
    }
}
