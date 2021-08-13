using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.Configuration;
using EasyLease.Entities.Models;
using EasyLease.LoggerService;
using EasyLease.Repository;
using EasyLease.WebAPI.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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

            FileStorageManager fileStorageManager = new FileStorageManager(fileStorageSettings);
            services.AddSingleton<IFileStorageManager>(fileStorageManager);
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

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration) {
            UserProfileSettings userProfileSettings = new UserProfileSettings();
            configuration.GetSection("UserProfileSettings").Bind(userProfileSettings);

            var builder = services.AddIdentityCore<User>(options => {
                // Password settings.
                options.Password.RequireDigit = userProfileSettings.RequireDigitForPassword;
                options.Password.RequireLowercase = userProfileSettings.RequireLowercaseForPassword;
                options.Password.RequireUppercase = userProfileSettings.RequireUppercaseForPassword;
                options.Password.RequireNonAlphanumeric = userProfileSettings.RequireNonAlphanumericPassword;
                options.Password.RequiredLength = userProfileSettings.RequiredLengthForPassword;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(userProfileSettings.DefaultLockoutTimeSpanForFailedAccess);
                options.Lockout.MaxFailedAccessAttempts = userProfileSettings.MaxFailedAccessForSignIn;
                options.Lockout.AllowedForNewUsers = userProfileSettings.AllowedLockoutForNewUsers;

                // User settings.
                options.User.AllowedUserNameCharacters = userProfileSettings.AllowedUserNameCharacters;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<RepositoryContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration) {
            JwtSettings jwtSettings = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(jwtSettings);

            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
    }
}