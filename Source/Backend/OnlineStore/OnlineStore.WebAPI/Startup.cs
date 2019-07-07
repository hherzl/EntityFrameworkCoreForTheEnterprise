using System;
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
using OnlineStore.Common.Security;
using OnlineStore.Core;
using OnlineStore.Core.BusinessLayer;
using OnlineStore.Core.BusinessLayer.Contracts;
using OnlineStore.Core.Domain;
using OnlineStore.WebAPI.Clients;
using OnlineStore.WebAPI.Clients.Contracts;
using OnlineStore.WebAPI.PolicyRequirements;
using Swashbuckle.AspNetCore.Swagger;

namespace OnlineStore.WebAPI
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
            services.AddDbContext<OnlineStoreDbContext>(builder =>
            {
                builder.UseSqlServer(Configuration["ConnectionStrings:OnlineStore"]);
            });

            // User info
            services.AddScoped<IUserInfo, UserInfo>();

            // Logger for services
            services.AddScoped<ILogger, Logger<Service>>();

            /* Identity Server for Online Store */

            services.Configure<OnlineStoreIdentityClientSettings>(Configuration.GetSection("OnlineStoreIdentityClientSettings"));
            services.AddSingleton<OnlineStoreIdentityClientSettings>();

            /* Rothschild House Payment gateway */
            services.Configure<RothschildHouseIdentitySettings>(Configuration.GetSection("RothschildHouseIdentitySettings"));
            services.AddSingleton<RothschildHouseIdentitySettings>();

            services.Configure<RothschildHousePaymentSettings>(Configuration.GetSection("RothschildHousePaymentSettings"));
            services.AddSingleton<RothschildHousePaymentSettings>();

            services.AddScoped<IRothschildHouseIdentityClient, RothschildHouseIdentityClient>();
            services.AddScoped<IRothschildHousePaymentClient, RothschildHousePaymentClient>();

            /* Online Store Services */
            services.AddScoped<IHumanResourcesService, HumanResourcesService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<ISalesService, SalesService>();

            /* Configuration for authorization */

            services
                .AddMvcCore()
                .AddAuthorization(options =>
                {
                    options
                        .AddPolicy(Policies.AdministratorPolicy, builder => builder.Requirements.Add(new AdministratorPolicyRequirement()));

                    options
                        .AddPolicy(Policies.WarehouseManagerPolicy, builder => builder.Requirements.Add(new WarehouseManagerPolicyRequirement()));

                    options
                        .AddPolicy(Policies.WarehouseOperatorPolicy, builder => builder.Requirements.Add(new WarehouseOperatorPolicyRequirement()));

                    options
                        .AddPolicy(Policies.CustomerPolicy, builder => builder.Requirements.Add(new CustomerPolicyRequirement()));
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
                options.SwaggerDoc("v1", new Info { Title = "Online Store API", Version = "v1" });

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
                app.UseDeveloperExceptionPage();

            app.UseCors(builder =>
            {
                // Add client origin in CORS policy

                // todo: Set port number for client app from appsettings file

                builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            /* Use authentication for Web API */

            app.UseAuthentication();

            /* Configuration for Swagger */

            app.UseSwagger();

            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Store API"));

            app.UseMvc();
        }
    }
#pragma warning restore CS1591
}
