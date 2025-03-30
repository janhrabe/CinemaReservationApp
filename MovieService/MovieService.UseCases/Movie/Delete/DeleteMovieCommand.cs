
using MediatR;
using MovieService.Core;

namespace MovieService.UseCases.Movie.Delete
{
    public record DeleteMovieCommand(Guid MovieId) : IRequest<Result>;
}