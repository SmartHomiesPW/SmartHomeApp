namespace SmartHome.Models.BackendModels
{
    public class LightSwitchBackend
    {
        public string switchable_Light_Id { get; set; }
        public string system_Id { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public int value { get; set; }
    }
}
