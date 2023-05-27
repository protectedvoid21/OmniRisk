using Microsoft.AspNetCore.Identity;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Models;
using OmniRiskAPI.Persistence;

namespace OmniRiskAPI.Setup {
    public class CryminalSeeder {
        public static async Task Seed(OmniRiskDbContext dbContext, UserManager<AppUser> userManager) {
            var crimeType = new CrimeType { Id = 0, Name = "Pedofilia" };
            using (StreamReader r = new StreamReader("data.json")) {
                string json = r.ReadToEnd();
                List<Person> persons = JsonConvert.DeserializeObject<List<Person>>(json);
            }
            
            var @cryminalists = [ new Cryminalist { id = i, PersonId = i, crimeTypeId = 0 } for i in range(50) ]

            dbContext.AddRange(crimeType);
            dbContext.AddRange(persons);
            dbContext.AddRange(@cryminalists);
            await dbContext.SaveChangesAsync();
        }
    }
}
