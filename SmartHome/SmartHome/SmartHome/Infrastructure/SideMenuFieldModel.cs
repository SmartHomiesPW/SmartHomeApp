using System;
using System.Windows.Input;

namespace SmartHome.Infrastructure
{
    public class SideMenuFieldModel
    {
        public string Title { get; set; }
        public string DisplayText { get; set; }
        public ICommand Command { get; set; }
        public Type PageModelType { get; set; }
    }
}
