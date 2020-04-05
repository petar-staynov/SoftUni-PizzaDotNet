namespace PizzaDotNet.Web
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using PizzaDotNet.Common;
    using PizzaDotNet.Common.Mapping;
    using PizzaDotNet.Data;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Repositories;
    using PizzaDotNet.Data.Seeding;
    using PizzaDotNet.Services.Messaging;
    using PizzaDotNet.Web.Infrastructure.Middlewares.Auth;
    using PizzaDotNet.Web.ViewModels.TodoItems;

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
            // Framework services
            // TODO: Add pooling when this bug is fixed: https://github.com/aspnet/EntityFrameworkCore/issues/9741
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtTokenValidation:Secret"]));

            services.Configure<TokenProviderOptions>(opts =>
            {
                opts.Audience = this.configuration["JwtTokenValidation:Audience"];
                opts.Issuer = this.configuration["JwtTokenValidation:Issuer"];
                opts.Path = "/api/account/login";
                opts.Expiration = TimeSpan.FromDays(15);
                opts.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services
                .AddAuthentication()
                .AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,
                        ValidateIssuer = true,
                        ValidIssuer = this.configuration["JwtTokenValidation:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = this.configuration["JwtTokenValidation:Audience"],
                        ValidateLifetime = true,
                    };
                });

            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<ApplicationUserStore>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton(this.configuration);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISmsSender, NullMessageSender>();

            // Identity stores
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            AutoMapperConfig.RegisterMappings(typeof(TodoItemViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                ApplicationDbContextSeeder.Seed(dbContext, serviceScope.ServiceProvider);
            }

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler(application =>
                {
                    application.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = GlobalConstants.JsonContentType;

                        var ex = context.Features.Get<IExceptionHandlerFeature>();
                        if (ex != null)
                        {
                            await context.Response
                                .WriteAsync(JsonConvert.SerializeObject(new { ex.Error?.Message, ex.Error?.StackTrace }))
                                .ConfigureAwait(continueOnCapturedContext: false);
                        }
                    });
                });
            }

            // POSSIBLE ISSUES HERE
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            // ###
            
            app.UseFileServer();

            app.UseJwtBearerTokens(
                app.ApplicationServices.GetRequiredService<IOptions<TokenProviderOptions>>(),
                PrincipalResolver);

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    Console.WriteLine("dev spa build");
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
                else
                {
                    Console.WriteLine("prod spa build");
                    spa.UseReactDevelopmentServer(npmScript: "build");
                }
            });
        }

        private static async Task<GenericPrincipal> PrincipalResolver(HttpContext context)
        {
            var email = context.Request.Form["email"];

            var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByEmailAsync(email);
            if (user == null || user.IsDeleted)
            {
                return null;
            }

            var password = context.Request.Form["password"];

            var isValidPassword = await userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword)
            {
                return null;
            }

            var roles = await userManager.GetRolesAsync(user);

            var identity = new GenericIdentity(email, "Token");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            return new GenericPrincipal(identity, roles.ToArray());
        }
    }
}
