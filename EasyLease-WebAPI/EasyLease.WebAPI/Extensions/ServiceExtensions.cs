using System.Collections.Generic;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.Configuration;
using EasyLease.LoggerService;
using EasyLease.Repository;
using EasyLease.WebAPI.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        public static void ConfigureFileStorage(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
            FileStorageSettings fileStorageSettings = new FileStorageSettings() { WebRootPath = env.WebRootPath };
            configuration.GetSection("FileStorageSettings").Bind(fileStorageSettings);

            fileStorageSettings.FileSignature = FileSignatureConfiguration.GetSignaturesFromExtensions(fileStorageSettings.AllowedExtensions);

            services.AddSingleton<FileStorageSettings>(fileStorageSettings);

            FileStorage fileStorage = new FileStorage(fileStorageSettings);
            services.AddSingleton<FileStorage>(fileStorage);
        }

        public static void ConfigureUserProfile(this IServiceCollection services, IConfiguration configuration) {
            UserProfileSettings userProfileSettings = new UserProfileSettings();
            configuration.GetSection("UserProfileSettings").Bind(userProfileSettings);

            userProfileSettings.FileSignature = FileSignatureConfiguration.GetSignaturesFromExtensions(userProfileSettings.AllowedExtensions);

            services.AddSingleton<UserProfileSettings>(userProfileSettings);
        }

        public static void ConfigureGeneralSettings(this IServiceCollection services, IConfiguration configuration) {
            GeneralSettings generalSettings = new GeneralSettings();
            configuration.GetSection("GeneralSettings").Bind(generalSettings);

            services.AddSingleton<GeneralSettings>(generalSettings);
        }

        public static void ConfigureAutoMapperProfile(this IServiceCollection services) {
            services.AddSingleton<IMapper>(provider => new MapperConfiguration(cfg =>
                cfg.AddProfile(new MappingProfile(provider.GetService<GeneralSettings>()))).CreateMapper());
        }
    }
}