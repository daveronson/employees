using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeDirectory
{
    using AutoMapper;
    using Filters;
    using HtmlTags;
    using Infrastructure;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Services;
    using System.IO;
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        private readonly IHostingEnvironment hostingEnvironment;
        public Startup(IHostingEnvironment env)
        {
            hostingEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        //public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(opt => opt.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddTransient<IPermissionAuthorizer, DbContextPermissionAuthorizer>();
            
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(DbContextTransactionFilter));
            });

            var employeeDirectoryConnectionString = "Data Source=" + Path.Combine(hostingEnvironment.ContentRootPath, "employeeDirectory.db");

            services.AddDbContext<DirectoryContext>(builder =>
                    builder.UseSqlite(employeeDirectoryConnectionString));

            //services.AddScoped(_ => new DirectoryContext(Configuration["Data:DefaultConnection:ConnectionString"]));
            
            services.AddAutoMapper();

            services.AddHtmlTags(reg =>
            {
                reg.Editors.Always.AddClass("form-control");
                reg.Editors.BuilderPolicy<EnumDropDownBuilder>();

                reg.Labels.Always.AddClass("control-label");
                reg.Labels.Always.AddClass("col-md-2");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = new PathString("/account/login"),
                AccessDeniedPath = new PathString("/account/forbidden")
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
