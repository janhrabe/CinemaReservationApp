
using CinemaService.API.Rows.GetById;
using CinemaService.UseCases.Rows.GetByHallId;

namespace CinemaService.API.Row.GetByHallId
{
    public class GetByHallId(IMediator mediator)
        : Endpoint<GetByIdRequest, List<Guid>>
    {
        public override void Configure()
        {
            Get(GetByIdRequest.Route);
            AllowAnonymous();
        }
        public override async Task HandleAsync(GetByIdRequest req, CancellationToken ct)
        {
            var result = await mediator.Send(new GetByHallIdCommand(req.Id), ct);

            if (result.IsSuccess)
            {
                Response = result;
            }
            ThrowError(string.Join("; ", result.Errors));
        }
    }
}
