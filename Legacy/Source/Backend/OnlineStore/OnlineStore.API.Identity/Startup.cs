using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.API.Identity.Domain;
using OnlineStore.API.Identity.Services;
using OnlineStore.API.Identity.Validation;

namespace OnlineStore.API.Identity
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            /* Setting up dependency injection */

            // DbContext
            services
                .AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("Identity"));

            // Password validator and profile
            services
                .AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
                .AddTransient<IProfileService, ProfileService>();

            /* Identity Server */

            services
                .AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients());

            services
                .AddAuthentication()
                .AddIdentityServerAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Seed IdentityDbContext in-memory

            var authDbContext = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetService<IdentityDbContext>();

            authDbContext.SeedInMemory();

            app.UseIdentityServer();
        }
    }
}
