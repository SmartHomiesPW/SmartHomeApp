using SmartHome.PageModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartHome.Pages
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var basePageModel = this.BindingContext as FreshMvvm.FreshBasePageModel;
        }
    }
}
