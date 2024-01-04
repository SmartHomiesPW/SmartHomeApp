using MvvmHelpers;
using System.Linq;

namespace SmartHome.Models
{
    public class User : ObservableObject
    {
        private string _id;
        private string _email;

        public string Id { get => _id; set => SetProperty(ref _id, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }

        public string Initials { get => Email?.First().ToString() ?? ""; }

        //public string Username { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }

        //public string Initials
        //{
        //    get
        //    {
        //        char first = FirstName?.Length > 0 ? FirstName[0] : ' ';
        //        char second = LastName?.Length > 0 ? LastName[0] : ' ';
        //        return $"{first}{second}".ToUpper();
        //    }
        //}
    }
}
