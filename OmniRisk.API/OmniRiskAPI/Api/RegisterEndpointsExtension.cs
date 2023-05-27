namespace OmniRiskAPI.Api; 

public static class RegisterEndpointsExtension {
    public static IEndpointRouteBuilder RegisterApiEndpoints(this IEndpointRouteBuilder routes) {
        routes.MapAlerts();
        routes.MapUsers();
        
        return routes;
    }    
}