using System.Collections.Generic;

namespace UsersApi.Models
{
    public class Country
    {
        //public int Id { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
