﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OU.Microservice.Shared.Options;
using OU.Microservice.Shared.Services;
using System.Security.Claims;

namespace OU.Microservice.Shared.Extensions
{
    public static class AuthenticationExt
    {
        public static IServiceCollection AddAuthenticationAndAuthorizationExt(this IServiceCollection services, IConfiguration configuration)
        {
            var identityOptions = configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();

            services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = identityOptions.Address;
                options.Audience = identityOptions.Audience;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() 
                {
                   ValidateAudience = true,
                   ValidateIssuerSigningKey = true,
                   ValidateLifetime = true,
                   ValidateIssuer = true,
                   RoleClaimType = "roles",
                   NameClaimType = "preferred_username"
                };
            }).AddJwtBearer("ClientCredentialSchema", options =>
            {
                options.Authority = identityOptions.Address;
                options.Audience = identityOptions.Audience;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                };
            });
            services.AddAuthorization(options => 
            {
                options.AddPolicy("ClientCredential", policy =>
                {
                    policy.AuthenticationSchemes.Add("ClientCredentialSchema");
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("client_id");
                });

                options.AddPolicy("Password", policy =>
                {

                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Email);

                });
            });
          

            return services;

        }
    }
}
