namespace CinemaService.UseCases.Rows.GetByHallId;
public record GetByHallIdCommand(Guid HallId) : ICommand<Result<List<Guid>>>;
