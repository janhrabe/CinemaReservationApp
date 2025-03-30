using Microsoft.EntityFrameworkCore;
using RegistrationService.Core.Entities;
using RegistrationService.Infrastructure.Data;

using RegistrationService.Infrastructure.Repositories;

namespace CinemaService.UnitTests.Infrastructure
{
    public class SeatRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly SeatRepository _repository;
        private readonly Guid _rowId1 = Guid.NewGuid();
        private readonly Guid _rowId2 = Guid.NewGuid();
        public SeatRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new AppDbContext(options);
            _repository = new SeatRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Set<Seat>().RemoveRange(_context.Set<Seat>());

            for (int i = 1; i <= 10; i++)
            {
                _context.Set<Seat>().Add(new Seat(i, _rowId1));
            }
            for (int i = 1; i <= 10; i++)
            {
                _context.Set<Seat>().Add(new Seat(i, _rowId2));
            }
            _context.SaveChanges();
        }
        [Fact]
        public void SeatExistsInRow_ShouldReturnTrue()
        {
            // Act
            for (int i = 1; i <= 10; i++)
            {
                var result = _repository.ExistsInRow(_rowId1, i);

                // Assert
                Assert.True(result);
            }
        }
        [Fact]
        public void SeatDontExistsInRow_ShouldReturnFalse()
        {
            // Act
            var result = _repository.ExistsInRow(_rowId1, 11);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void GetsAllTheSeatsInTheRow_ShouldReturnCorrectSeats()
        {
            // Act
            var result = _repository.GetByRowId(_rowId1).OrderBy(r => r.Number).ToList();

            // Assert
            for (int i = 1; i <= 10; i++)
            {
                Assert.Equal(result[i - 1].Number, i);
            }
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnSeatsWithCorrectRowId()
        {
            // Act
            var seats = await _repository.GetAllAsync(s => s.RowId == _rowId1);

            // Assert
            Assert.Equal(10, seats.Count);
        }
    }
}
