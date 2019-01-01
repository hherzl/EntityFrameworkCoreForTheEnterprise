using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnLineStore.Core;
using OnLineStore.Core.BusinessLayer;
using OnLineStore.Core.BusinessLayer.Contracts;
using OnLineStore.Core.DataLayer;
using OnLineStore.WebAPI.PolicyRequirements;
using Swashbuckle.AspNetCore.Swagger;

namespace OnLineStore.WebAPI
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
            /* Setting dependency injection */

            // For DbContext
            services.AddDbContext<OnLineStoreDbContext>(options => options.UseSqlServer(Configuration["AppSettings:ConnectionString"]));

            // User info
            services.AddScoped<IUserInfo, UserInfo>();

            // Logger for services
            services.AddScoped<ILogger, Logger<Service>>();

            // Services
            services.AddScoped<IHumanResourcesService, HumanResourcesService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<ISalesService, SalesService>();

            /* Configuration for MVC */

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            /* Configuration for authentication */

            services
                .AddMvcCore()
                .AddAuthorization(options =>
                {
                    options.AddPolicy("AdministratorPolicy", builder => builder.Requirements.Add(new AdministratorPolicyRequirement()));
                    options.AddPolicy("CustomerPolicy", builder => builder.Requirements.Add(new CustomerPolicyRequirement()));
                });

            /* Configuration for IdentityServer authentication */

            services
                .AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    // todo: Set values from appsettings file

                    options.Authority = "http://localhost:56000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "OnLineStoreApi";
                });

            /* Configuration for Help page */

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "OnLine Store API", Version = "v1" });

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

            app.UseCors(policy =>
            {
                // Add client origin in CORS policy

                // todo: Set port number for client app from appsettings file

                policy.WithOrigins("http://localhost:4200");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });

            /* Use authentication for Web API */

            app.UseAuthentication();

            /* Configuration for Swagger */

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnLine Store API");
            });

            app.UseMvc();
        }
    }
#pragma warning restore CS1591
}
