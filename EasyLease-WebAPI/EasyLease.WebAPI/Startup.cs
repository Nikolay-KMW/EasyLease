using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.WebAPI.ActionFilters;
using EasyLease.WebAPI.Extensions;
using EasyLease.WebAPI.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;

namespace EasyLease.WebAPI {
    public class Startup {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnv { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env) {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
            WebHostEnv = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();

            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryManager();
            services.ConfigureFileStorage(Configuration, WebHostEnv);
            services.ConfigureUserProfile(Configuration);
            services.ConfigureGeneralSettings(Configuration);

            services.AddAutoMapper(typeof(Startup));
            services.ConfigureAutoMapperProfile();

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddScoped<ValidationAdvertAttribute>();
            services.AddScoped<ValidationProfileAttribute>();
            services.AddScoped<ValidateAdvertExistsAttribute>();
            services.AddScoped<ValidateProfileExistsAttribute>();
            services.AddScoped<ValidationPhotoForAdvertAttribute>();
            services.AddScoped<ValidationPhotoForUserAttribute>();
            services.AddScoped<ValidateImageExistsAttribute>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();

            services.AddAuthentication();
            services.ConfigureIdentity(Configuration);
            services.ConfigureJWT(Configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerManager logger) {
            if (WebHostEnv.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
