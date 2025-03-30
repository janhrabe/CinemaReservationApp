namespace MovieService.WebApi.Movies
{
    public record DetailMovieRecord(Guid Id, string Title, string Description, string Director, string Cast, long DurationInMinutes, bool isPlaying);
}
