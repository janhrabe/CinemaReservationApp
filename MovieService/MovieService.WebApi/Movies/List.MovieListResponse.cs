namespace MovieService.WebApi.Movies
{
    public class MovieListResponse
    {
        public required List<MovieRecord> Movies { get; set; }
    }
}