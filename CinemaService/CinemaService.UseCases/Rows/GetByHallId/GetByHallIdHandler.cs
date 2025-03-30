using RegistrationService.Infrastructure.Repositories;

namespace CinemaService.UseCases.Rows.GetByHallId
{
    public class GetByHallIdHandler(RowRepository repository)
        : ICommandHandler<GetByHallIdCommand, Result<List<Guid>>>
    {
        public async Task<Result<List<Guid>>> Handle(GetByHallIdCommand request, CancellationToken cancellationToken)
        {
            var rowsInHall = repository.GetByHallId(request.HallId);

            var rowIds = new List<Guid>();

            foreach (var row in rowsInHall)
            {
                rowIds.Add(row.Id);
            }

            return Result.Success(rowIds);
        }
    }
}
