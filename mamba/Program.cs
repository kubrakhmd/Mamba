using mamba.DAL;
using mamba.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.EntityFrameworkCore;

namespace mamba
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt=>opt
            .UseSqlServer(builder.Configuration
            .GetConnectionString("Default")));
            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
               opt.Password.RequiredLength = 8;
              opt.Password.RequireNonAlphanumeric = false;

            opt.User.RequireUniqueEmail = true;
             
                opt.Lockout.AllowedForNewUsers = true;
               opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            var app = builder.Build();
            app.UseStaticFiles();
              app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute("default", "{area:exists}/{controller=home}/{action=index}/{id?}");
            app.MapControllerRoute("default","{controller=home}/{action=index}/{id?}");
            app.Run();
        }
    }
}
