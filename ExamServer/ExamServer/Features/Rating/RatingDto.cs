using ExamServer.Features.Shared;

namespace ExamServer.Features.Rating;

public record RatingDto(List<UserDto> UsersRating);