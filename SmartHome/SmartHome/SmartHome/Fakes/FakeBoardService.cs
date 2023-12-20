using SmartHome.Services;

namespace SmartHome.Fakes
{
    class FakeBoardService : IBoardService
    {
        //private ISensorService _sensorService { get; set; }
        //private ILightSwitchService _lightSwitchService { get; set; }
        //private IAlarmService _alarmService { get; set; }
        //private ICameraService _cameraService { get; set; }

        //public FakeBoardService(
        //    ISensorService sensorService,
        //    ILightSwitchService lightSwitchService,
        //    IAlarmService alarmService,
        //    ICameraService cameraService
        //    )
        //{
        //    _sensorService = sensorService;
        //    _lightSwitchService = lightSwitchService;
        //    _alarmService = alarmService;
        //    _cameraService = cameraService;
        //}

        //public async Task<List<IBoardDevice>> GetDevices(string machineId)
        //{
        //    var sensors = await _sensorService.GetSensors();
        //    var lightSwitches = await _lightSwitchService.GetLightSwitches();
        //    var alarmSensors = await _alarmService.GetAlarmSensors();
        //    var cameras = await _cameraService.GetCameras();

        //    List<IBoardDevice> devices = new List<IBoardDevice>();

        //    devices.AddRange(sensors);
        //    devices.AddRange(lightSwitches);
        //    devices.AddRange(alarmSensors);
        //    devices.AddRange(cameras);

        //    return await Task.FromResult(devices);
        //}
    }
}
