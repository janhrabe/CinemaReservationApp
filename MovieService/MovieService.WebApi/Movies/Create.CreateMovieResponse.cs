namespace MovieService.WebApi.Movies
{
    public class CreateMovieResponse
    {
        public Guid MovieId { get; set; }
        public string Message { get; set; }

        public CreateMovieResponse(Guid movieId)
        {
            MovieId = movieId;
            Message = "Movie created successfully.";
        }

        public CreateMovieResponse(string message)
        {
            MovieId = Guid.Empty;
            Message = message;
        }

    }
}