namespace RegistrationService.Infrastructure.Repositories
{
    public class RowRepository(AppDbContext dbContext) : EfRepository<Row>(dbContext), IRowRepository
    {
        private readonly AppDbContext _dbContext = dbContext;
        public List<Row> GetByHallId(Guid hallId)
        {
            return dbContext.Set<Row>().Where(r => r.HallId == hallId).ToList();
        }

        public bool ExistsInHall(Guid hallId, int number)
        {
            return dbContext.Set<Row>().Any(r => r.HallId == hallId && r.Number == number);
        }
    }
}
