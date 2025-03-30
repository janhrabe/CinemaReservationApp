using CinemaService.UseCases.Halls.GetAll;
using HallEntity = RegistrationService.Core.Entities.Hall;

namespace CinemaService.API.Halls.GetAll
{
    public class GetAll(IMediator mediator)
        : EndpointWithoutRequest<List<HallEntity>>
    {
        public override void Configure()
        {
            Get("/Halls");
            AllowAnonymous();

        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await mediator.Send(new GetHallCommand(), ct);

            if (result.IsSuccess)
            {
                Response = result;
            }
        }
    }
}
