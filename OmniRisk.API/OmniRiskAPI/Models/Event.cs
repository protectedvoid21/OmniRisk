using OmniRiskAPI.Authentication;

namespace OmniRiskAPI.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EventDate { get; set; }
        public EventType EventType { get; set; }
        public int EventTypeId { get; set; }
        public int EventStatusId { get; set; }
        public EventStatus EventStatus { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public bool IsAccepted { get; set; }
        public Guid? AuthorId { get; set; }
        public AppUser Author { get; set; }
    }
}
