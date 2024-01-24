﻿using Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExamServer.Features.Authorization.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string, string>>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IUserStore<Domain.Entities.User> _userStore;

    public RegisterCommandHandler(UserManager<Domain.Entities.User> userManager, IUserStore<Domain.Entities.User> userStore)
    {
        _userManager = userManager;
        _userStore = userStore;
    }

    public async Task<Result<string, string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        //Todo: Установить начальное значение рейтинга
        var user = new Domain.Entities.User() {Rating = 100};
        
        await _userStore.SetUserNameAsync(user, request.Username, cancellationToken);
        
        var checkPasswordRes = request.Password == request.ConfirmPassword;
        if (!checkPasswordRes)
            return new Result<string, string>(failure: "Пароли не совпадают!");

        var result = await _userManager.CreateAsync(user, request.Password);
        
        return !result.Succeeded ? new Result<string, string>(failure: "Ошибка регистрации. Попробуйте снова!") 
            : new Result<string, string>(success: "Вы успешно зарегистрировались!");
    }
}