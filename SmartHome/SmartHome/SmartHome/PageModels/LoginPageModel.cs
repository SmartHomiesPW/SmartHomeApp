using FreshMvvm;
using SmartHome.Models;
using SmartHome.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHome.PageModels
{
    public class LoginPageModel : BasePageModel
    {
        private IAuthenticationService _authenticationService;

        private string _email;
        private string _password;
        private bool _loginFailed = false;

        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public bool LoginFailed { get => _loginFailed; set => SetProperty(ref _loginFailed, value); }


        public ICommand LogInButtonTapped { get; set; }
        public ICommand GoToRegistrationButtonTapped { get; set; }

        public LoginPageModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            LogInButtonTapped = new FreshAwaitCommand(async (obj) =>
            {
                User user = await _authenticationService.LogIn(Email, Password);
                if (user.Email != null)
                {
                    ((App)App.Current).SwitchToMainPage();
                }
                else
                {
                    LoginFailed = true;
                }
                obj.SetResult(true);
            });
            GoToRegistrationButtonTapped = new FreshAwaitCommand(async (obj) =>
            {
                await CoreMethods.PushPageModel<RegistrationPageModel>(obj);
                obj.SetResult(true);
            });
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            LoginFailed = false;
        }
    }
}
