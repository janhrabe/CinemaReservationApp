using RegistrationService.Core.Entities;

namespace CinemaService.UseCases.Rows.GetById;

public record GetByIdCommand(Guid rowId) : ICommand<Result<Row>>;
