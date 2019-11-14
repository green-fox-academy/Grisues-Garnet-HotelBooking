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
using Newtonsoft.Json;

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
                services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        options.ClientId = Configuration.GetConnectionString("GoogleClientId");
                        options.ClientSecret = Configuration.GetConnectionString("GoogleClientSecret");
                    })
                    .AddFacebook(options =>
                    {
                        options.AppId = Configuration.GetConnectionString("FacebookAppClient");
                        options.AppSecret = Configuration.GetConnectionString("FacebookAppSecret");
                    });
            }
            else
            {
                services.AddDbContext<ApplicationContext>(builder =>
                    builder.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
                services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        IConfigurationSection googleAuthNSection =
                            Configuration.GetSection("Authentication:Google");

                        options.ClientId = googleAuthNSection["ClientId"];
                        options.ClientSecret = googleAuthNSection["ClientSecret"];
                    })
                    .AddFacebook(options =>
                    {
                        IConfigurationSection googleAuthNSection =
                            Configuration.GetSection("Authentication:Facebook");

                        options.AppId = googleAuthNSection["AppId"];
                        options.AppSecret = googleAuthNSection["AppSecret"];
                    });
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
            services.AddTransient<ITaxiReservationService, TaxiReservationService>();
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

            services.SetUpAutoMapper();
            services.AddMvc();
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
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
            else {
                app.UseHsts();
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