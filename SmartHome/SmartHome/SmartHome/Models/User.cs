namespace SmartHome.Models
{
    public class User
    {
        public string Id;
        public string Email { get; set; }

        public string Username { get; set; }
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
    }
}
