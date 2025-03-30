namespace MovieService.UseCases.Movie.Detail
{
    public record DetailMovieDTO(Guid Id, string Title, string Description, string Director, string Cast, long DurationInMinutes, bool isPlaying);
}
