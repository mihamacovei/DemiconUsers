using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApi.Models;

namespace UsersApi.BusinessLayer
{
    public interface IUserService
    {
        Task<UsersResultModel> GetUsersByCountry(string country);//should be userModel
        void ImportUsers();
    }
}
