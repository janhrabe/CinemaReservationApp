using RegistrationService.Core.Entities;

namespace CinemaService.UseCases.Halls.Update;
public record UpdateHallComand(Guid Id, HallStatus hallStatus) : ICommand<Result>;
