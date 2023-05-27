using Microsoft.AspNetCore.Identity;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Models;
using OmniRiskAPI.Persistence;

namespace OmniRiskAPI.Setup
{
    public class EventSeeder
    {
        public static async Task Seed(OmniRiskDbContext dbContext, UserManager<AppUser> userManager)
        {
            var users = new List<AppUser>();
            if (!userManager.Users.Any())
            {
                users = new List<AppUser>
                {
                    new AppUser
                    {
                        UserName = "admin",
                        Email = "admin@admin.com"
                    },
                    new AppUser
                    {
                        UserName = "user",
                        Email = "user@user.com"
                    },
                    new AppUser
                    {
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new AppUser
                    {
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                };

                var adminRole = new IdentityRole<Guid> { Name = "Admin", NormalizedName = "ADMIN" };
                var userRole = new IdentityRole<Guid> { Name = "User", NormalizedName = "USER" };

                dbContext.Roles.AddRange(adminRole, userRole);

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Haslo1");
                }

                await userManager.AddToRoleAsync(users[0], "ADMIN");
                await userManager.AddToRoleAsync(users[1], "USER");

                await dbContext.SaveChangesAsync();
            }

            if (dbContext.Events.Any())
            {
                return;
            }

            var eventTypes = new[] {
            new EventType { Name = "Wypadek samochodowy" },
            new EventType { Name = "Podpalenie" },
            new EventType { Name = "Akt wandalizmu" },
            new EventType { Name = "Nielegalne spozywanie alkoholu" },
            new EventType { Name = "Zamieszki" },
            new EventType { Name = "Kradziez" },
        };

            var eventsStatus = new[] {
            new EventStatus
            {
                Name = "Test status 1",
            },
            new EventStatus
            {
                Name = "Test status 2",
            },
        };


            var @events = new[] {
            new Event 
            { 
                Author = users[0],
                Description = "Test description",
                CreationDate = DateTime.Now,
                EventDate = DateTime.Now,
                EventType = eventTypes[0],
                EventStatus = eventsStatus[0],
                Latitude = 52.2297f,
                Longitude = 21.0122f,
                IsAccepted = true,
            },
            new Event
            {
                Author = users[1],
                Description = "Test description",
                CreationDate = DateTime.Now,
                EventDate = DateTime.Now,
                EventType = eventTypes[1],
                EventTypeId = 2,
                EventStatus = eventsStatus[1],
                Latitude = 53.2297f,
                Longitude = 23.0122f,
                IsAccepted = false,
            },
        };

            dbContext.AddRange(eventsStatus);
            dbContext.AddRange(@events);
            dbContext.SaveChanges();
        }
    }
}
