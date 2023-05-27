using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OmniRiskAPI.Authentication;

namespace OmniRiskAPI.Authorization;

public static class CurrentUserExtensions {
    public static IServiceCollection AddCurrentUser(this IServiceCollection services) {
        services.AddScoped<CurrentUser>();
        services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
        return services;
    }

    private sealed class ClaimsTransformation : IClaimsTransformation {
        private readonly CurrentUser _currentUser;
        private readonly UserManager<AppUser> _userManager;

        public ClaimsTransformation(CurrentUser currentUser, UserManager<AppUser> userManager) {
            _currentUser = currentUser;
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal) {
            _currentUser.Principal = principal;

            if (principal.FindFirstValue(ClaimTypes.NameIdentifier) is { Length: > 0 } name) {
                _currentUser.User = await _userManager.FindByNameAsync(name);
            }

            return principal;
        }
    }
}