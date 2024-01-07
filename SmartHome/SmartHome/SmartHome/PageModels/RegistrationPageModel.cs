using FreshMvvm;
using SmartHome.Models;
using SmartHome.Services;
using System;
using System.Windows.Input;

namespace SmartHome.PageModels
{
    public class RegistrationPageModel : BasePageModel
    {
        private IAuthenticationService _authenticationService;

        private string _email;
        private string _password;
        private string _firstName;
        private string _lastName;
        private bool _registrationFailed = false;
        private bool _missingRequiredFields = false;

        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public string FirstName { get => _firstName; set => SetProperty(ref _firstName, value); }
        public string LastName { get => _lastName; set => SetProperty(ref _lastName, value); }

        public bool RegistrationFailed { get => _registrationFailed; set => SetProperty(ref _registrationFailed, value); }
        public bool MissingRequiredFields { get => _missingRequiredFields; set => SetProperty(ref _missingRequiredFields, value); }

        public ICommand RegisterButtonTapped { get; set; }

        public RegistrationPageModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            RegisterButtonTapped = new FreshAwaitCommand(async (obj) =>
            {
                if (string.IsNullOrEmpty(Email)
                || string.IsNullOrEmpty(Password)
                || string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName))
                {
                    MissingRequiredFields = true;
                    RegistrationFailed = false;
                    obj.SetResult(true);
                    return;
                }

                User user = await _authenticationService.Register(Email, Password, FirstName, LastName);
                if (user.Email != null)
                {
                    await CoreMethods.PopPageModel();
                    ((App)App.Current).SwitchToMainPage();
                }
                else
                {
                    MissingRequiredFields = false;
                    RegistrationFailed = true;
                }
                obj.SetResult(true);
            });
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            RegistrationFailed = false;
            MissingRequiredFields = false;
        }
    }
}
