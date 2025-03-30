using Hall = RegistrationService.Core.Entities.Hall;

namespace CinemaService.UseCases.Halls.Create
{
    public class CreateHallHandler(IRepository<Hall> _repository)
        : ICommandHandler<CreateHallComand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateHallComand request, CancellationToken cancellationToken)
        {
            var hall = new Hall()
            {
                Name = request.name,
                Status = 0
            };

            var result = await _repository.AddAsync(hall, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(result.Id);
        }
    }
}
