using Newtonsoft.Json;
using UsersApi.Models;
using System.Net.Http.Headers;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace UsersApi.Tasks
{
    public class JobService : IHostedService, IDisposable
    {
        private Timer _timer = null;
        private readonly IServiceScopeFactory _scopeFactory;

        public JobService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        async Task<List<UserModel>> RequestToApi()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://randomuser.me/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            const int noOfUsers = 25;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api?results="+noOfUsers);
            var responseTask = await client.SendAsync(request);

            string response = await responseTask.Content.ReadAsStringAsync();
            //response = response.Substring(response.IndexOf("[") );
            //response = response.Substring(0, response.IndexOf("]")+1);

            //var users = new JavaScriptSerializer().Deserialize<List<UserModel>>(response);
            var users = JsonConvert.DeserializeObject<UsersJson>(response);
            return users.Users;
        }
        private async void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                try
                {
                    Context _context = scope.ServiceProvider.GetRequiredService<Context>();
                    List<UserModel> users = await RequestToApi();

                    users.ForEach(async userModel =>
                    {
                        string countryName = userModel.Location.country;
                        string username = userModel.Login.username;

                        var country = await _context.Countries.FindAsync(countryName);
                        var user = await _context.Users.FindAsync(username);
                        if (user != null)
                        {
                            user = _context.Users.Find(username);
                        }
                        else
                        {
                            user = new User(userModel.FullName, userModel.Gender, userModel.Email);
                            _context.Users.Add(user);
                            //_context.SaveChanges();
                            //user = _context.Users.Find(user.Id);
                        }
                        if (country != null)
                        {
                            country.Users.Add(user);
                        }
                        else
                        {
                            country = new Country((string)userModel.Location.country);
                            //user = _context.Users.Find(user.Id);
                            country.Users.Add(user);
                            _context.Countries.Add(country);
                        }
                        _context.SaveChanges();
                    });
                }
                catch(Exception ex)
                {

                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
