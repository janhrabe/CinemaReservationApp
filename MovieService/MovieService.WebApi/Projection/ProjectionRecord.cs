
namespace MovieService.WebApi.Projection
{

    public record ProjectionRecord(Guid Id, DateTime StartTime, Guid MovieId, Guid HallId);

}