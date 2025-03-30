
namespace MovieService.UseCases.Movie.Update
{


    public record MovieUpdateDTO(Guid Id, string Title, string Description, string Director, string Cast, long DurationInMinutes, bool isPlaying);


}