namespace PizzaDotNet.Web
{
    using System;
    using System.Reflection;

    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using PizzaDotNet.Data;
    using PizzaDotNet.Data.Common;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Repositories;
    using PizzaDotNet.Data.Seeding;
    using PizzaDotNet.Services;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Services.Messaging;
    using PizzaDotNet.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection"));
                });

            // Cache configuration
            services.AddDistributedMemoryCache();

            // Identity configuration
            services
                .AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Third-party authentication configuration
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = this.configuration.GetValue<string>("FacebookAppId");
                facebookOptions.AppSecret = this.configuration.GetValue<string>("FacebookAppSecret");
                facebookOptions.AccessDeniedPath = "/Home/LoginFailed";
            });
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = this.configuration.GetValue<string>("GoogleAppId");
                googleOptions.ClientSecret = this.configuration.GetValue<string>("GoogleAppSecret");
                googleOptions.AccessDeniedPath = "/Home/LoginFailed";
            });

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                })
                .AddSessionStateTempDataProvider();

            // Session configuration
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = new TimeSpan(0, 24, 0, 0);
            });

            // TempData configuration
            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
            });

            //Antiforgery token configuration
            services.AddAntiforgery(options =>
            {
                options.HeaderName = this.configuration.GetValue<string>("AntiforgeryHeader");
            });

            // Auto Mapper Configurations
            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));

            services.AddRazorPages()
                .AddSessionStateTempDataProvider();;

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x =>
                new SendGridEmailSender(this.configuration.GetValue<string>("SendGridKey")));
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IRatingsService, RatingsService>();
            services.AddTransient<IProductSizeService, ProductSizeService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IAddressesService, AddressesService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();

            // Google Cloud service
            services.AddSingleton<IGoogleCloudStorage, GoogleCloudStorage>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter()
                    .GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    // endpoints.MapControllerRoute("categories", "{controller=Categories}/{name}",
                    //     new { controller = "Categories", action = "Details"});
                    endpoints.MapRazorPages();
                });
        }
    }
}
