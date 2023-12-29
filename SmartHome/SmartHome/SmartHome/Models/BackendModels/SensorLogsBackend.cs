using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHome.Models.BackendModels
{
    public class SensorLogsBackend
    {
        public class SensorMeasureDto
        {
            public int sensor_Id { get; set; }
            public int system_Id { get; set; }
            public string name { get; set; }
            public string details { get; set; }
            public decimal value { get; set; }
        }

        //public class HumiditySensorMeasureDto : SensorMeasureDto
        //{
        //    public double humidity { get; set; }
        //    public DateTime dateTime { get; set; }
        //    public int sensorId { get; set; }
        //}
        //public class SunlightSensorMeasureDto : SensorMeasureDto
        //{
        //    public double LightValue { get; set; }
        //    public DateTime DateTime { get; set; }
        //    public int SensorId { get; set; }
        //}
    }
}
