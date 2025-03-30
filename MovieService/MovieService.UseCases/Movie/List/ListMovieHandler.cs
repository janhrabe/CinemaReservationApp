using Marten;
using MediatR;
using MovieService.Core;
using MovieService.Core.Entities;

namespace MovieService.UseCases.Movie.List
{
    public class ListMoviesHandler(IQuerySession session) : IRequestHandler<ListMovieQuery, Result<List<MovieDTO>>>
    {
        private readonly IQuerySession _session = session;

        public async Task<Result<List<MovieDTO>>> Handle(ListMovieQuery request, CancellationToken cancellationToken)
        {
            var movies = await _session.Query<MovieEntity>().ToListAsync(cancellationToken);

            if (movies == null || movies.Count == 0)
            {
                return Result<List<MovieDTO>>.Failure("No movies found.");
            }

            var movieDTOs = movies.Select(m =>
                new MovieDTO(m.Title, m.Description, m.Director, m.Cast, m.DurationInMinutes, m.IsPlaying)).ToList();

            return Result<List<MovieDTO>>.Success(movieDTOs);
        }
    }




}