using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NazariTest.Application.Interfaces;
using NazariTest.Persistence.AutoFac;
using NazariTest.Persistence.Context;
using NazariTest.Persistence.Data;

namespace NazariTest.Wep
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("NazariDb"));
                options.EnableSensitiveDataLogging();
            });


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddAutoMapper(Assembly.Load("NazariTest.Application"));
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.AddAutofacDependencyServices();

            });
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
                name: "default",
                pattern: "{controller=FinancialYear}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
