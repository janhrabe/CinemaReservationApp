namespace CinemaService.UseCases.Rows.Create;
public record CreateRowComand(int Number, Guid HallId) : ICommand<Result<Guid>>;
