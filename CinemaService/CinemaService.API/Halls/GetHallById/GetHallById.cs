using CinemaService.UseCases.Halls.GetHallById;

namespace CinemaService.API.Halls.GetHallById
{
    public class GetHallById(IMediator mediator)
         : Endpoint<GetHallByIdRequest, bool>
    {
        public override void Configure()
        {
            Get("/halls/{Id}");
            AllowAnonymous();
        }
        public async override Task HandleAsync(GetHallByIdRequest req, CancellationToken ct)
        {

            var hallExists = await mediator.Send(new GetHallByIdCommand(req.Id), ct);

            if (hallExists)
            {
                Response = true;
            }
            else
            {
                Response = false;
            }
        }
    }
}


