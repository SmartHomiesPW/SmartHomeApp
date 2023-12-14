using RestSharp;
using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Services.SensorService
{
    public class SensorServiceClient : ISensorService
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;

        public SensorServiceClient()
        {
            // https://localhost:7239/

            _restClientOptions = new RestClientOptions("https://10.0.2.2:7239/api/system")
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
            var response = await _restClient.GetJsonAsync<List<Sensor>>("1/board/1/devices/sensors");

            var result = new List<Sensor>();
            return await Task.FromResult(result);
        }
    }
}
