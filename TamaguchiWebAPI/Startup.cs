using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TamaguchiBL.Models;
using Microsoft.EntityFrameworkCore;

namespace TamaguchiWebAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container!
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Add Session support
            //The following two commands set the Session state to work!
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            #endregion
            #region Add DB
            string connectionString = Configuration.GetConnectionString("Tamaguchi");
            services.AddDbContext<TamaguchiContext>(options =>
            options.UseLazyLoadingProxies()
            .UseSqlServer(connectionString));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Development and Https redirection support
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            #endregion


            app.UseRouting();

            app.UseAuthorization();

            #region Session support
            //Tells the application to use Session!
            app.UseSession();
            #endregion

            app.UseEndpoints(endpoints =>
            {
                //Map all routings for controllers
                endpoints.MapControllers();
            });

        }
    }
}
