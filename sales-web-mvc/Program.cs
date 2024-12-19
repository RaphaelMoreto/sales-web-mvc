using Microsoft.EntityFrameworkCore;
using sales_web_mvc.Data;

namespace sales_web_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<sales_web_mvcContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("sales_web_mvcContext"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("sales_web_mvcContext")),
                    mysqlOptions => mysqlOptions.MigrationsAssembly("sales-web-mvc")
                ));

            builder.Services.AddScoped<SeedingService>();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
