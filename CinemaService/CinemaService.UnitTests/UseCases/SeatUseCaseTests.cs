using Ardalis.Result;
using CinemaService.UseCases.Seats.Create;
using CinemaService.UseCases.Seats.GetSeatsWithRows;
using Microsoft.EntityFrameworkCore;
using RegistrationService.Core.Entities;
using RegistrationService.Infrastructure.Data;
using RegistrationService.Infrastructure.Repositories;

namespace CinemaService.UnitTests.UseCases
{
    public class SeatUseCaseUnitTests
    {
        private readonly SeatRepository _seatRepository;
        private readonly RowRepository _rowRepository;
        private readonly CancellationToken _token;
        private readonly AppDbContext _appDbContext;

        private readonly CreateSeatHandler _createSeatHandler;
        private readonly GetSeatsWithRowsHandler _getSeatsWithRowsHandler;

        private readonly Guid _hallId;
        private readonly Row row1full;
        private readonly Row row2full;

        private readonly List<Guid> seatIdsRow1;
        private readonly List<Guid> seatIdsRow2;
        public SeatUseCaseUnitTests()
        {
            var _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _appDbContext = new AppDbContext(_options);
            _seatRepository = new SeatRepository(_appDbContext);
            _rowRepository = new RowRepository(_appDbContext);

            _createSeatHandler = new CreateSeatHandler(_seatRepository);
            _getSeatsWithRowsHandler = new GetSeatsWithRowsHandler(_rowRepository, _seatRepository);

            _hallId = Guid.NewGuid();

            var row1 = new Row(1, _hallId);
            var row2 = new Row(2, _hallId);
            row1full = _rowRepository.AddAsync(row1).GetAwaiter().GetResult();
            _rowRepository.SaveChangesAsync().GetAwaiter().GetResult();

            row2full = _rowRepository.AddAsync(row2).GetAwaiter().GetResult();
            _rowRepository.SaveChangesAsync().GetAwaiter().GetResult();

            seatIdsRow1 = new List<Guid>();

            for (int i = 1; i <= 10; i++)
            {
                var seat = _seatRepository.AddAsync(new Seat(i, row1full.Id)).GetAwaiter().GetResult();
                _seatRepository.SaveChangesAsync().GetAwaiter().GetResult();
                seatIdsRow1.Add(seat.Id);
            }


            for (int i = 1; i <= 10; i++)
            {
                var seat = _seatRepository.AddAsync(new Seat(i, row2full.Id)).GetAwaiter().GetResult();
                _seatRepository.SaveChangesAsync().GetAwaiter().GetResult();
                seatIdsRow1.Add(seat.Id);
            }
        }

        [Fact]
        public async Task AddSeat_ShouldReturnCorrectRow()
        {
            // Arrange
            var seatName = 11;
            CreateSeatCommand comand = new CreateSeatCommand(seatName, row1full.Id);

            // Act
            var newSeatId = await _createSeatHandler.Handle(comand, _token);
            var result = await _seatRepository.GetByIdAsync(newSeatId.Value);

            //Assert
            Assert.Equal(seatName, result.Number);
        }
        [Fact]
        public async Task AddExistingSeat_ShouldThrowException()
        {
            // Arrange
            var seatName = 1;
            CreateSeatCommand comand = new CreateSeatCommand(seatName, row1full.Id);

            // Act
            var newSeatId = await _createSeatHandler.Handle(comand, _token);


            //Assert
            Assert.True(newSeatId.IsError());
        }
        [Fact]
        public async Task test()
        {
            // Arrange
            GetSeatsWithRowsQuery comand = new GetSeatsWithRowsQuery(_hallId);

            // Act
            var result = await _getSeatsWithRowsHandler.Handle(comand, _token);

            //Assert
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(result[i], new Tuple<int, int, Guid>(i + 1, 1, seatIdsRow1[i]));
            }
        }
    }
}
