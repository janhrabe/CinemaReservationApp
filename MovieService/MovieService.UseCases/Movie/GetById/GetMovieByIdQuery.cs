using MediatR;
using MovieService.Core;

namespace MovieService.UseCases.Movie.GetById
{
    public record GetMovieByIdQuery(Guid MovieId) : IRequest<Result<MovieDTO>>;
}
