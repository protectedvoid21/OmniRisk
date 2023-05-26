namespace OmniRiskAPI.Api; 

public static class AlertsApi {
    public static RouteGroupBuilder MapAlerts(this IEndpointRouteBuilder routes) {
        var group = routes.MapGroup("Alerts");
        group.WithTags("alerts");

        group.MapGet("/", () => { });
        
        return group;
    }
}