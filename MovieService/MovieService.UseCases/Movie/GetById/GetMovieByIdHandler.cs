using Marten;
using MediatR;
using MovieService.Core;
using MovieService.Core.Entities;

namespace MovieService.UseCases.Movie.GetById
{
    public class GetMovieHandler(IQuerySession session) : IRequestHandler<GetMovieByIdQuery, Result<MovieDTO>>
    {
        private readonly IQuerySession _session = session;

        public async Task<Result<MovieDTO>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _session.LoadAsync<MovieEntity>(request.MovieId, cancellationToken);
            if (entity == null) return Result<MovieDTO>.NotFound();

            var movieDto = new MovieDTO(entity.Title, entity.Description, entity.Director, entity.Cast, entity.DurationInMinutes, entity.IsPlaying);

            return Result<MovieDTO>.Success(movieDto);
        }
    }

}
