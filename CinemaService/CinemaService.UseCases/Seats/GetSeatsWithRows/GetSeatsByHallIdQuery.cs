using MediatR;

namespace CinemaService.UseCases.Seats.GetSeatsWithRows
{
    public class GetSeatsWithRowsQuery(Guid hallId) : IRequest<List<Tuple<int, int, Guid>>>
    {
        public Guid HallId { get; } = hallId;
    }

}
