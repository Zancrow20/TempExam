using Contracts;
using ExamServer.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExamServer.Features.User;

public class UserQueryHandler : IRequestHandler<UserQuery, Result<UserDto, string>>
{
    private readonly UserManager<Domain.Entities.User> _userManager;

    public UserQueryHandler(UserManager<Domain.Entities.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<UserDto, string>> Handle(UserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            return "Пользователя с таким именем не существует!";
        }

        var userDto = new UserDto() {Username = user.UserName, Rating = user.Rating};
        return userDto;
    }
}