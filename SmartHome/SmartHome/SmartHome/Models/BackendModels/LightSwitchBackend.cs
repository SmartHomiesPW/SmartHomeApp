namespace SmartHome.Models.BackendModels
{
    public class LightSwitchBackend
    {
        public string switchable_light_id { get; set; }
        public string system_id { get; set; }
        public int value { get; set; }
    }
}
