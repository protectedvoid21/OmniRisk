using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OmniRiskAPI.Authorization;
using OmniRiskAPI.Dtos;
using OmniRiskAPI.Models;
using OmniRiskAPI.Persistence;

namespace OmniRiskAPI.Api;

public static class EventsApi {
    public static RouteGroupBuilder MapEvents(this IEndpointRouteBuilder routes) {
        var group = routes.MapGroup("Events");
        group.WithTags("events");

        group.MapGet("/", GetAll);
        group.MapPut("/", Accept);
        group.MapPost("/", Add)
            .RequireAuthorization(x => x.RequireCurrentUser())
            .AddOpenApiSecurityRequirement();

        return group;
    }

    private static async Task<Results<BadRequest, Ok<AddEventRequest>>> Add(
        [FromServices] OmniRiskDbContext dbContext, [FromBody] AddEventRequest request, CancellationToken ct) {
        var @event = new Event {
            EventTypeId = request.EventTypeId,
            Description = request.Description,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };

        dbContext.Add(@event);
        await dbContext.SaveChangesAsync(ct);

        return TypedResults.Ok(request);
    }
    
    private static async Task<Results<BadRequest, Ok<IEnumerable<GetEventResponse>>>> GetAll(
        [FromServices] OmniRiskDbContext dbContext, bool? acceptedOnly, CancellationToken ct) {
        var eventsResponse = dbContext.Events
            .Where(x => acceptedOnly == true ? x.IsAccepted : true)
            .Select(x => new GetEventResponse(x.Id, x.AuthorId, x.Author.UserName ?? "Unknown", x.Description, x.EventDate, x.EventType, x.EventStatus, x.Latitude, x.Longitude, x.Author))
            .AsEnumerable();
        return TypedResults.Ok(eventsResponse);
    }

    private static async Task<Results<BadRequest, Ok>> Accept(
        [FromServices] OmniRiskDbContext dbContext, [AsParameters] AcceptEventRequest request, CancellationToken ct) {
        var @event = await dbContext.Events.FindAsync(request.EventId);

        if (@event == null) {
            return TypedResults.BadRequest();
        }

        if (request.IsApproved) {
            @event.IsAccepted = true;
        }
        else {
            dbContext.Remove(@event);
        }

        await dbContext.SaveChangesAsync(ct);
        return TypedResults.Ok();
    }
}