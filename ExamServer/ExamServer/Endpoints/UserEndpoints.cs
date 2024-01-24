using Contracts;
using ExamServer.Features.Authorization.Login;
using ExamServer.Features.Authorization.Register;
using ExamServer.Features.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamServer.Endpoints;

public class UserEndpoints
{
    public static void Map(WebApplication app)
    {
        var routeGroup = app.MapGroup("/user").RequireAuthorization();

        routeGroup.MapGet("/{username}", async ([FromServices] IMediator mediator, string username) =>
        {
            var query = new UserQuery(){Username = username};
            var result = await mediator.Send(query);
            return result.Match(Results.Ok, Results.BadRequest);
        });
        
        routeGroup.MapGet("", async (HttpContext context, [FromServices] IMediator mediator) =>
        {
            var username = context.User.Identity.Name;
            var query = new UserQuery(){Username = username};
            var result = await mediator.Send(query);
            return result.Match(Results.Ok, Results.BadRequest);
        });

        //app.MapGet("/", () => "Hello world").RequireAuthorization();
    }
}