using OmniRiskAPI.Models;
using OmniRiskAPI.Persistence;

namespace OmniRiskAPI.Setup; 

public static class AlertTypeSeeder {
    public static void Seed(OmniRiskDbContext dbContext) {
        if (dbContext.AlertTypes.Any()) {
            return;
        }
        
        var alertTypes = new[] {
            new AlertType { Name = "Traffic accident" },
            new AlertType { Name = "Arson" },
            new AlertType { Name = "Acts of wandalism" },
            new AlertType { Name = "Illegal alcohol consumption" },
            new AlertType { Name = "Riots" },
            new AlertType { Name = "Robbery" },
        };
        
        dbContext.AddRange(alertTypes);
        dbContext.SaveChanges();
    }
}