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
            AppUITestExtentions.ShowSideMenu(App);
            App.Screenshot("ShowSideMenuOnAllDevicesPage");
        }

        [Test]
        public void ShouldChangeLightStateWhenClicked()
        {
            App.Repl();
            App.Query(x => x.Marked("Kitchen Main Light")).Should().NotBeNull();

            App.Screenshot("ShowSideMenuOnAllDevicesPage");
        }
    }
}
