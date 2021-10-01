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
        public CorsPolicy Policy { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env) {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
            WebHostEnv = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            Policy = new CorsPolicy();
            Policy.Origins.Add("http://localhost:4200");

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
            services.AddScoped<IAuthorizationHandler, UserIsOwnerAdvertAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, UserVisitAuthorizationHandler>();
            services.AddHttpContextAccessor();

            services.AddAuthentication();

            services.AddAuthorization(options => {
                options.AddPolicy("UserIsOwnerAdvert",
                    policy => policy.Requirements.Add(new UserIsOwnerAdvertRequirement()));
                options.AddPolicy("UserVisit",
                    policy => policy.Requirements.Add(new UserVisitRequirement()));
            });

            services.ConfigureIdentity(Configuration);
            services.ConfigureJWT(Configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerManager logger, ICorsService corsService, ICorsPolicyProvider corsPolicyProvider) {


            if (WebHostEnv.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }


            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseStaticFiles(new StaticFileOptions {
                OnPrepareResponse = context => {
                    if (context.File.Name.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) {
                        var origin = context.Context.Request.Headers[CorsConstants.Origin];
                        var requestHeaders = context.Context.Request.Headers;

                        var isOptionsRequest = string.Equals(context.Context.Request.Method, CorsConstants.PreflightHttpMethod, StringComparison.OrdinalIgnoreCase);
                        var isPreflightRequest = isOptionsRequest && requestHeaders.ContainsKey(CorsConstants.AccessControlRequestMethod);

                        var corsResult = new CorsResult {
                            IsPreflightRequest = isPreflightRequest,
                            IsOriginAllowed = IsOriginAllowed(Policy, origin),
                        };

                        if (!corsResult.IsOriginAllowed) {
                            context.Context.Response.StatusCode = 204;
                        }
                    }

                }
            });


            //app.UseStaticFiles();
            // app.UseStaticFiles(new StaticFileOptions {
            //     ServeUnknownFileTypes = true,
            //     OnPrepareResponse = (ctx) => {
            //         var policy = corsPolicyProvider.GetPolicyAsync(ctx.Context, "CorsPolicy")
            //             .ConfigureAwait(false)
            //             .GetAwaiter().GetResult();

            //         var corsResult = corsService.EvaluatePolicy(ctx.Context, policy);

            //         ctx.Context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
            //         corsService.ApplyResult(corsResult, ctx.Context.Response);

            //     }

            //     // OnPrepareResponse = context => {
            //     //     context.Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
            //     // }

            //     // OnPrepareResponse = ctx => {


            //     //     // using Microsoft.AspNetCore.Http;
            //     //     ctx.Context.Response.Headers.Add(
            //     //          "Access-Control-Allow-Origin", "http://localhost:4200");
            //     // }


            //     //OnPrepareResponse = ctx => ctx.Context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200")
            // });


            // app.UseStaticFiles(new StaticFileOptions {
            //     OnPrepareResponse = ctx => {
            //         ctx.Context.Response.Headers.Append(new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("Access-Control-Allow-Origin", "*"));
            //         ctx.Context.Response.Headers.Append(new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept"));


            //     },

            //     // OnPrepareResponse = ctx => {
            //     //     // using Microsoft.AspNetCore.Http;
            //     //     ctx.Context.Response.Headers.Append(
            //     //          "Cache-Control", $"public, max-age={500}");
            //     // }

            //     // FileProvider = new PhysicalFileProvider(
            //     //         Path.Combine(Directory.GetCurrentDirectory(), "upload")),
            //     // RequestPath = new Microsoft.AspNetCore.Http.PathString("/upload")
            // });

            // app.UseStaticFiles(new StaticFileOptions {
            //     ServeUnknownFileTypes = true,
            //     DefaultContentType = "image/jpeg",
            //     OnPrepareResponse = ctx => {
            //         ctx.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //     }
            // });

            app.UseRouting();

            //app.UseCors("CorsPolicy");

            // app.UseStaticFiles(new StaticFileOptions() {
            //     OnPrepareResponse = ctx => {
            //         ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            //         ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers",
            //           "Origin, X-Requested-With, Content-Type, Accept");
            //     },

            // });

            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private bool IsOriginAllowed(CorsPolicy policy, StringValues origin) {
            if (StringValues.IsNullOrEmpty(origin)) {
                return false;
            }

            if (policy.AllowAnyOrigin || policy.IsOriginAllowed(origin)) {
                return true;
            }

            return false;
        }
    }
}
