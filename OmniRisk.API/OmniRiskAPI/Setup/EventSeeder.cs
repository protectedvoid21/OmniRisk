using Microsoft.AspNetCore.Identity;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Models;
using OmniRiskAPI.Persistence;

namespace OmniRiskAPI.Setup {
    public class EventSeeder {
        public static async Task Seed(OmniRiskDbContext dbContext, UserManager<AppUser> userManager) {
            var users = new List<AppUser>();
            if (!userManager.Users.Any()) {
                users = new List<AppUser> {
                    new AppUser {
                        UserName = "admin",
                        Email = "admin@admin.com"
                    },
                    new AppUser {
                        UserName = "user",
                        Email = "user@user.com"
                    },
                    new AppUser {
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new AppUser {
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                    new AppUser {
                        UserName = "mike",
                        Email = "mike@test.com"
                    },
                };

                var adminRole = new IdentityRole<Guid> { Name = "Admin", NormalizedName = "ADMIN" };
                var userRole = new IdentityRole<Guid> { Name = "User", NormalizedName = "USER" };

                dbContext.Roles.AddRange(adminRole, userRole);

                await dbContext.SaveChangesAsync();

                foreach (var user in users) {
                    await userManager.CreateAsync(user, "Haslo123!");
                }

                await userManager.AddToRoleAsync(users[0], "Admin");
                await userManager.AddToRoleAsync(users[1], "User");

                await dbContext.SaveChangesAsync();
            }

            if (dbContext.Events.Any()) {
                return;
            }

            var eventTypes = new[] {
                new EventType { Name = "Wypadek samochodowy" }, //0
                new EventType { Name = "Podpalenie" }, //1
                new EventType { Name = "Akt wandalizmu" }, //2
                new EventType { Name = "Nielegalne spozywanie alkoholu" }, //3
                new EventType { Name = "Zamieszki" }, //4
                new EventType { Name = "Kradziez" }, //5 
                new EventType { Name = "Dzikie wysypiska smieci" }, //6
                new EventType { Name = "Nielegalne rajdy samochodowe" }, //7
                new EventType { Name = "Proba oszustwa" }, //8
                new EventType { Name = "Przestepstwo na tle seksualnym" }, //9
                new EventType { Name = "Chuliganstwo" }, //10
                new EventType { Name = "Grozne zwierzeta" }, //11
                new EventType { Name = "Alert pogodowy" }, //12
                new EventType { Name = "Przerwa w dostawie pradu" }, //13
                new EventType { Name = "Przerwa w dostawie wody" }, //14
                new EventType { Name = "Przerwa w dostawie gazu" }, //15
                new EventType { Name = "Skazenie biologiczne" }, //16
                new EventType { Name = "Nielegalne skladowisko odpadow" }, //17
                new EventType { Name = "Przekroczenie limitu predkosci" }, //18
                new EventType { Name = "Utoniecie" }, //19
                new EventType { Name = "Zaklocenie ciszy nocnej" }, //20
                new EventType { Name = "Zaklocenie dzialania komunikacji miejskiej" }, //21
                new EventType { Name = "Rozbrajanie bomby" } //22
            };

            var eventsStatus = new[] {
                new EventStatus { Name = "Weryfikacja", },
                new EventStatus { Name = "Potwierdzone", },
                new EventStatus { Name = "Potwierdzone (wyeliminowane)", },
            };


            var @events = new[] {
                new Event {
                    Author = users[0],
                    Description = "Rozbicie szyb na przystanku",
                    CreationDate = DateTime.Now,
                    EventDate = DateTime.Now,
                    EventType = eventTypes[2],
                    EventStatus = eventsStatus[1],
                    Latitude = 51.116694f,
                    Longitude = 17.041798f,
                    IsAccepted = true,
                },
                new Event {
                    Author = users[1],
                    Description = "Obowiazuje od 29.05.2023 do 03.06.2023",
                    CreationDate = DateTime.Now,
                    EventDate = DateTime.Now,
                    EventType = eventTypes[13],
                    EventStatus = eventsStatus[0],
                    Latitude = 51.115249f,
                    Longitude = 17.035861f,
                    IsAccepted = false,
                },
                new Event {
                    Author = users[3],
                    Description = "Kradziez ze sklepu spozywczego, sprawcy ujeci",
                    CreationDate = DateTime.Now,
                    EventDate = DateTime.Now,
                    EventType = eventTypes[5],
                    EventStatus = eventsStatus[2],
                    Latitude = 51.099362f,
                    Longitude = 17.027687f,
                    IsAccepted = true,
                },
                new Event {
                    Author = users[2],
                    Description = "Bojki kiboli po meczu. Interweniowala policja.",
                    CreationDate = DateTime.Now,
                    EventDate = DateTime.Now,
                    EventType = eventTypes[4],
                    EventStatus = eventsStatus[2],
                    Latitude = 51.095318f,
                    Longitude = 16.997423f,
                    IsAccepted = true,
                },
                new Event {
                    Author = users[4],
                    Description = "Stluczka, tramwaj 16 - zmiana trasy przez Dworzec Glowny",
                    CreationDate = DateTime.Now,
                    EventDate = DateTime.Now,
                    EventType = eventTypes[15],
                    EventStatus = eventsStatus[1],
                    Latitude = 51.095343f,
                    Longitude = 17.042437f,
                    IsAccepted = true,
                },
                new Event {
                    Author = users[4],
                    Description = "",
                    CreationDate = DateTime.Now,
                    EventDate = DateTime.Now,
                    EventType = eventTypes[18],
                    EventStatus = eventsStatus[2],
                    Latitude = 51.106292f,
                    Longitude = 17.025594f,
                    IsAccepted = true,
                },
                new Event {
                    Author = users[3],
                    Description = "Wykolejenie pojazu szynowego",
                    CreationDate = DateTime.Now,
                    EventDate = DateTime.Now,
                    EventType = eventTypes[21],
                    EventStatus = eventsStatus[2],
                    Latitude = 51.100992f,
                    Longitude = 17.045817f,
                    IsAccepted = true,
                },
                new Event {
                    Author = users[1],
                    Description = "Znaleziono niewybuch 250kg",
                    CreationDate = DateTime.Now,
                    EventDate = DateTime.Now,
                    EventType = eventTypes[22],
                    EventStatus = eventsStatus[2],
                    Latitude = 51.124704f,
                    Longitude = 17.033404f,
                    IsAccepted = true,
                },
                new Event {
                    Author = users[1],
                    Description = "Zauwazono niedzwiedzia",
                    CreationDate = DateTime.Now,
                    EventDate = DateTime.Now,
                    EventType = eventTypes[11],
                    EventStatus = eventsStatus[1],
                    Latitude = 51.097134f,
                    Longitude = 17.074142f,
                    IsAccepted = true,
                },
                new Event {
                    Author = users[2],
                    Description = "",
                    CreationDate = DateTime.Now,
                    EventDate = DateTime.Now,
                    EventType = eventTypes[19],
                    EventStatus = eventsStatus[1],
                    Latitude = 51.114823f,
                    Longitude = 17.034471f,
                    IsAccepted = true,
                },
                new Event {
                    Author = users[4],
                    Description = "",
                    CreationDate = DateTime.Now,
                    EventDate = DateTime.Now,
                    EventType = eventTypes[0],
                    EventStatus = eventsStatus[0],
                    Latitude = 51.108508f,
                    Longitude = 17.027299f,
                    IsAccepted = true,
                },
            };

            dbContext.AddRange(eventsStatus);
            dbContext.AddRange(@events);
            await dbContext.SaveChangesAsync();
        }
    }
}