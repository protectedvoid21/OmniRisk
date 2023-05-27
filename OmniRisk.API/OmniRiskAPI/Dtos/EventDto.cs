
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Models;

namespace OmniRiskAPI.Dtos
{
    public record AddEventRequest(Guid? AuthorId, int EventTypeId, float Latitude, float Longitude, string? Description, int? EventStatusId, DateTime EventDate);

    public record GetEventResponse(int Id, Guid? AuthorId, string AuthorName, string? Description, DateTime? EventDate, EventType EventType, EventStatus EventStatus, float Latitude, float Longitude, AppUser Author);
    public record GetEventStatusResponse(int Id, string Name);
    public record GetEventTypeResponse(int Id, string Name);
    public record GetCrimeTypeResponse(int Id, string Name);

    public record AcceptEventRequest(int EventId, bool IsApproved);
}
