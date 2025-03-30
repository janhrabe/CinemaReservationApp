using FastEndpoints;
using Microsoft.CodeAnalysis;
using MovieService.Core;
using MovieService.Core.Interfaces;
using MovieService.Core.Specifications;
using MovieService.UseCases.Projection.ListByMovieId;

namespace MovieService.WebApi.Projection
{
    public class ListProjectionsEndpoint(IProjectionRepository repository) : Endpoint<ListProjectionsQuery, Result<ProjectionListResponse>>
    {
        private readonly IProjectionRepository _repository = repository;

        public override void Configure()
        {
            Get("/movies/{MovieId}/projections");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ListProjectionsQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProjectionsByMovieIdSpec(request.MovieId);
            var projections = await _repository.ListAsync(p => p.MovieId == request.MovieId, cancellationToken);

            if (projections == null || projections.Count == 0)
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }

            var projectionRecords = projections
                .Select(p => new ProjectionRecord(p.Id, p.StartTime, p.MovieId, p.HallId))
                .ToList();

            var response = new ProjectionListResponse
            {
                Projections = projectionRecords
            };

            await SendOkAsync(Result<ProjectionListResponse>.Success(response), cancellationToken);
        }
    }




}
