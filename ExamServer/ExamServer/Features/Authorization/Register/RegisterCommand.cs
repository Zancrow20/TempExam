﻿using Contracts;
using MediatR;

namespace ExamServer.Features.Authorization.Register;

public class RegisterCommand : IRequest<Result<string, string>>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}