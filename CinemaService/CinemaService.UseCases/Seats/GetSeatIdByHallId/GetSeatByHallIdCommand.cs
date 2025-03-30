namespace CinemaService.UseCases.Seats.GetByHallId;
public record GetSeatByHallIdCommand(Guid HallId) : ICommand<Result<List<Guid>>>;
