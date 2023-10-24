using FreshMvvmApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshMvvmApp.Services
{
    public interface IMachineService
    {
        public Task<List<IDevice>> GetDevices(string machineId);
    }
}
