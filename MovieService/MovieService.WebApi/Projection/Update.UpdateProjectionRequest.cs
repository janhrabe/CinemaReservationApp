using Microsoft.AspNetCore.Mvc;

namespace MovieService.WebApi.Projection
{
    public class UpdateProjectionRequest
    {
        public const string Route = "/projections/{ProjectionId:Guid}";

        public static string BuildRoute(Guid projectionId) => Route.Replace("{ProjectionId:Guid}", projectionId.ToString());
        [FromRoute]
        public Guid ProjectionId { get; set; }
        public DateTime StartTime { get; set; }
        public Guid MovieId { get; set; }
        public Guid HallId { get; set; }
    }
}