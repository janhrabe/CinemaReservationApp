namespace CinemaService.UseCases.Halls.GetHallById
{
    public record GetHallByIdCommand(Guid hallId) : ICommand<Result<bool>>;
}
