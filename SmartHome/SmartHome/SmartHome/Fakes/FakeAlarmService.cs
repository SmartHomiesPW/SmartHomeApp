﻿using SmartHome.Models;
using SmartHome.Services;
using SmartHome.Services.AlarmService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    internal class FakeAlarmService : IAlarmService
    {
        public async Task<List<AlarmSensor>> GetAlarmSensors()
        {
            Func<object, Task<bool>> alarmSensorCommand = new Func<object, Task<bool>>(async (param) =>
            {
                bool result = false;
                if (param is AlarmSensor alarmSensor)
                {
                    result = (alarmSensor.Status == DeviceStatus.On) ? await AlarmSensorTurnOff(alarmSensor) : await AlarmSensorTurnOn(alarmSensor);
                }
                return result;
            });

            var alarmSensorList = new List<AlarmSensor>()
            {
                new AlarmSensor()
                {
                    Id= "53",
                    BoardId = "2",
                    Name = "Kitchen Movement Sensor",
                    Status = DeviceStatus.On,
                    Command = alarmSensorCommand,
                },
                new AlarmSensor()
                {
                    Id= "82",
                    BoardId = "1",
                    Name = "Living Room Movement Sensor",
                    Status = DeviceStatus.Off,
                    Command = alarmSensorCommand,
                },
                                new AlarmSensor()
                {
                    Id= "83",
                    BoardId = "1",
                    Name = "Living Room Movement Sensor",
                    Status = DeviceStatus.Off,
                    Command = alarmSensorCommand,
                },
                                                new AlarmSensor()
                {
                    Id= "43",
                    BoardId = "2",
                    Name = "Living Room Movement Sensor",
                    Status = DeviceStatus.On,
                    MovementDetected = true,
                    Command = alarmSensorCommand,
                }
            };

            return await Task.FromResult(alarmSensorList);
        }


        public async Task<bool> AlarmSensorTurnOn(AlarmSensor alarmSensor)
        {
            alarmSensor.Status = DeviceStatus.On;

            return await Task.FromResult(true);
        }
        public async Task<bool> AlarmSensorTurnOff(AlarmSensor alarmSensor)
        {
            alarmSensor.Status = DeviceStatus.Off;
            alarmSensor.MovementDetected = false;

            return await Task.FromResult(true);
        }

    }
}
