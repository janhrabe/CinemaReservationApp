using CinemaService.UseCases.Seats.GetById;
using SeatEntity = RegistrationService.Core.Entities.Seat;

namespace CinemaService.API.Seat.GetById
{
    public class GetById(IMediator mediator)
        : Endpoint<GetByIdRequest, SeatEntity>
    {
        public override void Configure()
        {
            Get(GetByIdRequest.Route);
            AllowAnonymous();
        }
        public async override Task HandleAsync(GetByIdRequest req, CancellationToken ct)
        {
            var result = await mediator.Send(new GetByIdCommand(req.Id), ct);

            if (result.IsSuccess)
            {
                Response = result;
            }
        }
    }
}
