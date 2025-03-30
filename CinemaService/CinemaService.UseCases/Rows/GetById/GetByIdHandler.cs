using RegistrationService.Core.Entities;

namespace CinemaService.UseCases.Rows.GetById
{
    public class GetByIdHandler(IRepository<Row> rowRepository)
    : ICommandHandler<GetByIdCommand, Result<Row>>
    {
        public async Task<Result<Row>> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await rowRepository.GetByIdAsync(request.rowId, cancellationToken);

            if (result is null)
            {
                return Result.Error("Row with this Id does not exists");
            }
            return Result.Success(result);
        }
    }
}
