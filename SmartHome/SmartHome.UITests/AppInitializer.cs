using System;
using Xamarin.UITest;

namespace SmartHome.UITests
{
    public class AppInitializer
    {
        private const string ApkPath = "../../../SmartHome/SmartHome.Android/bin/Debug/com.companyname.smarthome.apk";
        //private const string ApkPath = "G:\\Inżynierka\\wip\\SmartHome\\SmartHome\\SmartHome.Android\\bin\\UITests\\com.companyname.smarthome.apk";

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
                    .ApkFile(ApkPath)
                    .EnableLocalScreenshots()
                    .StartApp();
            }

            return;
        }
    }
}