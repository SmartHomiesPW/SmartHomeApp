using MvvmHelpers;
using SmartHome.Models.BackendModels;

namespace SmartHome.Models
{
    public class User : ObservableObject
    {
        private string _id;
        private string _email;

        public string Id { get => _id; set => SetProperty(ref _id, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Initials
        {
            get
            {
                char first = FirstName?.Length > 0 ? FirstName[0] : ' ';
                char second = LastName?.Length > 0 ? LastName[0] : ' ';
                return $"{first}{second}".ToUpper();
            }
        }


        public User() { }

        // Converting from backend DTOs
        public User(UserBackend userBackendDTO)
        {
            Id = userBackendDTO.User_Id;
            Email = userBackendDTO.Email;
            FirstName = userBackendDTO.FirstName;
            LastName = userBackendDTO.LastName;
        }
    }
}
