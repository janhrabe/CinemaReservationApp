namespace MovieService.WebApi.Projection
{
    public class UpdateProjectionResponse(ProjectionRecord projection)
    {
        public ProjectionRecord Projection { get; set; } = projection;
    }
}