namespace OmniRiskAPI.Api; 

public static class RegisterEndpointsExtension {
    public static IEndpointRouteBuilder RegisterApiEndpoints(this IEndpointRouteBuilder routes) {
        routes.MapGpt();
        routes.MapEvents();
        return routes;
    }    
}