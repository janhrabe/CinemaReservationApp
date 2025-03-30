namespace MovieService.UseCases.Movie
{
    public record CreateMovieDTO(string Title, string Description, string Director, string Cast, long DurationInMinutes, bool isPlaying);

}