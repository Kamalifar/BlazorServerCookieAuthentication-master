using BlazorServerTestDynamicAccess.Data;
using BlazorServerTestDynamicAccess.Infralayer;
using BlazorServerTestDynamicAccess.Services;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using BlazorServerTestDynamicAccess.Models.Mappings;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorServerTestDynamicAccess
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();


            #region Authentication
            //for use SecurityTrimming
            services.AddDNTCommonWeb();
            //Authentication
            services.AddScoped<IUnitOfWork, ApplicationDbContext>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ICookieValidatorService, CookieValidatorService>();
            services.AddScoped<IDbInitializerService, DbInitializerService>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<IUserInfoService, UserInfoService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    serverDbContextOptionsBuilder =>
                    {
                        var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                        serverDbContextOptionsBuilder.CommandTimeout(minutes);
                        serverDbContextOptionsBuilder.EnableRetryOnFailure();
                    });
            });
            //add policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy(CustomRoles.Admin, policy => policy.RequireRole(CustomRoles.Admin));
                options.AddPolicy(CustomRoles.User, policy => policy.RequireRole(CustomRoles.User));
            });
            // Needed for cookie auth.
            services
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.SlidingExpiration = false;
                    options.LoginPath = "/";
                    options.LogoutPath = "/";
                    //options.AccessDeniedPath = new PathString("/Home/Forbidden/");
                    options.Cookie.Name = ".my.app1.cookie";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnValidatePrincipal = context =>
                        {
                            var cookieValidatorService = context.HttpContext.RequestServices.GetRequiredService<ICookieValidatorService>();
                            return cookieValidatorService.ValidateAsync(context);
                        }
                    };
                });
            #endregion
            //AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddScoped<IHotelRoomService, HotelRoomService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            #region authentication

            app.UseAuthentication();
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializerService>();
                dbInitializer.Initialize();
                dbInitializer.SeedData();
            }
            #endregion

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
