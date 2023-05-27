using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace OmniRiskAPI.Authorization;

public static class OpenApiExtensions {
    public static IEndpointConventionBuilder AddOpenApiSecurityRequirement(this IEndpointConventionBuilder builder) {
        var scheme = new OpenApiSecurityScheme() {
            Type = SecuritySchemeType.Http,
            Name = JwtBearerDefaults.AuthenticationScheme,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Reference = new() {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        };

        return builder.WithOpenApi(operation => new OpenApiOperation(operation) {
            Security = {
                new OpenApiSecurityRequirement {
                    [scheme] = new List<string>()
                }
            }
        });
    }
}