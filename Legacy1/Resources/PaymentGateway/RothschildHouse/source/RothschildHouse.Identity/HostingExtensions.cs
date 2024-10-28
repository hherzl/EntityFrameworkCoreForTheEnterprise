using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Identity.Data;
using RothschildHouse.Identity.Data.Models;
using Serilog;

namespace RothschildHouse.Identity;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        builder
            .Services
            .AddDbContext<RothschildHouseDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")))
            ;

        builder
            .Services
            .AddIdentity<RothschildHouseUser, IdentityRole>()
            .AddEntityFrameworkStores<RothschildHouseDbContext>()
            .AddDefaultTokenProviders()
            ;

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<RothschildHouseUser>()
            ;

        //builder.Services.AddAuthentication()
        //    .AddGoogle(options =>
        //    {
        //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        //        // register your IdentityServer with Google at https://console.developers.google.com
        //        // enable the Google+ API
        //        // set the redirect URI to https://localhost:5001/signin-google
        //        options.ClientId = "copy client ID from Google here";
        //        options.ClientSecret = "copy client secret from Google here";
        //    });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app
            .MapRazorPages()
            .RequireAuthorization()
            ;

        return app;
    }
}
