using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class Board : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<IBoardDevice> Devices { get; set; }

        public Func<object, Task<bool>> Command { get; set; }
    }
}
