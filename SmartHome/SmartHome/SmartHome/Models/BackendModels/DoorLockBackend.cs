using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHome.Models.BackendModels
{
    public class DoorLockBackend
    {
        public string DoorLock_Id { get; set; }
        public string System_Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int IsOn { get; set; }
    }
}
