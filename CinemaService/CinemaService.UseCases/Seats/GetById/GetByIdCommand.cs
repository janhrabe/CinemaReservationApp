using RegistrationService.Core.Entities;

namespace CinemaService.UseCases.Seats.GetById;

public record GetByIdCommand(Guid seatId) : ICommand<Result<Seat>>;
