using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Models;
using OmniRiskAPI.Persistence;

namespace OmniRiskAPI.Setup {
    public class CryminalSeeder {
        public static async Task Seed(OmniRiskDbContext dbContext, UserManager<AppUser> userManager) {
            var crimeType = new CrimeType { Name = "Pedofilia" };
            using StreamReader r = new StreamReader("data.json");
            string json = await r.ReadToEndAsync();
            
            var persons = JsonConvert.DeserializeObject<List<Person>>(json);

            var cryminalists = new List<Cryminalist>();
            for (int i = 0; i < 50; i++) {
                cryminalists.Add(new Cryminalist { Person = persons[i], CrimeType = crimeType });
            }

            dbContext.AddRange(crimeType);
            dbContext.AddRange(persons);
            dbContext.AddRange(cryminalists);
            await dbContext.SaveChangesAsync();
        }
    }
}
