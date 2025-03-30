using Hall = RegistrationService.Core.Entities.Hall;

namespace CinemaService.UseCases.Halls.Update
{
    public class UpdateHallHandler(IRepository<Hall> _repository)
    : ICommandHandler<UpdateHallComand, Result>
    {
        public async Task<Result> Handle(UpdateHallComand command, CancellationToken cancellationToken)
        {
            var hallToUpdate = await _repository.GetByIdAsync(command.Id);

            hallToUpdate.Status = command.hallStatus;
            await _repository.UpdateAsync(hallToUpdate);
            await _repository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
