using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OmniRiskAPI.Authorization;
using OmniRiskAPI.Dtos;
using OmniRiskAPI.Models;
using OmniRiskAPI.Persistence;

namespace OmniRiskAPI.Api;

public static class AlertsApi {
    public static RouteGroupBuilder MapAlerts(this IEndpointRouteBuilder routes) {
        var group = routes.MapGroup("Alerts");
        group.WithTags("alerts");

        group.MapGet("/", GetAll);
        group.MapPut("/", Accept);
        group.MapPost("/", Add)
            .RequireAuthorization(x => x.RequireCurrentUser())
            .AddOpenApiSecurityRequirement();

        return group;
    }

    private static async Task<Results<BadRequest, Ok<AddAlertRequest>>> Add(
        [FromServices] OmniRiskDbContext dbContext, [FromBody] AddAlertRequest request, CancellationToken ct) {
        var alert = new Alert {
            AlertTypeId = request.AlertTypeId,
            Comment = request.Comment,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };

        dbContext.Add(alert);
        await dbContext.SaveChangesAsync(ct);

        return TypedResults.Ok(request);
    }

    private static async Task<Results<BadRequest, Ok<IEnumerable<GetAlertResponse>>>> GetAll(
        [FromServices] OmniRiskDbContext dbContext, CancellationToken ct) {
        var alertResponses = dbContext.Alerts
            .Where(x => x.IsAccepted)
            .Select(x => new GetAlertResponse(x.AuthorId, x.Author.UserName ?? "Deleted", x.Comment))
            .AsEnumerable();
        return TypedResults.Ok(alertResponses);
    }

    private static async Task<Results<BadRequest, Ok>> Accept(
        [FromServices] OmniRiskDbContext dbContext, [AsParameters] AcceptAlertRequest request, CancellationToken ct) {
        var alert = await dbContext.Alerts.FindAsync(request.AlertId);

        if (alert == null) {
            return TypedResults.BadRequest();
        }

        if (request.IsApproved) {
            alert.IsAccepted = true;
        }
        else {
            dbContext.Remove(alert);
        }

        await dbContext.SaveChangesAsync(ct);
        return TypedResults.Ok();
    }
}