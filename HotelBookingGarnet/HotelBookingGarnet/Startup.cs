using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;

namespace HotelBookingGarnet
{
    public class Startup
    {
        private IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production") { 
                services.AddDbContext<ApplicationContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection")));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(builder =>
                builder.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            }
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IHotelService, HotelService>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IBedService, BedService>();
            services.AddTransient<IRoomBedService, RoomBedService>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            });

            services.Configure<IdentityOptions>(options => { options.Password.RequireNonAlphanumeric = false; });
            
            services.AddMvc();
            services.AddPaging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<User> userManager)
        {
            Administrator.CreateAdmin(userManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}