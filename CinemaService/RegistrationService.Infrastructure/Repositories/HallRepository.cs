using Hall = RegistrationService.Core.Entities.Hall;

namespace RegistrationService.Infrastructure.Repositories
{
    public class HallRepository(AppDbContext dbContext) : EfRepository<Hall>(dbContext), IHallRepository
    {
        public async Task<List<Hall>> GetAll()
        {
            return dbContext.Set<Hall>().Where(h => h.Status == Cinema.Common.RequestModels.HallStatus.InOperation).ToList();
        }
    }
}
