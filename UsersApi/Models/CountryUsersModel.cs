using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApi.BusinessLayer;

namespace UsersApi.Models
{
    public class CountryUsersModel//Dictionary<string, List<User>>
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }//should be UserModel

        public CountryUsersModel()
        {
            this.Users = new List<User>();
        }
    }
}
