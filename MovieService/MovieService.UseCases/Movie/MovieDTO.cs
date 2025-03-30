namespace MovieService.UseCases.Movie
{
    public record MovieDTO(string Title, string Description, string Director, string Cast, long DurationInMinutes, bool isPlaying);
}
