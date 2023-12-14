using FreshMvvm;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SmartHome.Infrastructure
{
    public class CustomFreshMasterDetailNavigationContainer : FreshMasterDetailNavigationContainer
    {
        private Page _menuPage;
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();
        //private readonly Dictionary<string, IconModel> _pageIcons = new Dictionary<string, IconModel>();

        public CustomFreshMasterDetailNavigationContainer(string navigationServiceName) : base(navigationServiceName)
        {
            //On<iOS>().SetApplyShadow(true);
            MasterBehavior = MasterBehavior.Popover;
        }

        public void AddPage<T>(string title, string icon, object data = null) where T : FreshBasePageModel
        {
            // If this is the first page added, set it as default detail
            if (_pages.Count == 0)
            {
                Detail = ResolvePage(typeof(T), data);
            }

            _pages.Add(title, typeof(T));
            //_pageIcons.Add(title, new IconModel
            //{
            //    Text = title,
            //    Icon = icon,
            //    CurrentColor = currentColor,
            //    IsSvgIcon = isSvgIcon,
            //    Command = new Command<string>((pageTitle) =>
            //    {
            //        if (_pages.TryGetValue(pageTitle, out var newPageModel))
            //        {
            //            Detail = ResolvePage(newPageModel, data);
            //            IsPresented = false;
            //        }
            //    })
            //});

            //((SideMenuPageModel)_menuPage.BindingContext).SetPageIcons(_pageIcons);
        }

        private Page ResolvePage(Type pageModelType, object data)
        {
            var pageModel = FreshIOC.Container.Resolve(pageModelType);
            var page = FreshPageModelResolver.ResolvePageModel(pageModel.GetType(), data, (FreshBasePageModel)pageModel);
            page.GetModel().CurrentNavigationServiceName = NavigationServiceName;
            var containerPage = CreateContainerPage(page);
            return containerPage;
        }

        //public void AddAction(string title, string icon, ICommand command)
        //{
        //    _pageIcons.TryAdd(title, new IconModel { Text = title, Icon = icon, Command = command });
        //    ((SideMenuPageModel)_menuPage.BindingContext).SetPageIcons(_pageIcons);
        //}

        //protected override void CreateMenuPage(string menuPageTitle, string menuIcon = null)
        //{
        //    _menuPage = (ContentPage)FreshPageModelResolver.ResolvePageModel<SideMenuPageModel>();
        //    ((SideMenuPageModel)_menuPage.BindingContext).Title = menuPageTitle;
        //    var navPage = new NavigationPage(_menuPage) { Title = menuPageTitle };

        //    if (!string.IsNullOrEmpty(menuIcon))
        //    {
        //        navPage.IconImageSource = menuIcon;
        //    }

        //    Master = navPage;
        //}

        public void ClearPages()
        {
            Pages.Clear();
            PageNames.Clear();
            _pages.Clear();
            //_pageIcons.Clear();
        }
    }
}
