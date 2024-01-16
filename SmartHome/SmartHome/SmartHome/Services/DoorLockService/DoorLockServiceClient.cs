using RestSharp;
using SmartHome.Infrastructure.AppState;
using SmartHome.Models.BackendModels;
using SmartHome.Models;
using SmartHome.Services.LightSwitchService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Services.DoorLockService
{
    public class DoorLockServiceClient : IDoorLockService
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;
        private bool _isGetLightSwitchesInProgress = false;

        public DoorLockServiceClient(IAppState appState)
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

        public async Task<bool> DoorLockSetOff(DoorLock doorLock)
        {
            // the '1's should be taken from the user data. Hardcoded for now
            var baseLightSwitchString = $"1/board/1/devices/door-locks/set/{doorLock.Id}/0";
            try
            {
                var doorLockRestResponse = await _restClient.ExecutePutAsync(new RestRequest(baseLightSwitchString));
                if (doorLockRestResponse != null && doorLockRestResponse.IsSuccessful)
                {
                    doorLock.Status = DeviceStatus.Off;
                }
                return doorLockRestResponse.IsSuccessful;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DoorLockSetOn(DoorLock doorLock)
        {
            // the '1's should be taken from the user data. Hardcoded for now
            var baseLightSwitchString = $"1/board/1/devices/door-locks/set/{doorLock.Id}/1";
            try
            {
                var doorLockRestResponse = await _restClient.ExecutePutAsync(new RestRequest(baseLightSwitchString));
                if (doorLockRestResponse != null && doorLockRestResponse.IsSuccessful)
                {
                    doorLock.Status = DeviceStatus.On;
                }
                return doorLockRestResponse.IsSuccessful;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<DoorLock>> GetDoorLocks()
        {
            while (_isGetLightSwitchesInProgress)
            {
                await Task.Delay(100);
            }

            _isGetLightSwitchesInProgress = true;

            // the '1's should be taken from the user data. Hardcoded for now
            var baseLightSwitchString = "1/board/1/devices/door-locks/states";
            List<DoorLockBackend> doorLocksBackend = new List<DoorLockBackend>();
            try
            {
                var doorLocksRestResponse = await _restClient.ExecuteGetAsync<List<DoorLockBackend>>(new RestRequest(baseLightSwitchString));
                doorLocksBackend = doorLocksRestResponse.Data ?? doorLocksBackend;
            }
            catch
            {
                return new List<DoorLock>();
            }

            Func<object, Task<bool>> doorLockSwitchCommand = new Func<object, Task<bool>>(async (param) =>
            {
                bool commandResult = false;
                if (param is DoorLock doorLock)
                {
                    commandResult = (doorLock.Status == DeviceStatus.On) ? await DoorLockSetOff(doorLock) : await DoorLockSetOn(doorLock);
                }
                return commandResult;
            });

            var result = new List<DoorLock>();
            foreach (var doorLockBackend in doorLocksBackend)
            {
                result.Add(DoorLock.FromDoorLockBackend(doorLockBackend, doorLockSwitchCommand));
            }

            _isGetLightSwitchesInProgress = false;
            return await Task.FromResult(result);
        }
    }
}
