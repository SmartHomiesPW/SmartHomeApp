using System;
using System.Collections.Generic;
using System.Text;

namespace FreshMvvmApp.Models
{
    public interface IDevice
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
