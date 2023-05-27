using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace OmniRiskAPI.Authentication;

public static class AuthenticationServiceExtensions {
    public static IServiceCollection AddTokenService(this IServiceCollection services) {
        return services.AddSingleton<ITokenService, TokenService>();
    }
}

public interface ITokenService {
    string GenerateToken(string username, bool isAdmin = false);
}

public sealed class TokenService : ITokenService {
    private readonly string _issuer;
    private readonly SigningCredentials _jwtSigningCredentials;
    private readonly Claim[] _audiences;

    public TokenService(IAuthenticationConfigurationProvider authenticationConfigurationProvider) {
        var bearerSection =
            authenticationConfigurationProvider.GetSchemeConfiguration(JwtBearerDefaults.AuthenticationScheme);

        var section = bearerSection.GetSection("SigningKeys:0");

        _issuer = bearerSection["ValidIssuer"] ?? throw new InvalidOperationException("Issuer is not specified");
        var signingKeyBase64 = section["Value"] ?? throw new InvalidOperationException("Signing key is not specified");

        var signingKeyBytes = Convert.FromBase64String(signingKeyBase64);

        _jwtSigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKeyBytes),
            SecurityAlgorithms.HmacSha256Signature);

        _audiences = bearerSection.GetSection("ValidAudiences").GetChildren()
            .Where(s => !string.IsNullOrEmpty(s.Value))
            .Select(s => new Claim(JwtRegisteredClaimNames.Aud, s.Value!))
            .ToArray();
    }

    public string GenerateToken(string username, bool isAdmin = false) {
        var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, username));

        var id = Guid.NewGuid().ToString().GetHashCode().ToString("x", CultureInfo.InvariantCulture);
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, id));

        if (isAdmin) {
            identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
        }

        identity.AddClaims(_audiences);

        var handler = new JwtSecurityTokenHandler();

        var jwtToken = handler.CreateJwtSecurityToken(
            _issuer,
            audience: null,
            identity,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(30),
            issuedAt: DateTime.UtcNow,
            _jwtSigningCredentials);

        return handler.WriteToken(jwtToken);
    }
}