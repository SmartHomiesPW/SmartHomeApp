using System;
using Xamarin.UITest;

namespace SmartHome.UITests
{
    public class AppInitializer
    {
        // UI tests require Android Development Kit and Java Development Kit
        // (you will be prompted to install them by Visual Studio, in case you miss them)

        // To correctly launch UI tests, you need to first define (uncomment) the FAKES
        // variable in App.xaml.cs. Next step is open the emulator and build and run the app
        // with the Release config. Don't close the emulator.
        // Then you can launch the UI tests safely.

        private static IApp _app = null;
        private static Platform? _platform;

        public static IApp App
        {
            get => _app ?? throw new NullReferenceException("App was null");
            set => _app = value;
        }
        public static Platform Platform
        {
            get => _platform ?? throw new NullReferenceException("Platform was null");
            set => _platform = value;
        }

        public static void StartApp(Platform platform)
        {
            // only testing on Android

            _platform = platform;

            if (platform == Platform.Android)
            {
                App = ConfigureApp.Android
                    .InstalledApp("com.smarthome.smarthome")
                    .EnableLocalScreenshots()
                    .StartApp();
            }

            return;
        }
    }
}