using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UsersApi.BusinessLayer;

namespace UsersApi.Models
{
    public class UsersJsonModel
    {
        [JsonProperty("results")]
        public List<ApiUserModel> Users { get; set; }
    }
    public class UsersResultModel
    {
        [JsonProperty("countries")]
        public List<CountryUsersModel> Countries { get; set; }

        public UsersResultModel()
        {
            Countries = new List<CountryUsersModel>();
        }
    }
}
