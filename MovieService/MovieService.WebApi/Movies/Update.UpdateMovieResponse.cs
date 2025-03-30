namespace MovieService.WebApi.Movies
{
    public class UpdateMovieResponse(MovieRecord movie)
    {
        public MovieRecord Movie { get; set; } = movie;
    }
}