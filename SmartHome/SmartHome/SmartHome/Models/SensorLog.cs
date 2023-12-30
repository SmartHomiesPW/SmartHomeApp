//using System;
//using System.Collections.ObjectModel;
//using static SmartHome.Models.BackendModels.SensorLogsBackend;

//namespace SmartHome.Models
//{
//    public class SensorLog
//    {
//        public string SensorId;
//        public double Value { get; set; }
//        public DateTime Time { get; set; }

//        public static SensorLog FromSensorMeasureBackend(SensorMeasureDto sensorMeasure)
//        {
//            return new SensorLog()
//            {
//                SensorId = sensorMeasure.sensor_Id.ToString(),
//                Time = DateTime.Now
//            };
//        }

//        public SensorLog() { }

//        public SensorLog(SensorMeasureDto sensorMeasure)
//        {
//            var sensorLog = FromSensorMeasureBackend(sensorMeasure);
//            sensorLog.Value = (double)sensorMeasure.value;
//            sensorLog.CopyTo(this);
//        }
//        //public SensorLog(HumiditySensorMeasureDto sensorMeasure)
//        //{
//        //    var sensorLog = FromSensorMeasureBackend(sensorMeasure);
//        //    sensorLog.Value = sensorMeasure.Humidity;
//        //    sensorLog.CopyTo(this);
//        //}
//        //public SensorLog(SunlightSensorMeasureDto sensorMeasure)
//        //{
//        //    var sensorLog = FromSensorMeasureBackend(sensorMeasure);
//        //    sensorLog.Value = sensorMeasure.LightValue;
//        //    sensorLog.CopyTo(this);
//        //}

//        public void CopyTo(SensorLog sensorLog)
//        {
//            sensorLog.SensorId = this.SensorId;
//            sensorLog.Time = this.Time;
//            sensorLog.Value = this.Value;
//        }
//    }
//}
