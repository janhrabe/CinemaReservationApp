namespace MovieService.WebApi.Projection
{
    public class CreateProjectionResponse
    {
        public Guid MovieId { get; set; }
        public string Message { get; set; }

        public CreateProjectionResponse(Guid movieId)
        {
            MovieId = movieId;
            Message = "Projection created successfully.";
        }

        public CreateProjectionResponse(string message)
        {
            MovieId = Guid.Empty;
            Message = message;
        }

    }
}