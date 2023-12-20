using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHome.Infrastructure.AppService
{
    class AppService
    {
        public Application CurrentApp => Application.Current;

        public double Height => CurrentApp.MainPage.Height;

        public double Width => CurrentApp.MainPage.Width;

        //public Page PageOnTop => CurrentApp.MainPage.GetHandlerOnTop<Page>();

        public async Task ShowMenu(bool showMenu)
        {
            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    ((FreshMasterDetailNavigationContainer)CurrentApp.MainPage).IsPresented = showMenu;
            //});

            ////HACK: It is necessary to show properly the menu animation on android. Otherwise the animation is laggy if something (animation) is happening in the background
            //if (Device.RuntimePlatform == Device.Android)
            //{
            //    await Task.Delay(200);
            //}
        }
    }
}
