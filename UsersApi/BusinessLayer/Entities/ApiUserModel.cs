
using System.ComponentModel.DataAnnotations;

namespace UsersApi.BusinessLayer
{
    public class ApiUserModel
    {
        public string Gender { get; set; }
        public Name Name { get; set; }
        public Login Login { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
    }

    public class Location
    {
        public string Country { get; set; }
    }

    public class Name
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class Login
    {
        public string Username { get; set; }
    }
}
