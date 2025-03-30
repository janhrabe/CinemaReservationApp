using RegistrationService.Core.Entities;

namespace CinemaService.UseCases.Halls.GetHallById
{
    public class GetByIdHandler(IRepository<Hall> hallRepository)
       : ICommandHandler<GetHallByIdCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(GetHallByIdCommand request, CancellationToken cancellationToken)
        {
            var hall = await hallRepository.GetByIdAsync(request.hallId, cancellationToken);
            return hall != null;
        }
    }
}
