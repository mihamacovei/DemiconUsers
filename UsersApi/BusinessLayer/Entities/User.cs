using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApi.BusinessLayer
{
    public class User
    {
        public User()
        {

        }
        public User(string name, string username, string gender, string email, string country)
        {
            this.Name = name;
            this.Gender = gender;
            this.Email = email;
            this.Country = Country;
            this.Username = email;
            this.Id = email;
        }

        [Key]
        public string Id { get; private set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Username { get; set; }
    }
}
