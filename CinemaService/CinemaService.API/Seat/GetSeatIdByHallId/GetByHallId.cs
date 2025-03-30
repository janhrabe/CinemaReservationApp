using CinemaService.UseCases.Seats.GetByHallId;

namespace CinemaService.API.Seat.GetSeatIdByHallId
{
    public class GetByHallId(IMediator mediator)
        : Endpoint<GetByHallIdRequest, List<Guid>>
    {
        public override void Configure()
        {
            Get(GetByHallIdRequest.Route);
            AllowAnonymous();
        }
        public override async Task HandleAsync(GetByHallIdRequest req, CancellationToken ct)
        {
            var result = await mediator.Send(new GetSeatByHallIdCommand(req.Id), ct);

            if (result.IsSuccess)
            {
                Response = result;
            }
        }
    }
}
