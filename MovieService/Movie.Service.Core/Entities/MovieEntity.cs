namespace MovieService.Core.Entities
{
    public class MovieEntity : BaseEntity
    {
        public new Guid Id { get; set; }
        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string Director { get; set; }

        public required string Cast { get; set; }

        public required long DurationInMinutes { get; set; }
        public required bool IsPlaying { get; set; }


        public MovieEntity(string title, string description, string director, string cast, long durationInMinutes, bool isPlaying)
        {
            Title = title;
            Description = description;
            Director = director;
            Cast = cast;
            DurationInMinutes = durationInMinutes;
            IsPlaying = isPlaying;
        }

        public MovieEntity()
        {
        }

        public void UpdateMovie(string title, string description, string director, string cast, long durationInMinutes, bool isPlaying)
        {
            Title = title;
            Description = description;
            Director = director;
            Cast = cast;
            DurationInMinutes = durationInMinutes;
            IsPlaying = isPlaying;
        }

    }
}
