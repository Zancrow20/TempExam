using Contracts;
using ExamServer.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamServer.Features.Rating;

public class RatingQueryHandler : IRequestHandler<RatingQuery, Result<RatingDto, string>>
{
    private readonly UserManager<Domain.Entities.User> _userManager;

    public RatingQueryHandler(UserManager<Domain.Entities.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<RatingDto, string>> Handle(RatingQuery request, CancellationToken cancellationToken)
    {
        var rating = await _userManager.Users
            .Select(u => new UserDto(){ Username = u.UserName, Rating = u.Rating })
            .Skip(request.StartIndex)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken: cancellationToken);
        return new RatingDto(rating);
    }
}