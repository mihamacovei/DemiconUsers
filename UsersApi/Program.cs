using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UsersApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            //in .net 6

            ///var builder = WebApplication.CreateBuilder(args);

            ////DbAccessors
            //builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //builder.Services.AddMemoryCache();
            //builder.Services.AddDbContext<Context>(option => option.UseInMemoryDatabase("Country"));

            //builder.Services.AddControllers();
            ////Singlteton JobTask
            //builder.Services.AddHostedService<TimedHostedService>();

            ////builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddControllersWithViews();

            //builder.Services.AddMvc();
            //var app = builder.Build();

            //app.UseAuthorization();

            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //            Path.Combine(Directory.GetCurrentDirectory(), @"Assets")),
            //    RequestPath = new PathString("/Assets")
            //});
            //app.MapControllers();

            //app.UseRouting();


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
            //app.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
