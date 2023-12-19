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
        private bool _isGetSensorsInProgress = false;

        public SensorServiceClient(IAppState appState)
        {
            // https://localhost:5239/ for local backend connection
            // need to disable UseHttpsRedirection(); there first

            var sensorServiceUri = appState.Configuration["endpoints:sensorService"];

            _restClientOptions = new RestClientOptions(sensorServiceUri)
            {
                ThrowOnAnyError = true,
                MaxTimeout = 1000,
            };
            _restClient = new RestClient(_restClientOptions);
        }

        public async Task<ObservableCollection<SensorLog>> GetSensorLogs(Sensor sensor)
        {
            var baseSensorString = "1/board/1/devices/sensors/";
            var sensorLogs = await _restClient.GetJsonAsync<List<SensorLog>>(baseSensorString + sensor.SensorType.ToString());
            // doesn't work, missing backend endpoints

            return await Task.FromResult(new ObservableCollection<SensorLog>());
        }

        public async Task<List<Sensor>> GetSensors()
        {
            while (_isGetSensorsInProgress)
            {
                await Task.Delay(100);
            }

            _isGetSensorsInProgress = true;

            // the '1's should be taken from the user data. Hardcoded for now
            var baseSensorString = "1/board/1/devices/sensors/";

            var temperatureSensors = await _restClient.GetJsonAsync<List<TemperatureSensorBackend>>(baseSensorString + "temperature");
            var humiditySensors = await _restClient.GetJsonAsync<List<HumiditySensorBackend>>(baseSensorString + "humidity");
            var sunlightSensors = await _restClient.GetJsonAsync<List<SunlightSensorBackend>>(baseSensorString + "sunlight");

            var result = new List<Sensor>();
            foreach (var temperatureSensor in temperatureSensors)
            {
                result.Add(new Sensor(temperatureSensor));
            }
            foreach (var humiditySensor in humiditySensors)
            {
                result.Add(new Sensor(humiditySensor));
            }
            foreach (var sunlightSensor in sunlightSensors)
            {
                result.Add(new Sensor(sunlightSensor));
            }

            _isGetSensorsInProgress = false;
            return await Task.FromResult(result);
        }
    }
}
