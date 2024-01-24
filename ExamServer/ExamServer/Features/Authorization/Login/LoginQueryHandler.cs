using Contracts;
using ExamServer.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExamServer.Features.Authorization.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<UserInfoDto, string>>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly SignInManager<Domain.Entities.User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginQueryHandler(UserManager<Domain.Entities.User> userManager,  
    SignInManager<Domain.Entities.User> signInManager, IJwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }
    
    public async Task<Result<UserInfoDto, string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            return "User doesn't exists!";
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        
        if (result.Succeeded)
        {
            return new UserInfoDto
            {
                Token = _jwtGenerator.GenerateToken(user),
                Username = user.UserName,
                Rating = user.Rating
            };
        }

        return "User doesn't exists!";
    }
}