using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.WebAPI.ActionFilters;
using EasyLease.WebAPI.Extensions;
using EasyLease.WebAPI.Services;
using EasyLease.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
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

            services.ConfigureGeneralSettings(Configuration);

            services.AddMemoryCache();

            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();

            services.AddAutoMapper(typeof(Startup));
            services.ConfigureAutoMapperProfile();

            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryManager();
            services.ConfigureFileStorage(Configuration, WebHostEnv);
            services.ConfigureUserProfile(Configuration);
            services.ConfigureAdvert(Configuration);

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.ConfigureValidation();

            services.AddScoped<AdditionalDataService>();

            services.AddHttpContextAccessor();

            services.ConfigureAuthentication();
            services.ConfigureAuthorization();

            services.ConfigureIdentity(Configuration);
            services.ConfigureJWT(Configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerManager logger, ICorsService corsService, ICorsPolicyProvider corsPolicyProvider, IWebHostEnvironment env) {

            if (WebHostEnv.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseRouting();
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
