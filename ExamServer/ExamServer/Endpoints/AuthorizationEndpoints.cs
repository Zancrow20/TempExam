using Contracts;
using ExamServer.Features.Authorization.Login;
using ExamServer.Features.Authorization.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExamServer.Endpoints;

public class AuthorizationEndpoints
{
    public static void Map(WebApplication app)
    {
        var routeGroup = app.MapGroup("/auth");

        routeGroup.MapPost("/register", 
            async ([FromServices] IMediator mediator, [FromBody] Register form) =>
            {
                var command = new RegisterCommand
                {
                    Username = form.Username, Password = form.Password, ConfirmPassword = form.ConfirmPassword
                };
                var result = await mediator.Send(command);
                return result.Match(Results.Ok, Results.BadRequest);
            })
            .Produces<string>()
            .Produces<string>(400);
        
        routeGroup.MapPost("/login", 
            async ([FromServices] IMediator mediator, [FromBody] Login form) =>
            {
                var query = new LoginQuery() {Username = form.Username, Password = form.Password};
                var result = await mediator.Send(query);
                return result.Match(Results.Ok, Results.BadRequest);
            })
            .Produces<UserInfoDto>()
            .Produces<string>(400);
    }
}