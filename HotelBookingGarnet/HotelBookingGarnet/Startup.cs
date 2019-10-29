using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Globalization;
using HotelBookingGarnet.Services.Helpers.AutoMapper;
using Microsoft.AspNetCore.Http;

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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

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
            services.AddTransient<IGuestService, GuestService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            });
            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
            services.AddMvc()
                .AddViewLocalization(options => { options.ResourcesPath = "Resources"; })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "896619303427-qeleqnubha96r1dfpo4fc21bvkjeo80h.apps.googleusercontent.com";
                    options.ClientSecret = "aYYDQvMAmQFsptc2vpiHl56t";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "1020229731641843";
                    options.AppSecret = "055a9f70199974c65374ce5a15e9f1ca";
                });
                
            services.SetUpAutoMapper();
            services.AddMvc();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-GB"),
                    new CultureInfo("hu-HU")
                };
                options.DefaultRequestCulture = new RequestCulture("en-GB");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<User> userManager)
        {
            Administrator.CreateAdmin(userManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}