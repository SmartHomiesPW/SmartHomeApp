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

        public async Task<ObservableCollection<SensorLog>> GetSensorLogs(Sensor sensor)
        {
            var baseSensorString = "1/board/1/devices/sensors/";
            baseSensorString += sensor.SensorType.ToString().ToLower() + "/states";
            // doesn't work, missing backend endpoints
            var result = new List<SensorLog>();
            switch (sensor.SensorType)
            {
                case SensorType.Temperature:
                    var response = await _restClient.GetAsync(new RestRequest(baseSensorString));

                    var tempSensorLogs = await _restClient.GetJsonAsync<List<TemperatureSensorMeasureDto>>(baseSensorString);
                    foreach (var temperatureLog in tempSensorLogs)
                    {
                        result.Add(new SensorLog(temperatureLog));
                    }
                    break;
                    //case SensorType.Humidity:
                    //    var humSensorLogs = await _restClient.GetJsonAsync<List<HumiditySensorMeasureDto>>(baseSensorString);
                    //    foreach (var humidityLog in humSensorLogs)
                    //    {
                    //        result.Add(new SensorLog(humidityLog));
                    //    }
                    //    break;
                    //case SensorType.Sunlight:
                    //    var sunSensorLogs = await _restClient.GetJsonAsync<List<SunlightSensorMeasureDto>>(baseSensorString);
                    //    foreach (var sunlightLog in sunSensorLogs)
                    //    {
                    //        result.Add(new SensorLog(sunlightLog));
                    //    }
                    //    break;
            }

            return await Task.FromResult(new ObservableCollection<SensorLog>(result));
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

            //try
            //{
            //    temperatureSensors = await _restClient.GetJsonAsync<List<TemperatureSensorBackend>>(baseSensorString + "temperature");
            //    humiditySensors = await _restClient.GetJsonAsync<List<HumiditySensorBackend>>(baseSensorString + "humidity");
            //    sunlightSensors = await _restClient.GetJsonAsync<List<SunlightSensorBackend>>(baseSensorString + "sunlight");
            //}
            //catch { }
            //finally { _isGetSensorsInProgress = false; }

            //var result = new List<Sensor>();
            //foreach (var temperatureSensor in temperatureSensors)
            //{
            //    result.Add(new Sensor(temperatureSensor));
            //}
            //foreach (var humiditySensor in humiditySensors)
            //{
            //    result.Add(new Sensor(humiditySensor));
            //}
            //foreach (var sunlightSensor in sunlightSensors)
            //{
            //    result.Add(new Sensor(sunlightSensor));
            //}

            var result = new List<Sensor>
            {
                new Sensor()
                {
                    Id = "9293",
                    SensorType = SensorType.Temperature,
                    Status = DeviceStatus.On,
                    Name = "Missing Name",
                }
            };

            foreach (var sensor in result)
            {
                sensor.Logs = await GetSensorLogs(sensor);
            }

            return await Task.FromResult(result);
        }
    }
}
