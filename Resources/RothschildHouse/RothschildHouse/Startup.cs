using System;
using System.IO;
using System.Reflection;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RothschildHouse.Controllers;
using RothschildHouse.Domain;
using Swashbuckle.AspNetCore.Swagger;

namespace RothschildHouse
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
            /* Setting up dependency injection */

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<ILogger<TransactionController>, Logger<TransactionController>>();

            // In-memory DbContext
            services.AddDbContext<PaymentDbContext>(options =>
            {
                options
                    .UseInMemoryDatabase("Payment")
                    .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            // Identity Server
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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "RothschildHouse API", Version = "v1" });

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

            // Seed initial data in-memory for DbContext
            var paymentDbContext = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetService<PaymentDbContext>();

            paymentDbContext.SeedInMemory();

            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "RothschildHouse API V1");
            });

            app.UseMvc();
        }
    }
#pragma warning restore CS1591
}
