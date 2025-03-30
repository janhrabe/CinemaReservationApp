using Microsoft.AspNetCore.Mvc;

namespace MovieService.WebApi.Movies
{
    public class UpdateMovieRequest
    {
        public const string Route = "/movies/{MovieId:Guid}";

        public static string BuildRoute(Guid movieId) => Route.Replace("{MovieId:Guid}", movieId.ToString());
        [FromRoute]
        public Guid MovieId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Director { get; set; }
        public required string Cast { get; set; }
        public required long DurationInMinutes { get; set; }
        public required bool IsPlaying { get; set; }
    }
}