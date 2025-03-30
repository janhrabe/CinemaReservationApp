using Marten;
using MediatR;
using MovieService.Core;
using MovieService.Core.Entities;

namespace MovieService.UseCases.Movie.Update
{
    public class UpdateMovieHandler(IDocumentSession session) : IRequestHandler<UpdateMovieCommand, Result<MovieUpdateDTO>>
    {
        private readonly IDocumentSession _session = session;

        public async Task<Result<MovieUpdateDTO>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var existingMovie = await _session.LoadAsync<MovieEntity>(request.MovieId, cancellationToken);
            if (existingMovie == null)
            {
                return Result<MovieUpdateDTO>.NotFound();
            }

            existingMovie.UpdateMovie(request.NewTitle!, request.NewDescription!, request.NewDirector, request.NewCast, request.NewDurationInMinutes, request.isPlaying);

            _session.Store(existingMovie);
            await _session.SaveChangesAsync(cancellationToken);

            return Result<MovieUpdateDTO>.Success(new MovieUpdateDTO(existingMovie.Id,
                existingMovie.Title, existingMovie.Description, existingMovie.Director, existingMovie.Cast, existingMovie.DurationInMinutes, existingMovie.IsPlaying));
        }
    }


}
