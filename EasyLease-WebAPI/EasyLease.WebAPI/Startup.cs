using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyLease.Contracts;
using EasyLease.Entities.AppSettingsModels;
using EasyLease.WebAPI.Extensions;
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

            services.AddControllers(options => {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    _ => "The field is required.");
            });
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
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
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
