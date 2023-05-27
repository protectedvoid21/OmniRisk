using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Dtos;
using UserLoginInfo = OmniRiskAPI.Dtos.UserLoginInfo;

namespace OmniRiskAPI.Api; 

public static class UsersApi {
    public static RouteGroupBuilder MapUsers(this IEndpointRouteBuilder routes) {
        var group = routes.MapGroup("Users");
        group.WithTags("users");

        group.MapPost("/register", Register);
    }

    private static async Task<Results<NoContent, ValidationProblem>> Register(
        [FromServices] UserManager<AppUser> userManager, UserRegisterInfo userInfo) {
        var user = new AppUser {
            UserName = userInfo.UserName,
            Email = userInfo.Password
        };
        var result = await userManager.CreateAsync(user, userInfo.Password);

        if (result.Succeeded) {
            return TypedResults.NoContent();
        }

        return TypedResults.ValidationProblem(result.Errors.ToDictionary(e => e.Code, e => new[] { e.Description }));
    }

    private static async Task<Results<Ok<AuthToken>, BadRequest>> Login(
        [FromServices] UserManager<AppUser> userManager, UserLoginInfo userInfo) {
        var user = await userManager.FindByEmailAsync(userInfo.Email);
        if (user == null) {
            return TypedResults.BadRequest();
        }

        bool canLogIn = await userManager.CheckPasswordAsync(user, userInfo.Password);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(ClaimTypes.Email, user.Email) 
            }),
            Audience = audience,
            Issuer = issuer,
            Expires = DateTime.Now.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new {
            token = tokenHandler.WriteToken(token),
            expiration = tokenDescriptor.Expires
        });
    }
}