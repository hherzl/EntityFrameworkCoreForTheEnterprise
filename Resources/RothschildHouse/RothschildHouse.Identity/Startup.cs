using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RothschildHouse.Identity.Domain;
using RothschildHouse.Identity.Services;
using RothschildHouse.Identity.Validation;

namespace RothschildHouse.Identity
{
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            /* Setting up dependency injection */

            // For DbContext
            services.AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("Auth"));

            // Password validator and profile
            services
                .AddTransient<IResourceOwnerPasswordValidator, RothschildHouseResourceOwnerPasswordValidator>()
                .AddTransient<IProfileService, RothschildHouseProfileService>();

            /* Identity Server */

            // Use in-memory configurations

            services
                .AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients());

            // Add authentication
            services
                .AddAuthentication()
                .AddIdentityServerAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            var authDbContext = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetService<IdentityDbContext>();

            /* Seed AuthDbContext in-memory */

            authDbContext
                .SeedInMemory();

            app.UseIdentityServer();
        }
    }
}
