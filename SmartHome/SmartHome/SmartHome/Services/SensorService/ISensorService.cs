using SmartHome.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SmartHome.Services.SensorService
{
    public interface ISensorService
    {
        Task<List<Sensor>> GetSensors();
        Task<ObservableCollection<SensorLog>> GetSensorLogs(Sensor sensor);
    }
}
