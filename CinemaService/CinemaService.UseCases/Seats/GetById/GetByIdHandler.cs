using RegistrationService.Core.Entities;

namespace CinemaService.UseCases.Seats.GetById
{
    public class GetByIdHandler(IRepository<Seat> seatRepository)
    : ICommandHandler<GetByIdCommand, Result<Seat>>
    {
        public async Task<Result<Seat>> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await seatRepository.GetByIdAsync(request.seatId, cancellationToken);
            return Result.Success(result);
        }
    }
}
