using CinemaService.UseCases.Rows.GetById;
using RowEntity = RegistrationService.Core.Entities.Row;

namespace CinemaService.API.Rows.GetById
{
    public class GetById(IMediator mediator)
        : Endpoint<GetByIdRequest, RowEntity>
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
