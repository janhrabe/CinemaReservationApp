using FastEndpoints;
using MovieService.Core.Interfaces;
using MovieService.UseCases.Movie.Delete;

namespace MovieService.WebApi.Movies
{


    public class DeleteMovieEndpoint(IMovieRepository repository) : Endpoint<DeleteMovieCommand>
    {
        private readonly IMovieRepository _repository = repository;

        public override void Configure()
        {
            Delete("/movies/{MovieId}");
            AllowAnonymous();
        }
        public override async Task HandleAsync(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _repository.GetByIdAsync(request.MovieId, cancellationToken);

            if (movie == null)
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }

            await _repository.DeleteAsync(movie, cancellationToken);

            await SendOkAsync(cancellationToken);
        }
    }


}