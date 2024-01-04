using RestSharp;
using SmartHome.Infrastructure.AppState;
using SmartHome.Models;
using SmartHome.Models.BackendModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Services.AuthenticationService
{
    public class AuthenticationServiceClient : IAuthenticationService
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;
        private IAppState _appState;

        public List<AlarmSensor> AlarmSensors { get; set; } = new List<AlarmSensor>();

        public AuthenticationServiceClient(IAppState appState)
        {
            // set 'baseUrl' in 'appsettings.json' to
            // http://10.0.2.2:5239/api/system for local backend connection
            // need to disable UseHttpsRedirection(); in the local backend instance first to work

            _appState = appState;
            var sensorServiceUri = appState.Configuration["endpoints:authUrl"];

            _restClientOptions = new RestClientOptions(sensorServiceUri)
            {
                ThrowOnAnyError = false,
                MaxTimeout = 1000,
            };
            _restClient = new RestClient(_restClientOptions);
        }
        public async Task<User> Register(string email, string password)
        {
            var postRegister = $"register";
            try
            {
                string body = $"{{ \"email\": \"{email}\", \"password\": \"{password}\" }}";
                var request = new RestRequest(postRegister).AddBody(body);
                var response = await _restClient.ExecutePostAsync<User>(request);
                return response.Data ?? new User();
            }
            catch
            {
                return new User();
            }
        }

        public async Task<User> LogIn(string email, string password)
        {
            var postLogin = $"login";
            try
            {

                string body = $"{{ \"email\": \"{email}\", \"password\": \"{password}\" }}";
                var request = new RestRequest(postLogin).AddBody(body);
                var response = await _restClient.ExecutePostAsync<User>(request);
                return response.Data ?? new User();
            }
            catch
            {
                return new User();
            }
        }

        public Task<bool> LogOut()
        {
            _appState.UserData = null;
            return Task.FromResult(true);
        }
    }
}
