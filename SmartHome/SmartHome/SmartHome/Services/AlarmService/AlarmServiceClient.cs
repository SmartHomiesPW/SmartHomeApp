using RestSharp;
using SmartHome.Infrastructure.AppState;
using SmartHome.Models;
using SmartHome.Models.BackendModels;
using SmartHome.Services.LightSwitchService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Services.AlarmService
{
    public class AlarmServiceClient : IAlarmService
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;
        private bool _isGetAlarmSensorsInProgress = false;

        public AlarmServiceClient(IAppState appState)
        {
            // https://localhost:5239/ for local backend connection
            // need to disable UseHttpsRedirection(); there first

            var sensorServiceUri = appState.Configuration["endpoints:baseUrl"];

            _restClientOptions = new RestClientOptions(sensorServiceUri)
            {
                ThrowOnAnyError = false,
                MaxTimeout = 1000,
            };
            _restClient = new RestClient(_restClientOptions);
        }

        public async Task<List<AlarmSensor>> GetAlarmSensors()
        {
            while (_isGetAlarmSensorsInProgress)
            {
                await Task.Delay(100);
            }

            _isGetAlarmSensorsInProgress = true;

            var baseLightSwitchString = $"1/board/1/devices/alarm/1/sensors";
            List<AlarmSensorBackend> alarmSensorsBackend = new List<AlarmSensorBackend>();
            try
            {
                alarmSensorsBackend = await _restClient.GetJsonAsync<List<AlarmSensorBackend>>(baseLightSwitchString);
            }
            catch { }

            Func<object, Task<bool>> alarmSensorCommand = new Func<object, Task<bool>>(async (param) =>
            {
                bool commandResult = false;
                if (param is AlarmSensor alarmSensor)
                {
                    commandResult = (alarmSensor.Status == DeviceStatus.On) ? await AlarmSensorTurnOff(alarmSensor) : await AlarmSensorTurnOn(alarmSensor);
                }
                return commandResult;
            });

            var result = new List<AlarmSensor>();
            foreach (var alarmSensorBackend in alarmSensorsBackend)
            {
                result.Add(AlarmSensor.FromAlarmSensorBackend(alarmSensorBackend, alarmSensorCommand));
            }

            _isGetAlarmSensorsInProgress = false;
            return await Task.FromResult(result);
        }
        public async Task<bool> AlarmSensorTurnOff(AlarmSensor alarmSensor)
        {
            var baseLightSwitchString = $"1/board/1/devices/alarm/{alarmSensor.BoardId}/sensors";
            string body = $"[ {{ \"alarmSensorId\": {alarmSensor.Id}, \"alarmId\": {alarmSensor.BoardId}, \"isOn\": 0, \"movementDetected\": 0 }} ]";

            var request = new RestRequest(baseLightSwitchString).AddJsonBody(body);
            try
            {
                var putResponse = await _restClient.PutAsync(request);

                if (putResponse != null && putResponse.IsSuccessful)
                {
                    alarmSensor.Status = DeviceStatus.Off;
                    alarmSensor.MovementDetected = false;
                }

                return putResponse.IsSuccessful;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AlarmSensorTurnOn(AlarmSensor alarmSensor)
        {
            var baseLightSwitchString = $"1/board/1/devices/alarm/{alarmSensor.BoardId}/sensors";
            string body = $"[ {{ \"alarmSensorId\": {alarmSensor.Id}, \"alarmId\": {alarmSensor.BoardId}, \"isOn\": 1, \"movementDetected\": 0 }} ]";

            var request = new RestRequest(baseLightSwitchString).AddJsonBody(body);
            try
            {
                var putResponse = await _restClient.PutAsync(request);

                if (putResponse != null && putResponse.IsSuccessful)
                {
                    alarmSensor.Status = DeviceStatus.On;
                    alarmSensor.MovementDetected = false;
                }

                return putResponse.IsSuccessful;
            }
            catch
            {
                return false;
            }
        }

    }
}
