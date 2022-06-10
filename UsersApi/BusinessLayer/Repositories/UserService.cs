using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApi.DataAccessLayer;
using UsersApi.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace UsersApi.BusinessLayer
{
    public class UserService: IUserService
    {
        #region Property
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _dbContextScopeFactory;

        #endregion

        #region Constructor
        public UserService(IServiceScopeFactory dbContextScopeFactory, IMapper mapper)
        {
            //_context = context;
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;

        }
        #endregion

        #region Get Users by country
        /// <summary>
        /// Get Users by country
        /// </summary>
        /// <returns></returns>
        public async Task<UsersResultModel> GetUsersByCountry(string country)
        {
            try
            {
                using (var scope = _dbContextScopeFactory.CreateScope())
                {
                    ApiContext _context = scope.ServiceProvider.GetRequiredService<ApiContext>();
                    List<User> users;
                    if (!string.IsNullOrEmpty(country))
                    {
                        users = _context.Users.Select(u => u).ToList(); //await...ToListAsync..?
                    }
                    else
                    {
                        users = _context.Users.ToList();//await...ToListAsync..?
                    }
                    return BuildUsersByCountry(users);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private UsersResultModel BuildUsersByCountry(List<User> users)
        {
            UsersResultModel result = new UsersResultModel();
            foreach (User user in users)
            {
                var country = result.Countries.Find(u => u.Name == user.Country);
                if (country == null)
                {
                    country = new CountryUsersModel();
                    result.Countries.Add(country);
                }
                country.Users.Add(user);
            }
            return result;
        }

        #region Get Users from api
        public async Task<List<User>> GetUsersFromApi()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://randomuser.me/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            const int noOfUsers = 25;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api?results=" + noOfUsers);
            var responseTask = await client.SendAsync(request);

            string response = await responseTask.Content.ReadAsStringAsync();
            var usersResp = JsonConvert.DeserializeObject<UsersJsonModel>(response);

            return usersResp.Users.Select(u=> _mapper.Map<User>(u)).ToList();
        }
        #endregion

        #region import users from api into db
        public async void ImportUsers()
        {
            using (var scope = _dbContextScopeFactory.CreateScope())
            {
                try
                {
                    ApiContext _context = scope.ServiceProvider.GetRequiredService<ApiContext>();
                    List<User> users = await GetUsersFromApi();
                    var newUsers = await GetUsersNotExistsInDb(_context.Users, users);
                    _context.Users.AddRange(newUsers);
                    _context.SaveChanges();
                    
                    //await _context.BulkInsertAsync(newUsers);
                    //Relational-specific methods can only be used when the context is using a relational database provider.
                    //InMemory is not intended to be a relational database.
                    //If you're using an InMemory database, you'll want to skip running migrations

                    //in EF 6 can do this; EF 6 is not supported in VS 2019 windows
                    //_context.BulkInsert(users, options => {
                    //    options.InsertIfNotExists = true;
                    //    options.ColumnPrimaryKeyExpression = c => c.Id;
                    //});
                }
                catch (Exception ex)
                {
                }
            }
        }

        private async Task<List<User>> GetUsersNotExistsInDb(DbSet<User> dbUsers, List<User> users)
        {
            List<User> newUsers = new List<User>();//users.Where(u => _context.Users.FindAsync(u.Id) == null).ToList();

            var userIds = users.Select(c => c.Id);
            var usersInDb = await dbUsers
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync(); // single DB query
            foreach (var user in users)
            {
                var userInDb = usersInDb
                    .SingleOrDefault(u => u.Id == user.Id); // runs in memory
                if (userInDb == null)
                    newUsers.Add(user);
                /*
                _context.Users.ApplyCurrentValues(country);
                _context.Users.AddObject(country);
                */
            }
            return newUsers;
        }
        #endregion



        #region Create User
        /// <summary>
        /// Create user to Db
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<string> CreateUserAsync(User user)//userDTO - in this case is the same with User
        {
            try
            {
                var userExists = _context.Users.Where(c => c.Email.Equals(user.Email)).Any();//await .. AnyAsync
                if (userExists is false)
                {
                    //Ret = Mapper.Map<UserDTO, User>(b);
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return "User Created Success";
                }
                else return "Email already exists";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
