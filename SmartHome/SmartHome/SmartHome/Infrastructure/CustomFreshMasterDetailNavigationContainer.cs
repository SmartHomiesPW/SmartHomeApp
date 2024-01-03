using FreshMvvm;
using SmartHome.PageModels;
using SmartHome.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SmartHome.Infrastructure
{
    public class CustomFreshMasterDetailNavigationContainer : FreshMasterDetailNavigationContainer
    {
        private SideMenuPage _menuPage;
        private Dictionary<string, SideMenuFieldModel> _pages = new Dictionary<string, SideMenuFieldModel>();

        public CustomFreshMasterDetailNavigationContainer(string navigationServiceName) : base(navigationServiceName)
        {
            MasterBehavior = MasterBehavior.Popover;
        }

        protected override void CreateMenuPage(string menuPageTitle, string menuIcon = null)
        {
            _menuPage = (SideMenuPage)FreshPageModelResolver.ResolvePageModel<SideMenuPageModel>();

            var _menuPageModel = (SideMenuPageModel)_menuPage.BindingContext;
            NavigationPage navigationPage = new NavigationPage(_menuPage)
            {
                Title = _menuPageModel.AppState.UserData.Username,
            };
            if (!string.IsNullOrEmpty(menuIcon))
            {
                navigationPage.IconImageSource = menuIcon;
            }

            base.Master = navigationPage;
        }

        public void AddPage<T>(string title, string displayText, object data = null, bool isMainPage = true) where T : FreshBasePageModel
        {
            // If this is the first page added, set it as default detail
            if (_pages.Count == 0)
            {
                Detail = ResolvePage(typeof(T), data);
            }

            _pages.Add(
                    title,
                    new SideMenuFieldModel()
                    {
                        Title = title,
                        DisplayText = displayText,
                        Command = new Command<string>((pageTitle) =>
                        {
                            if (_pages.TryGetValue(pageTitle, out var newPageModel))
                            {
                                Detail = ResolvePage(newPageModel.PageModelType, data);
                                IsPresented = false;
                            }
                        }),
                        PageModelType = typeof(T),
                        IsMainPage = isMainPage,
                    }
                );
            ((SideMenuPageModel)_menuPage.BindingContext).PageFields.ReplaceRange(_pages.Values);
        }

        private Page ResolvePage(Type pageModelType, object data)
        {
            var pageModel = FreshIOC.Container.Resolve(pageModelType);
            var page = FreshPageModelResolver.ResolvePageModel(pageModel.GetType(), data, (FreshBasePageModel)pageModel);
            page.GetModel().CurrentNavigationServiceName = NavigationServiceName;
            var containerPage = CreateContainerPage(page);
            return containerPage;
        }
    }
}
