using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OmniRiskAPI.Dtos;
using OmniRiskAPI.Models;
using OmniRiskAPI.Persistence;
using OmniRiskAPI.Services;

namespace OmniRiskAPI.Api
{
    public static class GptApi
    {
        public static RouteGroupBuilder MapGpt(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("Gpt");
            group.WithTags("gpt");

            group.MapGet("/", Get);

            return group;
        }

        private static async Task<Results<BadRequest, Ok<List<TweetModel>>>> Get(
            [FromServices] TwitterService twitterService,
            [FromServices] GptService gptService,
            [FromQuery] string lat,
            [FromQuery] string lng,
            [FromQuery] double radius)
        {
            var tweets = await twitterService.GetTweetModels(lat, lng, radius);
            var filtered = await gptService.AnalyzeText(tweets);
            return TypedResults.Ok(filtered);
        }
    }
}
