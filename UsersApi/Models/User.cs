
namespace UsersApi.Models
{
    public class User
    {
        public User(string name, string gender, string email)
        {
            this.Name = name;
            this.Gender = gender;
            this.Email = email;
            this.Id = email;
        }
        public string Id { get; private set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
    }
}
