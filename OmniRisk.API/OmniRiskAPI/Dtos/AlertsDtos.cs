namespace OmniRiskAPI.Dtos; 

public record AddAlertRequest(Guid AuthorId, int AlertTypeId, float Latitude, float Longitude, string? Comment);

public record GetAlertResponse(Guid? AuthorId, string AuthorName, string? Comment);

public record AcceptAlertRequest(int AlertId, bool IsApproved);