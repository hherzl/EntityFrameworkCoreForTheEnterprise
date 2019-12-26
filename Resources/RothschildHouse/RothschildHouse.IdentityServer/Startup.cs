using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RothschildHouse.IdentityServer.Domain;
using RothschildHouse.IdentityServer.Services;
using RothschildHouse.IdentityServer.Validation;

namespace RothschildHouse.IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /* Setting up dependency injection */

            // For DbContext
            services.AddDbContext<AuthDbContext>(options => options.UseInMemoryDatabase("Auth"));

            // Password validator and profile
            services
                .AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
                .AddTransient<IProfileService, ProfileService>();

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

            /* Seed AuthDbContext in-memory */

            var authDbContext = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetService<AuthDbContext>();

            authDbContext.SeedInMemory();

            app.UseIdentityServer();
        }
    }
}
