using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsersApi.BusinessLayer;

namespace UsersApi.Tasks
{
    public class  JobTask: IHostedService, IDisposable
    {
        private Timer _timer = null;
        private IUserService _userService;


        public JobTask(IUserService userService)
        {
            _userService = userService;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                _userService.ImportUsers();
            }
            catch(Exception e)
            {
                //In case of an unsuccessful synchronization attempt, the Connector should return
                //data from the last successful synchronization
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
