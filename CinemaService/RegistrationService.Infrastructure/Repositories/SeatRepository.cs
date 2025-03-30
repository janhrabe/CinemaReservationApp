using System.Linq.Expressions;

namespace RegistrationService.Infrastructure.Repositories
{
    public class SeatRepository(AppDbContext dbContext) : EfRepository<Seat>(dbContext), ISeatRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public bool ExistsInRow(Guid rowId, int number)
        {
            return _dbContext.Set<Seat>().Any(s => s.RowId == rowId && s.Number == number);
        }

        public List<Seat> GetByRowId(Guid rowId)
        {
            return _dbContext.Set<Seat>().Where(s => s.RowId == rowId).ToList();
        }
        public async Task<List<Seat>> GetAllAsync(Expression<Func<Seat, bool>> predicate)
        {
            return await _dbContext.Set<Seat>().Where(predicate).ToListAsync();
        }
    }
}
