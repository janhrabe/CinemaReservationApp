
using CinemaService.UseCases.Halls.Update;

namespace CinemaService.API.Halls.Update
{
    public class Update(IMediator mediator)
        : Endpoint<UpdateHallRequest, UpdateHallResponse>
    {
        public override void Configure()
        {
            Put(UpdateHallRequest.Route);
            AllowAnonymous();
        }
        public async override Task HandleAsync(UpdateHallRequest req, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateHallComand(req.Id, req.Status), ct);

            if (result.IsSuccess)
            {
                Response = new UpdateHallResponse(req.Id, req.Status);
            }
        }
    }
}
