using Contracts;
using ExamServer.Features.Rating;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExamServer.Endpoints;

public class RatingEndpoints
{
    public static void Map(WebApplication app)
    {
        var routeGroup = app.MapGroup("/rating").RequireAuthorization();

        routeGroup.MapGet("/all", async ([FromServices] IMediator mediator, [FromBody] Pagination pagination) =>
        {
            var query = new RatingQuery() {PageSize = pagination.PageSize, StartIndex = pagination.StartIndex};
            var result = await mediator.Send(query);
            return result.Match(Results.Ok, Results.BadRequest);
        });
    }
}