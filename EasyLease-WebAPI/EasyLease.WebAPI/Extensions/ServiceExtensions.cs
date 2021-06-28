using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.LoggerService;
using EasyLease.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyLease.WebAPI.Extensions {
    public static class ServiceExtensions {
        public static void ConfigureCors(this IServiceCollection service) {
            service.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services) {
            services.Configure<IISOptions>(options => { });
        }

        public static void ConfigureLoggerService(this IServiceCollection services) {
            services.AddScoped<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<RepositoryContext>(opts => {
                opts.EnableDetailedErrors();
                opts.UseNpgsql(configuration.GetConnectionString("sqlConnection"),
                b => b.MigrationsAssembly("EasyLease.WebAPI"));
            });
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
        public static void ConfigureFileStorage(this IServiceCollection services, IConfiguration configuration) {
            FileStorageSettings fileStorageSettings = new FileStorageSettings();
            configuration.GetSection("FileStorageSettings").Bind(fileStorageSettings);

            services.AddSingleton<FileStorageSettings>(fileStorageSettings);
        }
    }
}