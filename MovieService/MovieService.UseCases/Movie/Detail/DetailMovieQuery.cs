using MediatR;
using MovieService.Core;

namespace MovieService.UseCases.Movie.Detail
{
    public record DetailListMovieQuery() : IRequest<Result<List<DetailMovieDTO>>>;
}