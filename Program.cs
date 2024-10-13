using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicStore.Models;

namespace MusicStoeCh6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("MusicContext") ?? 
                throw new InvalidOperationException("Connection string 'Music Context' not found.");

            builder.Services.AddDbContext<MusicContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MusicContext")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            app.MapControllerRoute(
                name: "sort_album",
                //can sort by artist or genre
                //sort=0 not sorted, sort=1 asc, sort=2 desc
                pattern: "{controller=Album}/{action=Index}/{ArtistSort?}/{GenreSort?}");


            app.MapControllerRoute(
                name: "sort_artist",
                //id=0 not sorted, id=1 sorted asc, id=2 sort desc
                pattern: "{controller=Artist}/{action=Index}/{ArtistSort?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}