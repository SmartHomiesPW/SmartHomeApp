using FreshMvvmApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshMvvmApp.Services
{
    public interface ISensorService
    {
        public Task<Dictionary<SensorType, Sensor>> GetSensors();
        public Task<List<SensorLog>> GetSensorLogs(string sensorId);
        public Task<List<SensorLog>> RefreshSensorLogs(string sensorId);
    }
}
