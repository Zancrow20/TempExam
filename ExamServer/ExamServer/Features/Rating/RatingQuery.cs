using Contracts;
using MediatR;

namespace ExamServer.Features.Rating;

public class RatingQuery : IRequest<Result<RatingDto, string>>
{
    public int PageSize { get; set; }
    public int StartIndex { get; set; }
}