using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Models;
using OmniRiskAPI.Persistence;

namespace OmniRiskAPI.Setup {
    public class CryminalSeeder {
        public static async Task Seed(OmniRiskDbContext dbContext, UserManager<AppUser> userManager) {
            var crimeType = new CrimeType { Id = 0, Name = "Pedofilia" };
            using StreamReader r = new StreamReader("data.json");
            string json = r.ReadToEnd();
            
            var persons = JsonConvert.DeserializeObject<List<Person>>(json);

            var cryminalists = new List<Cryminalist>();
            for (int i = 0; i < 50; i++) {
                cryminalists.Add(new Cryminalist { Id = i, PersonId = i, CrimeTypeId = 0 });
            }

            dbContext.AddRange(crimeType);
            dbContext.AddRange(persons);
            dbContext.AddRange(@cryminalists);
            await dbContext.SaveChangesAsync();
        }
    }
}
