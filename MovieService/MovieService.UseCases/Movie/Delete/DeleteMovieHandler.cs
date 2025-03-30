using Marten;
using MediatR;
using MovieService.Core;
using MovieService.Core.Entities;

namespace MovieService.UseCases.Movie.Delete
{
    public class DeleteMovieHandler(IDocumentSession session) : IRequestHandler<DeleteMovieCommand, Result>
    {
        private readonly IDocumentSession _session = session;

        public async Task<Result> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _session.LoadAsync<MovieEntity>(request.MovieId, cancellationToken);

            if (movie == null)
            {
                return Result.Failure($"Movie with Id {request.MovieId} not found");
            }

            _session.Delete(movie);
            await _session.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}

