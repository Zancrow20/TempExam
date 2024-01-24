using Contracts;
using ExamServer.Features.Shared;
using MediatR;

namespace ExamServer.Features.User;

public class UserQuery: IRequest<Result<UserDto, string>>
{
    public string Username { get; set; }
}