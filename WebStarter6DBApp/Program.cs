using Serilog;
using Serilog.Events;
using WebStarter6DBApp.Configuration;
using WebStarter6DBApp.DAO;
using WebStarter6DBApp.Services;

namespace WebStarter6DBApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Adds a new instance for every request (no singleton) to the IOC container
            builder.Services.AddScoped<IStudentDAO, StudentDAOImpl>();
            builder.Services.AddScoped<IStudentService, StudentServiceImpl>();

            // Add AutoMapper to IOC container. Now it can be injected.
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            // Config Logger
            builder.Host.UseSerilog((context, config) =>
            {
                // Read from configuration file
                config.ReadFrom.Configuration(context.Configuration);

                /* 
                 * The following are equivalent to what the "Serilog" entry in appsettings.json does:
                config
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()                    // Makes available the additional info, it gives an API with LogContext which allows us to push property
                //.Enrich.WithAspNetCore()                    // Automatically saves information to logs, e.g. for IP addresses
                .WriteTo.Console()                          // Output
                .WriteTo.File(
                    "Logs/logs.txt",                        // Path is relative to project
                    rollingInterval: RollingInterval.Day,   // When to change log file
                    outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss:fff zzz} {SourceContext} [{Debug}]" +
                            "{Message}{NewLine}{Exception}",
                    retainedFileCountLimit: null,           // No limit in the daily file creation
                    fileSizeLimitBytes: null                // No limit to the size of each log file
                );
                */
            });

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

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
