using Contracts;
using MediatR;

namespace ExamServer.Features.Login;

public class LoginQuery : IRequest<Result<UserInfoDto, int>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}