using InfrastructureLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ServicesLayer.Services;
using ServicesLayer.Services.AccountsServices;


namespace ServicesLayer
{
    public static class ServicesDependencies
    {

        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {

            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IAccountsServices, AccountServices>();
            services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                //options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            })
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            return services;
        }

    }
}
