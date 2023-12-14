using RestSharp;
using SmartHome.Infrastructure.AppState;
using SmartHome.Models;
using SmartHome.Models.BackendModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SmartHome.Services.SensorService
{
    public class SensorServiceClient : ISensorService
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;

        public SensorServiceClient(IAppState appState)
        {
            // https://localhost:5239/
            // Disable UseHttpsRedirection(); in backend first

            var sensorServiceUri = appState.Configuration["endpoints:sensorService"];

            _restClientOptions = new RestClientOptions(sensorServiceUri)
            {
                ThrowOnAnyError = true,
                MaxTimeout = 1000,
            };
            _restClient = new RestClient(_restClientOptions);
        }

        public async Task<ObservableCollection<SensorLog>> GetSensorLogs(string sensorId)
        {
            return await Task.FromResult(new ObservableCollection<SensorLog>());
        }

        public async Task<List<Sensor>> GetSensors()
        {
            //var request = new RestRequest("1/board/1/devices/sensors");
            //var httpClient = new HttpClient();
            //var response = await httpClient.GetAsync("http://10.0.2.2:5239/api/system/1/board/1/devices/sensors/temperature");
            var request = new RestRequest("1/board/1/devices/sensors/temperature");
            //var response = await _restClient.GetAsync(request);

            var baseSensorString = "1/board/1/devices/sensors/";

            var response1 = await _restClient.GetAsync(request);

            var response = await _restClient.GetJsonAsync<List<SunlightSensorBackend>>(baseSensorString + "temperature");

            var result = new List<Sensor>();
            return await Task.FromResult(result);
        }
    }
}
