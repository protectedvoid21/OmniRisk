namespace OmniRiskAPI.Models
{
    public class Cryminalist
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int CrimeTypeId { get; set; }
        public CrimeType CrimeType { get; set; }
    }
}
