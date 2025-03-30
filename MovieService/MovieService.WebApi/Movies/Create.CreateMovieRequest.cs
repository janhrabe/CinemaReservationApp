namespace MovieService.WebApi.Movies
{
    public class CreateMovieRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Director { get; set; }
        public required string Cast { get; set; }
        public long DurationInMinutes { get; set; }
        public bool IsPlaying { get; set; }
    }
}