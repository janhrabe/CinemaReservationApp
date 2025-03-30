using FastEndpoints;
using MediatR;
using MovieService.UseCases.Movie.GetById;
using MovieService.UseCases.Movie.Update;

namespace MovieService.WebApi.Movies
{
    public class UpdateMovie(IMediator mediator) : Endpoint<UpdateMovieRequest, UpdateMovieResponse>
    {
        private readonly IMediator _mediator = mediator;

        public override void Configure()
        {
            Put(UpdateMovieRequest.Route);
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateMovieRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new UpdateMovieCommand(
                    request.MovieId,
                    request.Title!,
                    request.Description!,
                    request.Director!,
                    request.Cast!,
                    request.DurationInMinutes,
                    request.IsPlaying),
                cancellationToken
            );

            if (!result.IsSuccess || result.ErrorMessage == "Not Found")
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }

            var query = new GetMovieByIdQuery(request.MovieId);
            var queryResult = await _mediator.Send(query, cancellationToken);

            if (!queryResult.IsSuccess || queryResult.ErrorMessage == "Not Found")
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }

            if (queryResult.IsSuccess)
            {
                var dto = queryResult.Value;
                Response = new UpdateMovieResponse(
                    new MovieRecord(
                        dto.Title,
                        dto.Description,
                        dto.Director,
                        dto.Cast,
                        dto.DurationInMinutes,
                        dto.isPlaying
                    )
                );
                return;
            }

        }
    }
}