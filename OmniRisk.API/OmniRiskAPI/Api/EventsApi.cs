using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmniRiskAPI.Authorization;
using OmniRiskAPI.Dtos;
using OmniRiskAPI.Models;
using OmniRiskAPI.Persistence;
using System.Text;

namespace OmniRiskAPI.Api;

public static class EventsApi {
    public static RouteGroupBuilder MapEvents(this IEndpointRouteBuilder routes) {
        var group = routes.MapGroup("Events");
        group.WithTags("events");

        group.MapGet("/", GetAll);
        group.MapGet("/eventStatus", GetAllEventsStatuses);
        group.MapGet("/eventType", GetAllEventsTypes);
        group.MapGet("/crimeType", GetAllCrimeTypes);
        group.MapGet("/persons", GetAllPersons);
        group.MapPut("/", Accept);
        group.MapPost("/", Add);
            //.RequireAuthorization(x => x.RequireCurrentUser())
            //.AddOpenApiSecurityRequirement();

        return group;
    }

    private static async Task<Results<BadRequest, Ok<Event>>> Add(
        [FromServices] OmniRiskDbContext dbContext, [FromBody] AddEventRequest request, CancellationToken ct) {
        var @event = new Event {
            EventTypeId = request.EventTypeId,
            EventStatusId = 1,
            AuthorId = request.AuthorId,
            EventDate = request.EventDate,
            Description = request.Description,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };


    dbContext.Add(@event);
        await dbContext.SaveChangesAsync(ct);

        var dbObject = await dbContext.Events
            .Include(x => x.EventStatus)
            .Include(x => x.Author)
            .Include(x => x.EventType)
            .FirstOrDefaultAsync(x => x.Id == @event.Id);

        return TypedResults.Ok(dbObject);
    }
    
    private static async Task<Results<BadRequest, Ok<IEnumerable<GetEventResponse>>>> GetAll(
        [FromServices] OmniRiskDbContext dbContext, bool? acceptedOnly, CancellationToken ct) {
        var eventsResponse = dbContext.Events
            .Where(x => acceptedOnly == true ? x.IsAccepted : true)
            .Select(x => new GetEventResponse(x.Id, x.AuthorId, x.Author.UserName ?? "Unknown", x.Description, x.EventDate, x.EventType, x.EventStatus, x.Latitude, x.Longitude, x.Author))
            .AsEnumerable();
        return TypedResults.Ok(eventsResponse);
    }

    private static async Task<Results<BadRequest, Ok<IEnumerable<GetPersonsResponse>>>> GetAllPersons(
        [FromServices] OmniRiskDbContext dbContext,  CancellationToken ct)
    {
        var eventsResponse = dbContext.Person
            .Select(x => new GetPersonsResponse(x.Id, x.FirstName, x.Surname, Encoding.ASCII.GetBytes(x.PhotoUrl), x.CurrentLocation))
            .AsEnumerable();
        return TypedResults.Ok(eventsResponse);
    }

    private static async Task<Results<BadRequest, Ok<IEnumerable<GetEventStatusResponse>>>> GetAllEventsStatuses(
        [FromServices] OmniRiskDbContext dbContext, CancellationToken ct)
    {
        var eventsResponse = dbContext.EventStatus
            .Select(x => new GetEventStatusResponse(x.Id, x.Name))
            .AsEnumerable();
        return TypedResults.Ok(eventsResponse);
    }

    private static async Task<Results<BadRequest, Ok<IEnumerable<GetEventTypeResponse>>>> GetAllEventsTypes(
       [FromServices] OmniRiskDbContext dbContext, CancellationToken ct)
    {
        var eventsResponse = dbContext.EventType
            .Select(x => new GetEventTypeResponse(x.Id, x.Name))
            .AsEnumerable();
        return TypedResults.Ok(eventsResponse);
    }

    private static async Task<Results<BadRequest, Ok<IEnumerable<GetCrimeTypeResponse>>>> GetAllCrimeTypes(
       [FromServices] OmniRiskDbContext dbContext, CancellationToken ct)
    {
        var eventsResponse = dbContext.CrimeTypes
            .Select(x => new GetCrimeTypeResponse(x.Id, x.Name))
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