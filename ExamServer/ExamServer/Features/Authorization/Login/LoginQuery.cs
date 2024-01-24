using Contracts;
using MediatR;

namespace ExamServer.Features.Authorization.Login;

public class LoginQuery : IRequest<Result<UserInfoDto, string>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}