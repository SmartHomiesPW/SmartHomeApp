using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHome.Models.BackendModels
{
    public class AlarmSensorBackend
    {
        public string alarm_Sensor_Id { get; set; }
        public string alarm_Id { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public int is_On { get; set; }
        public int movement_Detected { get; set; }
    }
}
