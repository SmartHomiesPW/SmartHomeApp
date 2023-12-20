using RestSharp;
using SmartHome.Infrastructure.AppState;
using SmartHome.Models;
using SmartHome.Models.BackendModels;
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
            // https://localhost:5239/ for local backend connection
            // need to disable UseHttpsRedirection(); there first

            var sensorServiceUri = appState.Configuration["endpoints:baseUrl"];

            _restClientOptions = new RestClientOptions(sensorServiceUri)
            {
                ThrowOnAnyError = true,
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

            var lightSwitchesBackend = await _restClient.GetJsonAsync<List<LightSwitchBackend>>(baseLightSwitchString);

            var result = new List<LightSwitch>();
            foreach (var lightSwitchBackend in lightSwitchesBackend)
            {
                result.Add(LightSwitch.FromLightSwitchBackend(lightSwitchBackend));
            }

            _isGetLightSwitchesInProgress = false;
            return await Task.FromResult(result);
        }

        public async Task<bool> LightTurnOff(LightSwitch lightSwitch)
        {
            // the '1's should be taken from the user data. Hardcoded for now
            var baseLightSwitchString = "1/board/1/devices/lights/states";
            string body = $"[ {{ lightId: {lightSwitch.Id}, isOn: true }} ]";

            var request = new RestRequest(baseLightSwitchString).AddJsonBody(body);
            var postResponse = await _restClient.PostAsync(request);

            return postResponse.IsSuccessful;
        }

        public async Task<bool> LightTurnOn(LightSwitch lightSwitch)
        {
            // the '1's should be taken from the user data. Hardcoded for now
            var baseLightSwitchString = "1/board/1/devices/lights/states";
            string body = $"[ {{ lightId: {lightSwitch.Id}, isOn: false }} ]";

            var request = new RestRequest(baseLightSwitchString).AddJsonBody(body);
            var postResponse = await _restClient.PostAsync(request);

            return postResponse.IsSuccessful;
        }
    }
}
