using Microsoft.EntityFrameworkCore;

namespace OmniRiskAPI.Persistence; 

public class OmniRiskDbContext : DbContext {
    public OmniRiskDbContext(DbContextOptions<OmniRiskDbContext> options) : base(options) {}
}