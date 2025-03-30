using RegistrationService.Infrastructure.Repositories;
using Seat = RegistrationService.Core.Entities.Seat;

namespace CinemaService.UseCases.Seats.Create
{
    public class CreateSeatHandler(SeatRepository repository)
        : ICommandHandler<CreateSeatCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateSeatCommand request, CancellationToken cancellationToken)
        {
            var seat = new Seat()
            {
                RowId = request.RowId,
                Number = request.Number,
            };

            var exists = repository.ExistsInRow(request.RowId, request.Number);

            if (exists)
            {
                return Result.Error("This seat already exists in this row");
            }

            var result = await repository.AddAsync(seat);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(result.Id);
        }
    }
}