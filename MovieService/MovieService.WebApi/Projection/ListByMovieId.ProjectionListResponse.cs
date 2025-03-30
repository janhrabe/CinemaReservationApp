namespace MovieService.WebApi.Projection
{
    public class ProjectionListResponse
    {
        public required List<ProjectionRecord> Projections { get; set; }
    }
}