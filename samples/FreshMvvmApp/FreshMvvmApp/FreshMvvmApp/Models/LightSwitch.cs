using System;
using System.Collections.Generic;
using System.Text;

namespace FreshMvvmApp.Models
{
    public class LightSwitch : IDevice
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
