using System;
using Xamarin.UITest;

namespace SmartHome.UITests
{
    public static class AppUITestExtentions
    {
        public static void EnterText(this IApp app, string marked, string text)
        {
            app.WaitForElement(marked, timeout: new TimeSpan(0, 2, 0));
            app.EnterText(marked, text);
            //app.Screenshot("EnterText");
            app.PressEnter();
        }

        public static void ShowSideMenu(this IApp app)
        {
            app.DragCoordinates(0, 100, 2000, 100);
            return;
        }
    }
}
