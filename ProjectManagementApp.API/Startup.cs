using Microsoft.AspNetCore.Builder;
using System.Reflection;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using Infrastructure.Data;
using Application.Services;
using Application.Services.Interfaces;
using Application.Extensions;
using Application.Configurations;
using AutoMapper;
using ProjectManagementApp.Api.ViewModels;
using Application.DTOs;
using ProjectManagementApp.Api.ViewModels.Profiles;
using Microsoft.AspNetCore.Mvc.Authorization;
//using Infrastructure.Identity;


namespace ProjectManagementApp.Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddJsonOptions(x =>
                            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddApplicationLayer();
            //services.AddJWTIdentityService();
            //services.AddCookieIdentity();

            services.AddAutoMapper(Assembly.GetExecutingAssembly()); //automapper.extension
            services.AddAutoMapper(typeof(MapperInitializer));


            //services.AddControllersWithViews(o =>
            //    o.Filters.Add(new AuthorizeFilter())); //global authorization for all controllers
            //  ** Will need to individually [AllowAnonymous] on the controller to login

            //services.AddAuthentication(CookieAuthenticationDefaults.Authenticaion
            //    .AddCookie(o =>
            //    {
            //        o.Events.OnRedirectToLogin = (ContextBoundObject) =>
            //        {
            //            Context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //            return Task.CompletedTask;
            //        };
            //    });

      //      services.AddMvc(opt => opt.EnableEndpointRouting = false); //may not need in updated versions?
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

            //app.UseMiddleware<ApiKeyMiddleware>();
            //app.MapGet("/customers",
            //    [Authorize] (ICustomerService service) => service.GetAllCustomersAsync());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(cfg =>
            {
                cfg.MapControllers();
            });
        }
    }
}
