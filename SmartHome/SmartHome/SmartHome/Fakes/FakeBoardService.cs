﻿using SmartHome.Models;
using SmartHome.Services;
using SmartHome.Services.LightSwitchService;
using SmartHome.Services.SensorService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Fakes
{
    class FakeBoardService : IBoardService
    {
        private ISensorService _sensorService { get; set; }
        private ILightSwitchService _lightSwitchService { get; set; }
        private IAlarmService _alarmService { get; set; }
        private ICameraService _cameraService { get; set; }

        public Dictionary<string, Board> Boards { get; set; } = new Dictionary<string, Board>();

        public FakeBoardService(
            ISensorService sensorService,
            ILightSwitchService lightSwitchService,
            IAlarmService alarmService,
            ICameraService cameraService
            )
        {
            _sensorService = sensorService;
            _lightSwitchService = lightSwitchService;
            _alarmService = alarmService;
            _cameraService = cameraService;
        }

        public async Task<List<IBoardDevice>> GetAllDevicesForGivenBoard(string boardId)
        {
            if (boardId == "2") return new List<IBoardDevice>();

            var sensors = await _sensorService.GetSensors();
            var lightSwitches = await _lightSwitchService.GetLightSwitches();
            var alarmSensors = await _alarmService.GetAlarmSensors();
            var cameras = await _cameraService.GetCameras();

            List<IBoardDevice> devices = new List<IBoardDevice>();

            devices.AddRange(sensors);
            devices.AddRange(lightSwitches);
            devices.AddRange(alarmSensors);
            devices.AddRange(cameras);

            //Boards[boardId].Devices = new List<IBoardDevice>(devices);

            return await Task.FromResult(devices);
        }

        public async Task<List<Board>> GetBoards()
        {
            var boardsIds = new List<(string, string)> { ("1", "Living Room"), ("2", "Garage") };
            foreach ((string boardId, string boardName) in boardsIds)
            {
                if (Boards.ContainsKey(boardId)) continue;
                var boardDetails = new Board { Id = boardId, Name = boardName, Devices = await GetAllDevicesForGivenBoard(boardId) };
                Boards.Add(boardId, boardDetails);
            }
            return await Task.FromResult(Boards.Values.ToList());
        }
    }
}
