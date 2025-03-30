using MediatR;

namespace MovieService.UseCases.GetSeatsByProjectionId
{
    public record GetAvailableSeatsByProjectionCommand(Guid projectionId) : IRequest<List<Guid>>;
}
