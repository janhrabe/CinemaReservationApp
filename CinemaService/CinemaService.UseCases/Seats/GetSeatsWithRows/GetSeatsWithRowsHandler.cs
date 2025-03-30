using MediatR;
using RegistrationService.Infrastructure.Repositories;

namespace CinemaService.UseCases.Seats.GetSeatsWithRows
{
    public class GetSeatsWithRowsHandler : IRequestHandler<GetSeatsWithRowsQuery, List<Tuple<int, int, Guid>>>
    {
        private readonly RowRepository _rowRepository;
        private readonly SeatRepository _seatRepository;

        public GetSeatsWithRowsHandler(RowRepository rowRepository, SeatRepository seatRepository)
        {
            _rowRepository = rowRepository;
            _seatRepository = seatRepository;
        }

        public async Task<List<Tuple<int, int, Guid>>> Handle(GetSeatsWithRowsQuery request, CancellationToken cancellationToken)
        {
            var rows = _rowRepository.GetByHallId(request.HallId);
            if (!rows.Any()) return new List<Tuple<int, int, Guid>>(); // Žádné řady

            var rowIds = rows.Select(r => r.Id).ToList();

            var seats = await _seatRepository.GetAllAsync(s => rowIds.Contains(s.RowId));

            return seats.Select(s => Tuple.Create(s.Number, rows.First(r => r.Id == s.RowId).Number, s.Id)).ToList();
        }
    }

}
