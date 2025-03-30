using CinemaService.UseCases.Halls.GetById;
using HallEntity = RegistrationService.Core.Entities.Hall;

namespace CinemaService.API.Halls.GetById
{
    public class GetById(IMediator mediator)
        : Endpoint<GetHallRequest, HallEntity>
    {
        public override void Configure()
        {
            Get(GetHallRequest.Route);
            AllowAnonymous();

        }
        public override async Task HandleAsync(GetHallRequest req, CancellationToken ct)
        {
            var result = await mediator.Send(new GetHallComand(req.Id), ct);

            if (result.IsSuccess)
            {
                Response = result;
            }
        }
    }
}
