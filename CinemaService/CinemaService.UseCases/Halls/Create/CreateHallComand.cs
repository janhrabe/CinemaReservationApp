namespace CinemaService.UseCases.Halls.Create;
public record CreateHallComand(string name) : ICommand<Result<Guid>>;
