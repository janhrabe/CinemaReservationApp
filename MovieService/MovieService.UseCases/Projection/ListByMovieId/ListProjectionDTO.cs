namespace MovieService.UseCases.Projection.ListByMovieId
{

    public record ListProjectionDTO(Guid ProjectionId, Guid MovieId, DateTime StartTime, Guid HallId);
}