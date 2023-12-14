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

        public ObservableCollection<SensorLog> Logs { get; set; }

        public double? SensorValue
        {
            get
            {
                if (Logs.Count > 0)
                {
                    Logs.OrderByDescending(o => o.Time).ToList();
                    return Logs[0].Value;
                }

                return null;
            }
        }

        public Func<object, Task<bool>> Command { get; set; }

        public static Sensor FromSensorBackend(SensorBackend sensor)
        {
            return new Sensor()
            {
                Id = sensor.sensor_Id,
                BoardId = sensor.system_Id,
                Status = DeviceStatus.On,
                Name = sensor.name,
                SensorType = SensorType.Unknown
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
            sensor.Logs = new ObservableCollection<SensorLog>(this.Logs);
            sensor.Command = this.Command;
        }
    }
}
