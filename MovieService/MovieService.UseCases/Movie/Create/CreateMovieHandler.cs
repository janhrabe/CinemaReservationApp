using Marten;
using MediatR;
using MovieService.Core;
using MovieService.Core.Entities;


namespace MovieService.UseCases.Movie.Create
{
    public class CreateMovieHandler(IDocumentSession session) : IRequestHandler<CreateMovieCommand, Result<Guid>>
    {
        private readonly IDocumentSession _session = session;

        public async Task<Result<Guid>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Title))
            {
                return Result<Guid>.Failure("Title is required.");
            }

            var newMovie = new MovieEntity
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Director = request.Director,
                Cast = request.Cast,
                DurationInMinutes = request.DurationInMinutes,
                IsPlaying = request.isPlaying,
            };

            _session.Store(newMovie);
            await _session.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(newMovie.Id);
        }
    }



}
