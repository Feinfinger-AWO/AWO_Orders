using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Connections;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWO_Orders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SqlConnectionStringBuilder builder =
                    new SqlConnectionStringBuilder(Configuration.GetConnectionString("LocationContext"));
            builder.Password = "admin";
            builder.UserID = "awo";
            builder.Authentication = SqlAuthenticationMethod.SqlPassword;

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddHttpContextAccessor();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddDbContext<LocationContext>(options =>
            {
                options.UseSqlServer(builder.ConnectionString);
            });

            services.AddDefaultIdentity<IdentityUser>(
                options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LocationContext>();

            services.AddDbContext<EmployeeContext>(options =>
                    options.UseSqlServer(builder.ConnectionString));

            services.AddDbContext<RightContext>(options =>
                    options.UseSqlServer(builder.ConnectionString));

            services.AddDbContext<ArticleTypeContext>(options =>
                    options.UseSqlServer(builder.ConnectionString));

            Startup.ConnectionString = builder.ConnectionString;
           
            services.AddDbContext<OrderStatusContext>(options =>
                    options.UseSqlServer(builder.ConnectionString));

            services.AddDbContext<OrdersContext>(options =>
                    options.UseSqlServer(builder.ConnectionString));

            services.AddDbContext<OrderLogEntriesContext>(options =>
                    options.UseSqlServer(builder.ConnectionString));

            services.AddDbContext<OrderPositionContext>(options =>
                    options.UseSqlServer(builder.ConnectionString));

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                 {
                    new CultureInfo("en"),
                    new CultureInfo("de"),
                    new CultureInfo("en-GB")
                };
                options.DefaultRequestCulture = new RequestCulture("de");
                options.DefaultRequestCulture.Culture.NumberFormat.NumberDecimalSeparator = ",";
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddDbContext<VOrderStatusContext>(options =>
                    options.UseSqlServer(builder.ConnectionString));

            services.AddDbContext<VOrderStatusEmplContext>(options =>
                    options.UseSqlServer(builder.ConnectionString));

            services.AddDbContext<OpenOrdersContext>(options =>
                    options.UseSqlServer(builder.ConnectionString));
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);

            app.UseAuthorization();
            
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        public static string ConnectionString { get; set; }
    }
}
