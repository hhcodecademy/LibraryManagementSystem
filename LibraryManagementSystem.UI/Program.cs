using LibraryManagementSystem.DAL.Data;
using LibraryManagementSystem.DAL.Repository.Interfaces;
using LibraryManagementSystem.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace LibraryManagementSystem.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var toastrOptions = new ToastrOptions()
            {
                ProgressBar = false,
                PositionClass = ToastPositions.TopRight,
                ShowDuration= 500
            };

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddNToastNotifyToastr(toastrOptions);
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddDbContext<LibraryDbContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
            });
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseNToastNotify();

            app.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
