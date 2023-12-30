using SmartHome.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Services
{
    public interface IAlarmService
    {
        Task<List<AlarmSensor>> GetAlarmSensors();

        Task<bool> AlarmSensorTurnOn(AlarmSensor alarmSensor);
        Task<bool> AlarmSensorTurnOff(AlarmSensor alarmSensor);

        Task<bool> AlarmSensorTurnOnAll();
        Task<bool> AlarmSensorTurnOffAll();
    }
}
