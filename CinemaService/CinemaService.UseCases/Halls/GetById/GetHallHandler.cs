using Hall = RegistrationService.Core.Entities.Hall;


namespace CinemaService.UseCases.Halls.GetById
{
    public class GetHallHandler(IRepository<Hall> _repository)
        : ICommandHandler<GetHallComand, Result<Hall>>
    {
        public async Task<Result<Hall>> Handle(GetHallComand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id);

            return Result.Success(result);
        }

    }
}
