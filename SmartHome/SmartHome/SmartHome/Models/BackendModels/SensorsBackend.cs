namespace SmartHome.Models.BackendModels
{
    public interface SensorBackend
    {
        string sensor_Id { get; set; }
        string system_Id { get; set; }
        string name { get; set; }
        string details { get; set; }    
        decimal value { get; set; }
    }

    public class TemperatureSensorBackend : SensorBackend
    {
        public string sensor_Id { get; set; }
        public string system_Id { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public decimal value { get; set; }
    }

    public class SunlightSensorBackend : SensorBackend
    {
        public string sensor_Id { get; set; }
        public string system_Id { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public decimal value { get; set; }
    }

    public class HumiditySensorBackend : SensorBackend
    {
        public string sensor_Id { get; set; }
        public string system_Id { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public decimal value { get; set; }
    }
}
