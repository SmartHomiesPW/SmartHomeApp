using RestSharp;
using SmartHome.Infrastructure.AppState;
using SmartHome.Models;
using SmartHome.Models.BackendModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Services.LightSwitchService
{
    public class LightSwitchServiceClient : ILightSwitchService
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;
        private bool _isGetLightSwitchesInProgress = false;

        public LightSwitchServiceClient(IAppState appState)
        {
            // set 'baseUrl' in 'appsettings.json' to
            // http://10.0.2.2:5239/api/system for local backend connection
            // need to disable UseHttpsRedirection(); in the local backend instance first to work

            var sensorServiceUri = appState.Configuration["endpoints:baseUrl"];

            _restClientOptions = new RestClientOptions(sensorServiceUri)
            {
                ThrowOnAnyError = false,
                MaxTimeout = 1000,
            };
            _restClient = new RestClient(_restClientOptions);
        }

        public async Task<List<LightSwitch>> GetLightSwitches()
        {
            while (_isGetLightSwitchesInProgress)
            {
                await Task.Delay(100);
            }

            _isGetLightSwitchesInProgress = true;

            // the '1's should be taken from the user data. Hardcoded for now
            var baseLightSwitchString = "1/board/1/devices/lights/states";
            List<LightSwitchBackend> lightSwitchesBackend = new List<LightSwitchBackend>();
            try
            {
                var lightsRestResponse = await _restClient.ExecuteGetAsync<List<LightSwitchBackend>>(new RestRequest(baseLightSwitchString));
                lightSwitchesBackend = lightsRestResponse.Data ?? lightSwitchesBackend;
            }
            catch
            {
                return new List<LightSwitch>();
            }

            Func<object, Task<bool>> lightSwitchCommand = new Func<object, Task<bool>>(async (param) =>
            {
                bool commandResult = false;
                if (param is LightSwitch lightSwitch)
                {
                    commandResult = (lightSwitch.Status == DeviceStatus.On) ? await LightTurnOff(lightSwitch) : await LightTurnOn(lightSwitch);
                }
                return commandResult;
            });

            var result = new List<LightSwitch>();
            foreach (var lightSwitchBackend in lightSwitchesBackend)
            {
                result.Add(LightSwitch.FromLightSwitchBackend(lightSwitchBackend, lightSwitchCommand));
            }

            _isGetLightSwitchesInProgress = false;
            return await Task.FromResult(result);
        }

        public async Task<bool> LightTurnOff(LightSwitch lightSwitch)
        {
            // the '1's should be taken from the user data. Hardcoded for now
            var baseLightSwitchString = "1/board/1/devices/lights/states";
            string body = $"[ {{ \"lightId\": {lightSwitch.Id}, \"isOn\": 0 }} ]";

            var request = new RestRequest(baseLightSwitchString).AddJsonBody(body);
            try
            {
                var putResponse = await _restClient.ExecutePutAsync(request);

                if (putResponse != null && putResponse.IsSuccessful)
                {
                    lightSwitch.Status = DeviceStatus.Off;
                }

                return putResponse.IsSuccessful;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> LightTurnOn(LightSwitch lightSwitch)
        {
            // the '1's should be taken from the user data. Hardcoded for now
            var baseLightSwitchString = "1/board/1/devices/lights/states";
            string body = $"[ {{ \"lightId\": {lightSwitch.Id}, \"isOn\": 1 }} ]";

            var request = new RestRequest(baseLightSwitchString).AddJsonBody(body);
            try
            {
                var putResponse = await _restClient.ExecutePutAsync(request);

                if (putResponse != null && putResponse.IsSuccessful)
                {
                    lightSwitch.Status = DeviceStatus.On;
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
