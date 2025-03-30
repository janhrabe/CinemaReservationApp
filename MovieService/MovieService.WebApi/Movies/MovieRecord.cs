namespace MovieService.WebApi.Movies
{
    public record MovieRecord(string Title, string Description, string Director, string Cast, long DurationInMinutes, bool isPlaying);
}
