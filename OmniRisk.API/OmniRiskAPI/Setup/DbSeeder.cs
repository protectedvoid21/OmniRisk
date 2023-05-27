using Microsoft.AspNetCore.Identity;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Models;
using OmniRiskAPI.Persistence;

namespace OmniRiskAPI.Setup; 

public static class DbSeeder {
    public static WebApplication SeedDatabase(this WebApplication webApplication) {
        var scope = webApplication.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<OmniRiskDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        EventSeeder.Seed(dbContext, userManager).Wait(); 

        return webApplication;
    }
}