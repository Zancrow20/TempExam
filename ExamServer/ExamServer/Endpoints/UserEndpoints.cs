using Contracts;
using ExamServer.Features.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExamServer.Endpoints;

public class UserEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/user/login", async (HttpContext context, [FromServices] IMediator mediator, [FromForm] Login form) =>
        {
            var query = new LoginQuery() { Username = form.Username, Password = form.Password};
            var result = await mediator.Send(query);
            return result.Match(success: Results.Ok, Results.BadRequest);
        });

        app.MapGet("/user/{username}", async (HttpContext context, [FromServices] IMediator mediator, string username) =>
        {
            
        }).RequireAuthorization();
        
        app.MapGet("/user", async (HttpContext context, [FromServices] IMediator mediator) =>
        {
            
        }).RequireAuthorization();

        app.MapGet("/", () => "Hello world");
    }
}