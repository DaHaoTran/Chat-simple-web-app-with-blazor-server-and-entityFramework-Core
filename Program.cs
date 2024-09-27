using ChatSimple.Context;
using ChatSimple.Data;
using ChatSimple.Hub;
using ChatSimple.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

namespace ChatSimple
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSession();

            builder.Services.AddDbContext<TestContext>(o =>
            {
                o.UseSqlServer(builder.Configuration.GetConnectionString("Defaultconnection"));
            });

            builder.Services.AddSignalR();
            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            builder.Services.AddScoped<ITestSvc, TestSvc>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapHub<BroadcastHub>("/broadcastHub");
            app.UseResponseCompression();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.UseSession();

            app.Run();
        }
    }
}