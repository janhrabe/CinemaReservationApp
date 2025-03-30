using Microsoft.EntityFrameworkCore;
using RegistrationService.Infrastructure.Data;
using RegistrationService.Infrastructure.Repositories;
using Row = RegistrationService.Core.Entities.Row;

namespace CinemaService.UnitTests.Core
{
    public class RowRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly RowRepository _repository;
        private readonly Guid _hall = Guid.NewGuid();

        public RowRepositoryTests()
        {
            var _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new AppDbContext(_options);
            _repository = new RowRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Set<Row>().RemoveRange(_context.Set<Row>());

            for (int i = 1; i <= 10; i++)
            {
                _context.Set<Row>().Add(new Row(i, _hall));
            }
            _context.SaveChanges();
        }

        [Fact]
        public async Task AddingTenRowsToHall_ShouldReturnCorrectAmountOfRows()
        {
            // Act
            var rows = _repository.GetByHallId(_hall);

            // Assert
            Assert.Equal(10, rows.Count);
        }
        [Fact]
        public async Task LookingForExistingRow_ShouldReturntrue()
        {
            // Act
            for (int i = 1; i <= 10; i++)
            {
                var result = _repository.ExistsInHall(_hall, 1);

                //Assert
                Assert.Equal(true, result);
            }
        }
        [Fact]
        public async Task LookingForNonxistingRow_ShouldReturnFalse()
        {
            // Act
            var result = _repository.ExistsInHall(_hall, 1);

            //Assert
            Assert.Equal(true, result);

        }
    }
}
