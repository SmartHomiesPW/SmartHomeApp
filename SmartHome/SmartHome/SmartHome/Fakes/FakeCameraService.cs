using SmartHome.Models;
using SmartHome.Services.CameraService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    public class FakeCameraService : ICameraService
    {
        public async Task<List<Camera>> GetCameras()
        {
            Func<object, Task<bool>> cameraCommand = new Func<object, Task<bool>>(async (param) =>
            {
                return await Task.FromResult(true);
            });

            List<Camera> cameraList = new List<Camera> {
                new Camera()
                {
                    Id = "9231",
                    BoardId = "1",
                    Status = DeviceStatus.On,
                    Name = "TestMainHallCamera",
                    Command = cameraCommand,
                },
                new Camera()
                {
                    Id = "32882",
                    BoardId = "1",
                    Status = DeviceStatus.Off,
                    Name = "TestDoorCamera",
                    Command = cameraCommand,
                },};

            return await Task.FromResult(cameraList);
        }

        public async Task<bool> CameraTurnOn(Camera camera)
        {
            camera.Status = DeviceStatus.On;

            return await Task.FromResult(true);
        }
        public async Task<bool> CameraTurnOff(Camera camera)
        {
            camera.Status = DeviceStatus.Off;

            return await Task.FromResult(true);
        }

        public Task<bool> CameraPairWith(Camera camera, AlarmSensor alarmSensor)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CameraSetRecordingSchedule(Camera camera, TimeSpan startTime, TimeSpan endTime)
        {
            camera.StartRecordingTime = startTime;
            camera.StopRecordingTime = endTime;

            return await Task.FromResult(true);
        }
    }
}
