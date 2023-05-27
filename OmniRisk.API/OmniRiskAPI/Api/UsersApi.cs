using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Dtos;
using OmniRiskAPI.Filters;
using UserLoginInfo = OmniRiskAPI.Dtos.UserLoginInfo;

namespace OmniRiskAPI.Api;

public static class UsersApi {
    public static RouteGroupBuilder MapUsers(this IEndpointRouteBuilder routes) {
        var group = routes.MapGroup("users");

        group.WithTags("Users");

        group.WithParameterValidation(typeof(UserLoginInfo));

        group.MapPost("/register", Register);
        group.MapPost("/login", Login);

        return group;
    }

    private static async Task<Results<Ok, ValidationProblem>> Register(UserRegisterInfo registerInfo,
        UserManager<AppUser> userManager) {
        var user = new AppUser() {
            UserName = registerInfo.UserName,
            Email = registerInfo.Email
        };
        
        var result =
            await userManager.CreateAsync(user, registerInfo.Password);

        if (result.Succeeded) {
            return TypedResults.Ok();
        }

        return TypedResults.ValidationProblem(result.Errors.ToDictionary(e => e.Code,
            e => new[] { e.Description }));
    }

    private static async Task<Results<BadRequest, Ok<AuthToken>>> Login(UserLoginInfo userInfo,
        UserManager<AppUser> userManager,
        ITokenService tokenService) {
        var user = await userManager.FindByEmailAsync(userInfo.Email);

        if (user == null || !await userManager.CheckPasswordAsync(user, userInfo.Password)) {
            return TypedResults.BadRequest();
        }

        return TypedResults.Ok(new AuthToken(tokenService.GenerateToken(user.UserName!)));
    }
}