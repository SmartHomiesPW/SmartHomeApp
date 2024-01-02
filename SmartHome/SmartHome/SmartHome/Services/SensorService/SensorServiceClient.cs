using RestSharp;
using SmartHome.Infrastructure.AppState;
using SmartHome.Models;
using SmartHome.Models.BackendModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static SmartHome.Models.BackendModels.SensorLogsBackend;

namespace SmartHome.Services.SensorService
{
    public class SensorServiceClient : ISensorService
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;
        private bool _isGetSensorsInProgress = false;

        public SensorServiceClient(IAppState appState)
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

        public async Task<List<Sensor>> GetSensors()
        {
            while (_isGetSensorsInProgress)
            {
                await Task.Delay(100);
            }

            _isGetSensorsInProgress = true;

            // the '1's should be taken from the user data. Hardcoded for now
            var baseSensorString = "1/board/1/devices/sensors/";
            List<TemperatureSensorBackend> temperatureSensors = new List<TemperatureSensorBackend>();
            List<HumiditySensorBackend> humiditySensors = new List<HumiditySensorBackend>();
            List<SunlightSensorBackend> sunlightSensors = new List<SunlightSensorBackend>();

            try
            {
                var temperatureRestResponse = await _restClient.ExecuteGetAsync<List<TemperatureSensorBackend>>(new RestRequest(baseSensorString + "temperature/states"));
                temperatureSensors = temperatureRestResponse.Data ?? temperatureSensors;
                var humidityRestResponse = await _restClient.ExecuteGetAsync<List<HumiditySensorBackend>>(new RestRequest(baseSensorString + "humidity/states"));
                humiditySensors = humidityRestResponse.Data ?? humiditySensors;
                var sunlightRestResponse = await _restClient.ExecuteGetAsync<List<SunlightSensorBackend>>(new RestRequest(baseSensorString + "sunlight/states"));
                sunlightSensors = sunlightRestResponse.Data ?? sunlightSensors;
            }
            catch
            {
                return new List<Sensor>();
            }
            finally
            {
                _isGetSensorsInProgress = false;
            }

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

            return await Task.FromResult(result);
        }
    }
}
