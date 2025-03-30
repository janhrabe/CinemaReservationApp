namespace CinemaService.UseCases.Seats.Create;
public record CreateSeatCommand(int Number, Guid RowId) : ICommand<Result<Guid>>;
