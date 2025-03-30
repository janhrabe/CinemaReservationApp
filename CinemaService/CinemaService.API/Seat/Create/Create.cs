using CinemaService.UseCases.Seats.Create;

namespace CinemaService.API.Seat.Create
{
    public class Create(IMediator mediator)
        : Endpoint<CreateSeatRequest, CreateSeatResponse>
    {
        public override void Configure()
        {
            Post(CreateSeatRequest.Route);
            AllowAnonymous();
            Summary(s =>
            {
                s.ExampleRequest = new CreateSeatRequest
                {
                    Number = 1,
                    RowId = new Guid()
                };
            });
        }
        public async override Task HandleAsync(CreateSeatRequest req, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateSeatCommand(req.Number, req.RowId), ct);

            if (result.IsSuccess)
            {
                Response = new CreateSeatResponse(req.Number, req.RowId);
                return;
            }
            ThrowError(string.Join("; ", result.Errors));
        }
    }
}
