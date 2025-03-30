using RegistrationService.Core.Entities;

namespace CinemaService.UseCases.Halls.GetById;
public record GetHallComand(Guid Id) : ICommand<Result<Hall>>;
