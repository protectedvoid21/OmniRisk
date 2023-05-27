
namespace OmniRiskAPI.Dtos
{
    public record AddEventRequest(Guid AuthorId, int EventTypeId, float Latitude, float Longitude, string? Description);

    public record GetEventResponse(Guid? AuthorId, string AuthorName, string? Description);

    public record AcceptEventRequest(int EventId, bool IsApproved);
}
