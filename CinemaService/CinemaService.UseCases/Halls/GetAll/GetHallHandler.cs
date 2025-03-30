using RegistrationService.Infrastructure.Repositories;
using Hall = RegistrationService.Core.Entities.Hall;


namespace CinemaService.UseCases.Halls.GetAll
{
    public class GetAllHandler(HallRepository _repository)
        : ICommandHandler<GetHallCommand, Result<List<Hall>>>
    {
        public async Task<Result<List<Hall>>> Handle(GetHallCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAll();

            return Result.Success(result);
        }

    }
}
