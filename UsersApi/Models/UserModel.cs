using Newtonsoft.Json;
using System.Collections.Generic;

namespace UsersApi.Models
{
    public class UsersJson
    {
        [JsonProperty("results")]
        public List<UserModel> Users { get; set; }
    }

    public class UserModel
    {
        public string Gender { get; set; }
        public dynamic Name { get; set; }

        public string FullName => string.Join(" ", this.Name.first, this.Name.last);
        public dynamic Login { get; set; }
        public dynamic Location { get; set; }
        public string Email { get; set; }
    }
}
