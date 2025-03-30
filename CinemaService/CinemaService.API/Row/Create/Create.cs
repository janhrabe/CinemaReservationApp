using CinemaService.UseCases.Rows.Create;
namespace CinemaService.API.Row.Create
{
    public class Create(IMediator mediator)
        : Endpoint<CreateRowRequest, CreateRowResponse>
    {
        public override void Configure()
        {
            Post(CreateRowRequest.Route);
            AllowAnonymous();
            Summary(s =>
            {
                s.ExampleRequest = new CreateRowRequest { Number = 1 };
            });
        }
        public override async Task HandleAsync(CreateRowRequest req, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateRowComand(req.Number, req.HallId), ct);

            if (result.IsSuccess)
            {
                Response = new CreateRowResponse(req.Number, req.HallId);
                return;
            }
            ThrowError(string.Join("; ", result.Errors));

        }
    }
}
