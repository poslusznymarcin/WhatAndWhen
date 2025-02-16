using Microsoft.EntityFrameworkCore;
using System.Text;
using WhatAndWhen.Services;
using WhatAndWhenData;
using WhatAndWhen.Middleware;
using Microsoft.AspNetCore.Identity;


namespace WhatAndWhen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<WhatAndWhenContext>(); // Scoped by default
            



            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WhatAndWhenContext>();

            // Dodanie innych serwisów
            builder.Services.AddTransient<ITaskService, TaskService>();

            // Dodanie kontrolerów z widokami i RazorPages
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Dodanie Session i MemoryCache
            builder.Services.AddSession();
            builder.Services.AddMemoryCache(); 


            var app = builder.Build();

            // Konfiguracja potoku HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();


            app.UseAuthorization();
            app.UseMiddleware<LastVisitMiddleware>();
            app.MapStaticAssets();
            app.MapRazorPages();

            app.Use(async (context, next) =>
            {

                var requestContent = new StringBuilder();
                requestContent.AppendLine("=== Request Info ===");
                requestContent.AppendLine($"method = {context.Request.Method.ToUpper()}");
                requestContent.AppendLine($"path = {context.Request.Path}");
                requestContent.AppendLine("-- headers");
                foreach (var (headerkey, headerValue) in context.Request.Headers)
                {

                    requestContent.AppendLine($"header = {headerkey} value = {headerValue}");
                }

                requestContent.AppendLine("-- body");
                context.Request.EnableBuffering();
                var requestReader = new StreamReader(context.Request.Body);
                var content = await requestReader.ReadToEndAsync();
                requestContent.AppendLine($"body = {content}");
                Console.Write(requestContent.ToString());
                context.Request.Body.Position = 0;
                await next();
            });

            app.UseHttpsRedirection();
            app.UseRouting();






            app.Run();
        }
    }
}
