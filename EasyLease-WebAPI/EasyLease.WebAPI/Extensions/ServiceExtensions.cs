using System;
using System.Reflection;
using System.Text;
using AutoMapper;
using EasyLease.Contracts;
using EasyLease.Entities;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.Entities.Configuration;
using EasyLease.Entities.Models;
using EasyLease.LoggerService;
using EasyLease.Repository;
using EasyLease.WebAPI.Services;
using EasyLease.WebAPI.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace EasyLease.WebAPI.Extensions {
    public static class ServiceExtensions {
        private static GeneralSettings _generalSettings;

        public static void ConfigureGeneralSettings(this IServiceCollection services, IConfiguration configuration) {
            var generalSection = configuration.GetSection(GeneralSettings.General);

            var originUIUrl = generalSection.GetValue<string>("OriginUIUrl", string.Empty);
            var hoursOffsetForUkraine = generalSection.GetValue<short>("HoursOffsetForUkraine", 3);
            var pageSize = generalSection.GetValue<short>("PageSize", 10);
            var maxPageSize = generalSection.GetValue<short>("MaxPageSize", 50);

            _generalSettings = new GeneralSettings(originUIUrl, hoursOffsetForUkraine, pageSize, maxPageSize);

            services.AddSingleton<GeneralSettings>(_generalSettings);
        }

        public static void ConfigureCors(this IServiceCollection service) {
            service.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder =>
                builder
                .WithOrigins(_generalSettings.OriginUIUrl)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination", "Authorization"));
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
            FileStorageSettings fileStorageSettings = configuration.GetSection(FileStorageSettings.FileStorage).Get<FileStorageSettings>();

            fileStorageSettings.WebRootPath = env.WebRootPath;
            fileStorageSettings.FileSignature = FileSignatureConfiguration.GetSignaturesFromExtensions(fileStorageSettings.AllowedExtensions);

            services.AddSingleton<FileStorageSettings>(fileStorageSettings);
            services.AddSingleton<IFileStorageManager>(new FileStorageManager(fileStorageSettings));
        }

        public static void ConfigureUserProfile(this IServiceCollection services, IConfiguration configuration) {
            UserProfileSettings userProfileSettings = configuration.GetSection(UserProfileSettings.UserProfile).Get<UserProfileSettings>();

            userProfileSettings.FileSignature = FileSignatureConfiguration.GetSignaturesFromExtensions(userProfileSettings.AllowedExtensions);

            services.AddSingleton<UserProfileSettings>(userProfileSettings);
        }

        public static void ConfigureAutoMapperProfile(this IServiceCollection services) {
            services.AddSingleton<IMapper>(provider => new MapperConfiguration(config =>
                config.AddProfile(new MappingProfile(provider.GetService<GeneralSettings>()))).CreateMapper());
        }

        public static void ConfigureAuthorization(this IServiceCollection services) {
            services.AddAuthorization(options => {
                options.AddPolicy("UserIsOwnerAdvert",
                    policy => policy.Requirements.Add(new UserIsOwnerAdvertRequirement()));
                options.AddPolicy("UserVisit",
                    policy => policy.Requirements.Add(new UserVisitRequirement()));
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration) {
            UserProfileSettings userProfileSettings = configuration.GetSection(UserProfileSettings.UserProfile).Get<UserProfileSettings>();

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
            .AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddEntityFrameworkStores<RepositoryContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration) {
            JwtSettings jwtSettings = configuration.GetSection(JwtSettings.JWT).Get<JwtSettings>();

            services.AddSingleton<JwtSettings>(jwtSettings);

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