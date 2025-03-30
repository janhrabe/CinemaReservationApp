using Microsoft.AspNetCore.Mvc;

namespace MovieService.WebApi.Movies
{
    public record DeleteMovieRequest
    {
        [FromRoute]
        public Guid MovieId { get; set; }
    }
}