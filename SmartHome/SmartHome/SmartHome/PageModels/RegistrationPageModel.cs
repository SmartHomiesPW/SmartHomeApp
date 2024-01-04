using FreshMvvm;
using SmartHome.Models;
using SmartHome.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SmartHome.PageModels
{
    public class RegistrationPageModel : BasePageModel
    {
        private IAuthenticationService _authenticationService;

        private string _email;
        private string _password;
        private bool _registrationFailed = false;

        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }

        public bool RegistrationFailed { get => _registrationFailed; set => SetProperty(ref _registrationFailed, value); }

        public ICommand RegisterButtonTapped { get; set; }

        public RegistrationPageModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            RegisterButtonTapped = new FreshAwaitCommand(async (obj) =>
            {
                User user = await _authenticationService.Register(Email, Password);
                if (user.Email != null)
                {
                    ((App)App.Current).SwitchToMainPage();
                }
                else
                {
                    RegistrationFailed = true;
                }
                obj.SetResult(true);
            });
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            RegistrationFailed = false;
        }
    }
}
