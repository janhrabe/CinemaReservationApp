using MediatR;
using MovieService.Core;

namespace MovieService.UseCases.Movie.List
{
    public record ListMovieQuery() : IRequest<Result<List<MovieDTO>>>;
}