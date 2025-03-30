using RegistrationService.Core.Entities;

namespace CinemaService.UseCases.Halls.GetAll;
public record GetHallCommand() : ICommand<Result<List<Hall>>>;
