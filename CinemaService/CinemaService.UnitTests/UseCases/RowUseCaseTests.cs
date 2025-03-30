using Ardalis.Result;
using CinemaService.UseCases.Rows.Create;
using CinemaService.UseCases.Rows.GetByHallId;
using CinemaService.UseCases.Rows.GetById;
using Microsoft.EntityFrameworkCore;
using RegistrationService.Core.Entities;
using RegistrationService.Infrastructure.Data;
using RegistrationService.Infrastructure.Repositories;

namespace CinemaService.UnitTests.UseCases
{
    public class RowUseCaseUnitTests
    {
        private readonly RowRepository _repository;
        private readonly CancellationToken _token;
        private readonly AppDbContext _appDbContext;

        private readonly CreateRowHandler _createHandler;
        private readonly GetByHallIdHandler _getByHallIdHandler;
        private readonly GetByIdHandler _getByIdHandler;

        private readonly Guid _hallId;
        public RowUseCaseUnitTests()
        {
            var _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _appDbContext = new AppDbContext(_options);
            _repository = new RowRepository(_appDbContext);

            _createHandler = new CreateRowHandler(_repository);
            _getByHallIdHandler = new GetByHallIdHandler(_repository);
            _getByIdHandler = new GetByIdHandler(_repository);

            _hallId = Guid.NewGuid();

            for (int i = 1; i <= 10; i++)
            {
                _repository.AddAsync(new Row(i, _hallId));
                _repository.SaveChangesAsync();
            }

        }

        [Fact]
        public async Task AddRow_ShouldReturnCorrectRow()
        {
            // Arrange
            var rowName = 11;
            CreateRowComand comand = new(rowName, _hallId);

            // Act
            var newRowId = await _createHandler.Handle(comand, _token);
            var res = newRowId.Value;
            var result = await _repository.GetByIdAsync(res);

            //Assert
            Assert.Equal(rowName, result.Number);
        }

        [Fact]
        public async Task AddExistingRow_ShouldThrowException()
        {
            // Arrange
            var rowName = 1;
            CreateRowComand comand = new(rowName, _hallId);

            // Act
            var newRowId = await _createHandler.Handle(comand, _token);

            // Assert
            Assert.True(newRowId.IsError());
        }
        [Fact]
        public async Task GetByHallIdHandler_ShouldReturnCorrectRows()
        {
            GetByHallIdCommand command = new(_hallId);
            var rowsIds = await _getByHallIdHandler.Handle(command, _token);

            Assert.Equal(10, rowsIds.Value.Count);
        }
        [Fact]
        public async Task GetByIdHandler_ShouldReturnCorrectRow()
        {
            var row = new Row(11, _hallId);
            var insertedRow = await _repository.AddAsync(row);

            GetByIdCommand command = new(insertedRow.Id);
            var result = await _getByIdHandler.Handle(command, _token);

            Assert.Equal(result.Value.Id, insertedRow.Id);
        }
    }
}
