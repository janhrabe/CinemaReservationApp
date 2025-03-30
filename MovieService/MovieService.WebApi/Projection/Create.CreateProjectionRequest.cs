namespace MovieService.WebApi.Projection
{
    public class CreateProjectionRequest
    {
        public DateTime StartTime { get; set; }
        public Guid MovieId { get; set; }
        public Guid HallId { get; set; }
    }
}