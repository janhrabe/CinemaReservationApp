using CinemaService.UseCases.Halls.Create;

namespace CinemaService.API.Halls.Create
{
    public class Create(IMediator mediator)
        : Endpoint<GetHallRequest, CreateHallResponse>
    {
        public override void Configure()
        {
            Post(GetHallRequest.Route);
            AllowAnonymous();
            Summary(s =>
            {
                s.ExampleRequest = new GetHallRequest { Name = "New hall name" };
            });
        }
        public override async Task HandleAsync(GetHallRequest req, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateHallComand(req.Name), ct);

            if (result.IsSuccess)
            {
                Response = new CreateHallResponse(req.Name);
                return;
            }
        }
    }
}
