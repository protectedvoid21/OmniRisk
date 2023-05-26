using Microsoft.EntityFrameworkCore;
using OmniRiskAPI.Models;

namespace OmniRiskAPI.Persistence; 

public class OmniRiskDbContext : DbContext {
    public OmniRiskDbContext(DbContextOptions<OmniRiskDbContext> options) : base(options) {}
    
    public DbSet<Alert> Alerts { get; set; }
}