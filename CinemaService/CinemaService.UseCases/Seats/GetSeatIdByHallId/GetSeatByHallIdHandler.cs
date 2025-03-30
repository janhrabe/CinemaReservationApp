using RegistrationService.Infrastructure.Repositories;

namespace CinemaService.UseCases.Seats.GetByHallId;

public class GetSeatByHallIdHandler(RowRepository rowRepository, SeatRepository seatRepository)
    : ICommandHandler<GetSeatByHallIdCommand, Result<List<Guid>>>
{
    public async Task<Result<List<Guid>>> Handle(GetSeatByHallIdCommand request, CancellationToken cancellationToken)
    {
        var rowsInHall = rowRepository.GetByHallId(request.HallId);

        var rowIds = new List<Guid>();

        foreach (var row in rowsInHall)
        {
            rowIds.Add(row.Id);
        }

        var seatIds = new List<Guid>();

        foreach (var row in rowIds)
        {
            var result = seatRepository.GetByRowId(row);
            foreach (var seat in result)
            {
                seatIds.Add(seat.Id);
            }
        }

        return Result.Success(seatIds);
    }
}
