using NUnit.Framework;
using Xamarin.UITest;

namespace SmartHome.UITests
{
    [TestFixture(Platform.Android)]
    public abstract class BaseTestFixture
    {
        protected IApp App => AppInitializer.App;
        protected bool OnAndroid => AppInitializer.Platform == Platform.Android;

        protected BaseTestFixture(Platform platform)
        {
            AppInitializer.Platform = platform;
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            // Logs into the app through the fake auth service to show the main screen
            AppInitializer.StartApp(Platform.Android);
            AppUITestExtentions.EnterText(App, "EmailEntry", "K");
            App.Tap(x => x.Marked("LoginButton"));
        }
    }

}
