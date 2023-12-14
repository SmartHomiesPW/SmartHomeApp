﻿using SmartHome.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Services
{
    public interface ILightSwitchService
    {
        Task<List<LightSwitch>> GetLightSwitches();

        Task<bool> LightTurnOn(LightSwitch lightSwitch);
        Task<bool> LightTurnOff(LightSwitch lightSwitch);
    }
}
