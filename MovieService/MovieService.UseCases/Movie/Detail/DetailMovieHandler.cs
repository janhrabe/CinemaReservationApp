using Marten;
using MediatR;
using MovieService.Core;
using MovieService.Core.Entities;

namespace MovieService.UseCases.Movie.Detail
{
    public class DetailMoviesHandler(IQuerySession session) : IRequestHandler<DetailListMovieQuery, Result<List<DetailMovieDTO>>>
    {
        private readonly IQuerySession _session = session;

        public async Task<Result<List<DetailMovieDTO>>> Handle(DetailListMovieQuery request, CancellationToken cancellationToken)
        {
            var movies = await _session.Query<MovieEntity>().ToListAsync(cancellationToken);

            if (movies == null || movies.Count == 0)
            {
                return Result<List<DetailMovieDTO>>.Failure("No movies found.");
            }

            var movieDTOs = movies.Select(m =>
                new DetailMovieDTO(m.Id, m.Title, m.Description, m.Director, m.Cast, m.DurationInMinutes, m.IsPlaying)).ToList();

            return Result<List<DetailMovieDTO>>.Success(movieDTOs);
        }
    }




}