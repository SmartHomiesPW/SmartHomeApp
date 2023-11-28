using SmartHome.Models;
using SmartHome.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    class FakeBoardService : IBoardService
    {
        private ISensorService _sensorService { get; set; }
        private ILightSwitchService _lightSwitchService { get; set; }

        public FakeBoardService(ISensorService sensorService, ILightSwitchService lightSwitchService)
        {
            _sensorService = sensorService;
            _lightSwitchService = lightSwitchService;
        }

        public async Task<List<IBoardDevice>> GetDevices(string machineId)
        {
            var sensors = await _sensorService.GetSensors();
            var lightSwitches = await _lightSwitchService.GetLightSwitches();

            List<IBoardDevice> devices = new List<IBoardDevice>();

            devices.AddRange(sensors);
            devices.AddRange(lightSwitches);

            return await Task.FromResult(devices);
        }
    }
}
