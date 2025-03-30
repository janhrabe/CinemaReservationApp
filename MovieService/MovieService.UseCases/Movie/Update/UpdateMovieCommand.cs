using MediatR;
using MovieService.Core;
namespace MovieService.UseCases.Movie.Update
{
    public record UpdateMovieCommand(Guid MovieId, string NewTitle, string NewDescription, string NewDirector, string NewCast, long NewDurationInMinutes, bool isPlaying) : IRequest<Result<MovieUpdateDTO>>;
}
