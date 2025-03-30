using RegistrationService.Infrastructure.Repositories;
using Row = RegistrationService.Core.Entities.Row;

namespace CinemaService.UseCases.Rows.Create
{
    public class CreateRowHandler(RowRepository repository)
        : ICommandHandler<CreateRowComand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateRowComand request, CancellationToken cancellationToken)
        {
            var row = new Row()
            {
                HallId = request.HallId,
                Number = request.Number,
            };

            var exists = repository.ExistsInHall(request.HallId, request.Number);

            if (exists)
            {
                return Result.Error("This row already exists in this hall");
            }

            var result = await repository.AddAsync(row);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(result.Id);
        }
    }
}
