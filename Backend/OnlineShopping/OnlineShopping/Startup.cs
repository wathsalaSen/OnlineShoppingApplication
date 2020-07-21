using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OnlineShopping.Business;
using OnlineShopping.Common.Interfaces;
using OnlineShopping.Common.Models;
using OnlineShopping.Data;

namespace OnlineShopping
{
    public class Startup
    {
        private readonly IHostEnvironment _currentEnvironment;
        private readonly string AllowSpecificOrigins = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            _currentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);

            services.AddScoped(typeof(ILoginBusiness), typeof(LoginBusiness));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddCors(cors =>
            {
                cors.AddPolicy(name: AllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("https://localhost:4200", "http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddControllers();
            //services.AddDbContextPool<OnlineShoppingContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("OnlineShoppingDBConnection")));
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<OnlineShoppingContext>();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettingsModel>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettingsModel>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            if (_currentEnvironment.IsEnvironment("Test"))
            {
                return;
            }

            //if (_currentEnvironment.IsDevelopment() && Configuration.GetValue<bool>("UseInMemoryDataBase"))
            //{
            //    services.AddDbContext<OnlineShoppingContext>(c => c.UseInMemoryDatabase("OnlineShoppingDBConnection"));
            //}
            //else
            //{
            //    services.AddDbContext<OnlineShoppingContext>(c =>
            //       c.UseSqlServer(Configuration.GetConnectionString("OnlineShoppingDBConnection")));
            //}
        }
    }
}
