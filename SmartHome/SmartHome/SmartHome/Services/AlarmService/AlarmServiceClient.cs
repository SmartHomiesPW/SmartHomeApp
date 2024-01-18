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

        public List<AlarmSensor> AlarmSensors { get; set; } = new List<AlarmSensor>();

        public AlarmServiceClient(IAppState appState)
        {
            // set 'baseUrl' in 'appsettings.json' to
            // http://10.0.2.2:5239/api/system for local backend connection
            // need to disable UseHttpsRedirection(); in the local backend instance first to work

            var sensorServiceUri = appState.Configuration["endpoints:baseUrl"];

            _restClientOptions = new RestClientOptions(sensorServiceUri)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ThrowOnAnyError = false,
                MaxTimeout = 5000,
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

            var getAlarmSensorString = $"1/board/1/devices/alarm/1/sensors";
            List<AlarmSensorBackend> alarmSensorsBackend = new List<AlarmSensorBackend>();
            try
            {
                var response = await _restClient.ExecuteGetAsync<List<AlarmSensorBackend>>(new RestRequest(getAlarmSensorString));
                alarmSensorsBackend = response.Data ?? alarmSensorsBackend;
            }
            catch
            {
                return new List<AlarmSensor>();
            }

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
            AlarmSensors = result;
            return await Task.FromResult(result);
        }
        public async Task<bool> AlarmSensorTurnOff(AlarmSensor alarmSensor)
        {
            var baseLightSwitchString = $"1/board/1/devices/alarm/{alarmSensor.BoardId}/sensors";
            string body = $"{{ \"alarmSensorId\": \"{alarmSensor.Id}\", \"alarmId\": \"{alarmSensor.BoardId}\", \"isOn\": 0, \"movementDetected\": 0 }}";

            var request = new RestRequest(baseLightSwitchString).AddJsonBody(body);
            try
            {
                var putResponse = await _restClient.ExecutePutAsync(request);

                if (putResponse != null && putResponse.IsSuccessful)
                {
                    UpdateLocalAlarmSensorState_TurnOff(alarmSensor);
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
            string body = $"{{ \"alarmSensorId\": \"{alarmSensor.Id}\", \"alarmId\": \"{alarmSensor.BoardId}\", \"isOn\": 1, \"movementDetected\": 0 }}";

            var request = new RestRequest(baseLightSwitchString).AddJsonBody(body);
            try
            {
                var putResponse = await _restClient.ExecutePutAsync(request);

                if (putResponse != null && putResponse.IsSuccessful)
                {
                    UpdateLocalAlarmSensorState_TurnOn(alarmSensor);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AlarmSensorTurnOnAll()
        {
            var baseLightSwitchString = $"1/board/1/devices/alarm/state";
            string body = $"{{ \"isActive\": 1, \"Alarm_Id\": \"1\" }}";

            var request = new RestRequest(baseLightSwitchString).AddJsonBody(body);
            try
            {
                var putResponse = await _restClient.ExecutePutAsync(request);

                if (putResponse != null && putResponse.IsSuccessful)
                {
                    foreach (AlarmSensor alarmSensor in AlarmSensors)
                    {
                        UpdateLocalAlarmSensorState_TurnOn(alarmSensor);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AlarmSensorTurnOffAll()
        {
            var baseLightSwitchString = $"1/board/1/devices/alarm/state";
            string body = $"{{ \"isActive\": 0, \"Alarm_Id\": \"1\" }}";

            var request = new RestRequest(baseLightSwitchString).AddJsonBody(body);
            try
            {
                var putResponse = await _restClient.ExecutePutAsync(request);

                if (putResponse != null && putResponse.IsSuccessful)
                {
                    foreach (AlarmSensor alarmSensor in AlarmSensors)
                    {
                        UpdateLocalAlarmSensorState_TurnOff(alarmSensor);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        private void UpdateLocalAlarmSensorState_TurnOn(AlarmSensor alarmSensor)
        {
            if (alarmSensor.Status == DeviceStatus.Off && alarmSensor.MovementDetected)
            {
                // Only change 'MovementDetected' to false if there might be a remaining 'true' on a switched off sensor
                // Prevents false negatives, e.g. if we already have a turned on sensor
                // and this would cause the visual cue to not display
                alarmSensor.MovementDetected = false;
            }

            alarmSensor.Status = DeviceStatus.On;
        }

        private void UpdateLocalAlarmSensorState_TurnOff(AlarmSensor alarmSensor)
        {
            alarmSensor.Status = DeviceStatus.Off;
            alarmSensor.MovementDetected = false;
        }
    }
}
