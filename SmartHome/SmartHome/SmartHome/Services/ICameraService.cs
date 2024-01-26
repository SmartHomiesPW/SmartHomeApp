using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Services.CameraService
{
    public interface ICameraService
    {
        // No counterpart on the backend and no real implementation in the app.
        // Left for future reference

        Task<List<Camera>> GetCameras();

        Task<bool> CameraTurnOn(Camera camera);
        Task<bool> CameraTurnOff(Camera camera);

        Task<bool> CameraSetRecordingSchedule(Camera camera, TimeSpan startHour, TimeSpan endHour);
        Task<bool> CameraPairWith(Camera camera, AlarmSensor alarmSensor);
    }
}
