using IPInfo.Core;
using IPInfo.Core.Services;
using IPInfo.Data;
using IPInfo.Library;
using IPInfo.Library.Configuration;
using IPInfo.Library.Interfaces;
using IPInfo.Middlewares;
using IPInfo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Hangfire;
using Hangfire.SqlServer;
using System;
using System.Runtime.Caching;
using IPInfo.Services.Configuration;

namespace IPInfo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AddHangfire(services);

            services.AddControllers();

            services.AddDbContext<IPInfoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("IPInfo.Data")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton(new IPProviderConfiguration { APIRootUrl = Configuration.GetValue<string>("IPAPIRootUrl"), APIKey = Configuration.GetValue<string>("IPAPIKey") });
            services.AddSingleton(new ExpirationConfiguration { ExpirationMinutes = Configuration.GetValue<double>("CachingExpirationMinutes") });

            services.AddSingleton<ICachingService, CachingService>();
            services.AddScoped<IIPInfoProvider, IPInfoProvider>();
            services.AddScoped<IIPService, IPService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IP Info API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My IP Info API v1");
            });
        }

        private void AddHangfire(IServiceCollection services)
        {
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("Default"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer();
        }
    }
}
