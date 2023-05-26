using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Models;

namespace OmniRiskAPI.Persistence; 

public class OmniRiskDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid> {
    public OmniRiskDbContext(DbContextOptions<OmniRiskDbContext> options) : base(options) {}
    
    public DbSet<Alert> Alerts { get; set; }
    public DbSet<AlertType> AlertTypes { get; set; }
}