using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RothschildHouse.Models;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<PaymentDbContext>(options => options.UseInMemoryDatabase("Payment"));

            services
                .AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    // todo: Set values from appsettings file

                    options.Authority = "http://localhost:18000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "RothschildHouseApi";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            var paymentDbContext = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetService<PaymentDbContext>();

            paymentDbContext.SeedInMemory();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
#pragma warning restore CS1591
}
