using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OmniRiskAPI.Authorization;
using OmniRiskAPI.Persistence;

namespace OmniRiskAPI.Authentication;

public static class AuthenticationExtensions {
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration,
        bool isProduction) {
        services.AddIdentity<AppUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<OmniRiskDbContext>()
            .AddDefaultTokenProviders();

        var key = Convert.FromBase64String(
            configuration.GetValue<string>("Authentication:Schemes:Bearer:SigningKeys:0:Value") ??
            throw new Exception("MissingIdentitySettingsSecret"));

        services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.Events = new JwtBearerEvents {
                    OnMessageReceived = context => {
                        context.Token = context.Token;
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context => {
                        context.NoResult();
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "text/plain";
                        return context.Response.WriteAsync(context.Exception.ToString());
                    },
                    OnChallenge = context => {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize("401 Not authorized");
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context => {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize("403 Not authorized");
                        return context.Response.WriteAsync(result);
                    },
                };
                x.RequireHttpsMetadata = isProduction;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetValue<string>("Authentication:Schemes:Bearer:ValidIssuer") ??
                                  throw new Exception("MissingIdentitySettingsSecret"),
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorizationBuilder().AddCurrentUserHandler();
        services.AddCurrentUser();
        return services;
    }
}