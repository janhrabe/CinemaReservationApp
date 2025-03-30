namespace MovieService.WebApi.Movies
{
    public class DetailMovieResponse
    {
        public required List<DetailMovieRecord> Movies { get; set; }
    }
}