using System.Net;
using Contracts;
using Domain.Entities;
using ExamServer.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExamServer.Features.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<UserInfoDto, int>>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginQueryHandler(UserManager<User> userManager,  
    SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }
    
    public async Task<Result<UserInfoDto, int>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            return int.Parse(HttpStatusCode.Unauthorized.ToString());
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

        return int.Parse(HttpStatusCode.Unauthorized.ToString());
    }
}