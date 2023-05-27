using OmniRiskAPI.Persistence;

namespace OmniRiskAPI.Setup; 

public static class DbSeeder {
    public static WebApplication SeedDatabase(this WebApplication webApplication) {
        var scope = webApplication.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<OmniRiskDbContext>();
        
        AlertTypeSeeder.Seed(dbContext);

        return webApplication;
    }
}