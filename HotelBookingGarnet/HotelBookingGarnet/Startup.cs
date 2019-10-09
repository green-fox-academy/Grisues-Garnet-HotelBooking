using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;
using System;
using HotelBookingGarnet.Services.Helpers.AutoMapper;


namespace HotelBookingGarnet
{
    public class Startup
    {
        private IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("ProductionConnection")));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(builder =>
                    builder.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            }

            services.BuildServiceProvider().GetService<ApplicationContext>().Database.Migrate();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IHotelService, HotelService>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<IBlobService, BlobService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IBedService, BedService>();
            services.AddTransient<IRoomBedService, RoomBedService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IHotelPropertyTypeService, HotelPropertyTypeService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.Configure<IdentityOptions>(options => { options.Password.RequireNonAlphanumeric = false; });
            services.SetUpAutoMapper();
            services.AddMvc();
            services.AddPaging();
        }

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