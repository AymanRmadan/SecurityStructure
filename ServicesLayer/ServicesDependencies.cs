﻿using InfrastructureLayer.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServicesLayer.Services;
using ServicesLayer.Services.AccountsServices;
using System.Text;


namespace ServicesLayer
{
    public static class ServicesDependencies
    {

        public static IServiceCollection AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
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


            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                var validIssuers = configuration.GetSection("JWT:ValidIssuers").Get<string[]>();
                var validAudiences = configuration.GetSection("JWT:ValidAudiences").Get<string[]>();


                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                };


            });


            return services;
        }

    }
}
