using SmartHome.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Services
{
    public interface ISensorService
    {
        Task<List<Sensor>> GetSensors();
        Task<List<SensorLog>> GetSensorLogs(string sensorId);
    }
}
