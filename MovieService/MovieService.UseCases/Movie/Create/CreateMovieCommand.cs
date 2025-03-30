using MediatR;
using MovieService.Core;


namespace MovieService.UseCases.Movie.Create
{
    public record CreateMovieCommand(string Title, string Description, string Director, string Cast, long DurationInMinutes, bool isPlaying) : IRequest<Result<Guid>>;

}
