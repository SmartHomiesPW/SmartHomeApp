using SmartHome.Models;
using SmartHome.Services;
using SmartHome.Services.SensorService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    class FakeBoardService : IBoardService
    {
        private ISensorService _sensorService { get; set; }
        private ILightSwitchService _lightSwitchService { get; set; }
        private IAlarmService _alarmService { get; set; }

        public FakeBoardService(ISensorService sensorService, ILightSwitchService lightSwitchService, IAlarmService alarmService)
        {
            _sensorService = sensorService;
            _lightSwitchService = lightSwitchService;
            _alarmService = alarmService;
        }

        public async Task<List<IBoardDevice>> GetDevices(string machineId)
        {
            var sensors = await _sensorService.GetSensors();
            var lightSwitches = await _lightSwitchService.GetLightSwitches();
            var alarmSensors = await _alarmService.GetAlarmSensors();

            List<IBoardDevice> devices = new List<IBoardDevice>();

            devices.AddRange(sensors);
            devices.AddRange(lightSwitches);
            devices.AddRange(alarmSensors);

            return await Task.FromResult(devices);
        }
    }
}
