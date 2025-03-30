using CinemaService.UseCases.Seats.GetSeatsWithRows;

namespace CinemaService.API.Seat.GetSeatsWithRow
{
    public class GetSeatsWithRow(IMediator mediator) : Endpoint<GetSeatsWithRowRequest, List<Tuple<int, int, Guid>>>
    {
        private readonly IMediator _mediator = mediator;

        public override void Configure()
        {
            Get("/Seats/by-hall/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetSeatsWithRowRequest req, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetSeatsWithRowsQuery(req.Id), ct);
            await SendAsync(result, cancellation: ct);
        }
    }
}
