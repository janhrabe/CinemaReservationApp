namespace MovieService.UseCases.Projection.GetById
{
    public record ProjectionByIdDTO(Guid Id, DateTime StartTime, Guid MovieId, Guid HallId);

}