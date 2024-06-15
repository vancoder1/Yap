using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yap.Data;
using Yap.Hubs;
using Yap.Models;
using Yap.Services;

namespace Yap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            var chatDbConnectionString = builder.Configuration.GetConnectionString("ChatDbConnection") ?? throw new InvalidOperationException("Connection string 'ChatDbConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(defaultConnectionString));
            builder.Services.AddDbContext<ChatDbContext>(options =>
                options.UseSqlServer(chatDbConnectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            // Add SignalR service
            builder.Services.AddSignalR();

            // Add custom services
            builder.Services.AddScoped<ChatService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Chat}/{action=Index}/{id?}");
            app.MapRazorPages();

            // Map SignalR hubs
            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}
