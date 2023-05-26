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
    public DbSet<CrimeType> CrimeTypes { get; set; }
    public DbSet<Cryminalist> Cryminalists { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventStatus> EventStatus { get; set; }
    public DbSet<EventType> EventType { get; set; }
    public DbSet<Person> Person { get; set; }
}