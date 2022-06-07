using System.Collections.Generic;

namespace UsersApi.Models
{
    public class Country
    {
        public Country(string name)
        {
            this.Name = name;
            this.Id = name;
        }
        public string Id { get; private set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
