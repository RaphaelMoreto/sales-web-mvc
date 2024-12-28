using Microsoft.EntityFrameworkCore;
using sales_web_mvc.Data;
using sales_web_mvc.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace sales_web_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<sales_web_mvcContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("sales_web_mvcContext"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("sales_web_mvcContext")),
                    mysqlOptions => mysqlOptions.MigrationsAssembly("sales-web-mvc")
                ));

            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<SellerService>();
            builder.Services.AddScoped<DepartmentService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //CRIA UM ESPAÇO TEMPORÁRIO DENTRO DO SISTEMA DE GERENCIAMENTO DE SERVIÇOS DO ASP.NET CORE, CONHECIDO COMO "Dependecy Injection (DI)"
            using (var scope = app.Services.CreateScope()) //ESCOPO SENDO CRIADO
            {
                var seedingServices = scope.ServiceProvider.GetRequiredService<SeedingService>();
                seedingServices.Seed();
            }

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

            app.UseRequestLocalization(localizationOptions);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
