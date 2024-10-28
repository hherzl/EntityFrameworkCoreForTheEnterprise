﻿using System;
using System.IO;
using System.Reflection;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineStore.API.Common.Clients;
using OnlineStore.API.Common.Clients.Contracts;
using OnlineStore.API.Sales.PolicyRequirements;
using OnlineStore.API.Sales.Security;
using OnlineStore.Core;
using OnlineStore.Core.Business;
using OnlineStore.Core.Business.Contracts;
using OnlineStore.Core.Domain;
using Swashbuckle.AspNetCore.Swagger;

namespace OnlineStore.API.Sales
{
#pragma warning disable CS1591
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /* Configuration for MVC */

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            /* Setting dependency injection */

            // For DbContext
            services.AddDbContext<OnlineStoreDbContext>(builder =>builder.UseSqlServer(Configuration["ConnectionStrings:OnlineStore"]));

            // User info
            services.AddScoped<IUserInfo, UserInfo>();

            // Logger for services
            services.AddScoped<ILogger, Logger<Service>>();

            /* Rothschild House Payment gateway */
            services.Configure<RothschildHouseIdentitySettings>(Configuration.GetSection("RothschildHouseIdentitySettings"));
            services.AddSingleton<RothschildHouseIdentitySettings>();

            services.Configure<RothschildHousePaymentSettings>(Configuration.GetSection("RothschildHousePaymentSettings"));
            services.AddSingleton<RothschildHousePaymentSettings>();

            services.AddScoped<IRothschildHouseIdentityClient, RothschildHouseIdentityClient>();
            services.AddScoped<IRothschildHousePaymentClient, RothschildHousePaymentClient>();

            /* Online Store Services */
            services.AddScoped<ISalesService, SalesService>();

            /* Configuration for authorization */

            services
                .AddMvcCore()
                .AddAuthorization(options =>
                {
                    options.AddPolicy(Policies.CustomerPolicy, builder =>
                    {
                        builder.Requirements.Add(new CustomerPolicyRequirement());
                    });
                });

            /* Configuration for Identity Server authentication */

            services
                .AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    var settings = new IdentityServerAuthenticationOptions();

                    Configuration.Bind("IdentityServerSettings", settings);

                    options.Authority = settings.Authority;
                    options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
                    options.ApiName = settings.ApiName;
                    options.ApiSecret = settings.ApiSecret;
                });

            /* Configuration for Help page */

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Online Store Sales API", Version = "v1" });

                // Get xml comments path
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // Set xml path
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                // Add client origin in CORS policy

                // todo: Set port number for client app from appsettings file

                builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                ;
            });

            /* Use authentication for Web API */

            app.UseAuthentication();

            /* Configuration for Swagger */

            app.UseSwagger();

            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Store Sales API"));

            app.UseMvc();
        }
    }
#pragma warning restore CS1591
}
