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
using OnlineStore.API.Warehouse.PolicyRequirements;
using OnlineStore.Core;
using OnlineStore.Core.Business;
using OnlineStore.Core.Business.Contracts;
using OnlineStore.Core.Domain;
using Swashbuckle.AspNetCore.Swagger;

namespace OnlineStore.API.Warehouse
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

            /* Online Store Services */
            services.AddScoped<IWarehouseService, WarehouseService>();

            /* Configuration for authorization */

            services
                .AddMvcCore()
                .AddAuthorization(options =>
                {
                    options.AddPolicy(Security.Policies.SearchProductsPolicy, builder =>
                    {
                        builder.Requirements.Add(new SearchProductsPolicy());
                    });

                    options.AddPolicy(Security.Policies.GetProductInventoryPolicy, builder =>
                    {
                        builder.Requirements.Add(new GetProductInventoryPolicy());
                    });

                    options.AddPolicy(Security.Policies.PostProductPolicy, builder =>
                    {
                        builder.Requirements.Add(new PostProductPolicy());
                    });

                    options.AddPolicy(Security.Policies.PutProductUnitPricePolicy, builder =>
                    {
                        builder.Requirements.Add(new PutProductUnitPricePolicy());
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
                options.SwaggerDoc("v1", new Info { Title = "Online Store Warehouse API", Version = "v1" });

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

            /* Use authentication for Web API */

            app.UseAuthentication();

            /* Configuration for Swagger */

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options
                    .SwaggerEndpoint("/swagger/v1/swagger.json", "Online Store Warehouse API");
            });

            app.UseMvc();
        }
    }
#pragma warning restore CS1591
}
