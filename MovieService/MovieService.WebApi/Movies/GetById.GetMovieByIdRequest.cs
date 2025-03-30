using Microsoft.AspNetCore.Mvc;

namespace MovieService.WebApi.Movies
{
    public class GetMovieByIdRequest
    {

        [FromRoute]
        public Guid MovieId { get; set; }


    }
}