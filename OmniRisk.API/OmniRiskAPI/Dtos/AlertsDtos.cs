namespace OmniRiskAPI.Dtos; 

public record AddAlertRequest(Guid AuthorId, int AlertTypeId, float Latitude, float Longitude, string? Comment);

public record GetAlertsResponse();