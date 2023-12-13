using MvvmHelpers;
using System;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class Camera : ObservableObject, IBoardDevice
    {
        private string _name;
        private DeviceStatus _status;
        private TimeSpan _startRecordingTime;
        private TimeSpan _stopRecordingTime;
        private ObservableRangeCollection<AlarmSensor> _pairedSensors;

        public string Id { get; set; }
        public string BoardId { get; set; }
        public BoardDeviceType DeviceType { get => BoardDeviceType.Camera; }

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public DeviceStatus Status { get => _status; set => SetProperty(ref _status, value); }

        public TimeSpan StartRecordingTime { get => _startRecordingTime; set => SetProperty(ref _startRecordingTime, value); }
        public TimeSpan StopRecordingTime { get => _stopRecordingTime; set => SetProperty(ref _stopRecordingTime, value); }

        public ObservableRangeCollection<AlarmSensor> PairedSensors { get => _pairedSensors; set => SetProperty(ref _pairedSensors, value); }

        public Func<object, Task<bool>> Command { get; set; }
    }
}
