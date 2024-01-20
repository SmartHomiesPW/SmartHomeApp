using FluentAssertions;
using NUnit.Framework;
using Xamarin.UITest;

namespace SmartHome.UITests.Pages
{
    internal class AllDevicesPageTests : BaseTestFixture
    {
        public AllDevicesPageTests(Platform platform) : base(platform)
        {
        }

        [Test]
        public void ShouldShowSideMenu()
        {
            //App.Repl();
            AppUITestExtentions.ShowSideMenu(App);
            App.Query(x => x.Marked("SidePanelUserData")).Should().NotBeNull();
            //App.Screenshot("ShowSideMenuOnAllDevicesPage");
        }

        [Test]
        public void ShouldChangeLightStateWhenClicked()
        {
            // Switch active light off
            App.Tap(x => x.Marked("Kitchen Main Light_Status"));
            App.Query(x => x.Marked("Kitchen Main Light_Status").Text("Off")).Should().HaveCount(1);
            //App.Repl();
        }

        [Test]
        public void ShouldChangeAlarmStateWhenClicked()
        {
            // Switch alarm Movement Sensor off and on again
            App.WaitForElement("Kitchen Movement Sensor_Status", timeout: new System.TimeSpan(0, 0, 2));
            App.Query(x => x.Marked("Kitchen Movement Sensor_Status").Text("On")).Should().HaveCount(1);
            App.Tap(x => x.Marked("Kitchen Movement Sensor_Status"));
            App.Query(x => x.Marked("Kitchen Movement Sensor_Status").Text("Off")).Should().HaveCount(1);
            App.Tap(x => x.Marked("Kitchen Movement Sensor_Status"));
            App.Query(x => x.Marked("Kitchen Movement Sensor_Status").Text("On")).Should().HaveCount(1);
            //App.Repl();
        }
    }
}
