﻿using System;
using System.Buffers;
using System.Data.Common;
using Pomelo.EntityFrameworkCore.MySql.IntegrationTests.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Pomelo.EntityFrameworkCore.MySql.IntegrationTests
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            Configuration = AppConfig.Config;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(options =>
            {
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new NewtonsoftJsonOutputFormatter(new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                }, ArrayPool<char>.Shared, options));
            });

            ConfigureEntityFramework(services);

            services
                .AddLogging(builder =>
                    builder
                        .AddConfiguration(AppConfig.Config.GetSection("Logging"))
                        .AddConsole()
                        .AddDebug()
                )
                .AddIdentity<AppIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDb>()
                .AddDefaultTokenProviders();

            services.AddControllers();
        }

        public static void ConfigureEntityFramework(IServiceCollection services, DbConnection connection = null)
        {
            if (connection == null)
            {
                services.AddDbContextPool<AppDb>(
                    options => options.UseMySql(AppConfig.Config["Data:ConnectionString"],
                        mysqlOptions =>
                        {
                            mysqlOptions.MaxBatchSize(AppConfig.EfBatchSize);
                            mysqlOptions.ServerVersion(AppConfig.Config["Data:ServerVersion"]);
                            if (AppConfig.EfRetryOnFailure > 0)
                            {
                                mysqlOptions.EnableRetryOnFailure(AppConfig.EfRetryOnFailure, TimeSpan.FromSeconds(5), null);
                            }
                        }
                ));
            }
            else
            {
                services.AddDbContext<AppDb>(
                    options => options.UseMySql(connection,
                        mysqlOptions =>
                        {
                            mysqlOptions.MaxBatchSize(AppConfig.EfBatchSize);
                            mysqlOptions.ServerVersion(AppConfig.Config["Data:ServerVersion"]);
                            if (AppConfig.EfRetryOnFailure > 0)
                            {
                                mysqlOptions.EnableRetryOnFailure(AppConfig.EfRetryOnFailure, TimeSpan.FromSeconds(5), null);
                            }
                        }
                ));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
