using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHome.Models.BackendModels
{
    public class SensorLogsBackend
    {
        public interface SensorMeasureDto
        {
            DateTime dateTime { get; set; }
            int sensorId { get; set; }
        }

        public class TemperatureSensorMeasureDto : SensorMeasureDto
        {
            public double temperature { get; set; }
            public DateTime dateTime { get; set; }
            public int sensorId { get; set; }
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
