using Microsoft.AspNetCore.Mvc;

namespace MovieService.WebApi.Projection
{
    public class GetProjectionByIdRequest
    {
        public const string Route = "/projections/{ProjectionId:Guid}";
        [FromRoute]
        public Guid ProjectionId { get; set; }

        public static string BuildRoute(Guid projectionId) => Route.Replace("{ProjectionId:Guid}", projectionId.ToString());
    }
}