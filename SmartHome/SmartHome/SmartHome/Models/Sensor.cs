using MvvmHelpers;
using SmartHome.Models.BackendModels;
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
        public double SensorValue { get; set; }

        public Func<object, Task<bool>> Command { get; set; }


        // Converting from backend DTOs
        public static Sensor FromSensorBackend(SensorBackend sensor)
        {
            return new Sensor()
            {
                Id = sensor.sensor_Id,
                BoardId = sensor.system_Id,
                Status = DeviceStatus.On,
                Name = sensor.name,
                SensorValue = (double)sensor.value,
                SensorType = SensorType.Unknown,
            };
        }

        public Sensor() { }

        public Sensor(TemperatureSensorBackend sensor)
        {
            var appSensor = Sensor.FromSensorBackend(sensor);
            appSensor.SensorType = SensorType.Temperature;
            appSensor.CopyTo(this);
        }

        public Sensor(HumiditySensorBackend sensor)
        {
            var appSensor = Sensor.FromSensorBackend(sensor);
            appSensor.SensorType = SensorType.Humidity;
            appSensor.CopyTo(this);
        }

        public Sensor(SunlightSensorBackend sensor)
        {
            var appSensor = Sensor.FromSensorBackend(sensor);
            appSensor.SensorType = SensorType.Sunlight;
            appSensor.CopyTo(this);
        }

        public void CopyTo(Sensor sensor)
        {
            sensor.Id = this.Id;
            sensor.BoardId = this.BoardId;
            sensor.SensorType = this.SensorType;
            sensor.Status = this.Status;
            sensor.Name = this.Name;
            sensor.Command = this.Command;
            sensor.SensorValue = this.SensorValue;
        }
    }
}
