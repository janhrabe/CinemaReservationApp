using Ardalis.SharedKernel;
using Cinema.Common.RequestModels;
using CinemaService.UseCases.Halls.Create;
using CinemaService.UseCases.Halls.Update;
using Microsoft.EntityFrameworkCore;
using RegistrationService.Core.Entities;
using RegistrationService.Infrastructure.Data;
using RegistrationService.Infrastructure.Repositories;

namespace CinemaService.UnitTests.UseCases
{
    public class HallsUseCaseUnitTests
    {
        private readonly CreateHallHandler _createHandler;
        private readonly UpdateHallHandler _updateHandler;
        private readonly IRepository<Hall> _repository;
        private readonly CancellationToken _token = new CancellationToken();
        private readonly AppDbContext _appDbContext;
        public HallsUseCaseUnitTests()
        {
            var _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _appDbContext = new AppDbContext(_options);

            _repository = new HallRepository(_appDbContext);
            _createHandler = new CreateHallHandler(_repository);
            _updateHandler = new UpdateHallHandler(_repository);
        }

        [Fact]
        public async Task AddHall_ShouldReturnCorrectHall()
        {
            // Arrange
            var hallName = "Hall 1";
            CreateHallComand comand = new CreateHallComand(hallName);

            // Act
            var newHallId = await _createHandler.Handle(comand, _token);
            var res = newHallId.Value;
            var result = await _repository.GetByIdAsync(res);

            //Assert
            Assert.Equal(hallName, result.Name);
        }

        [Fact]
        public async Task UpdateHall_ShouldReturnCorrectHall()
        {
            // Arrange

            Hall hall = new Hall("Hall 1", HallStatus.InOperation);
            var insertedHall = await _repository.AddAsync(hall);
            await _repository.SaveChangesAsync();
            UpdateHallComand comand = new UpdateHallComand(insertedHall.Id, HallStatus.OutOfOperation);

            // Act
            var newHallId = await _updateHandler.Handle(comand, _token);

            var result = await _repository.GetByIdAsync(insertedHall.Id);

            //Assert
            Assert.Equal(result.Status, HallStatus.OutOfOperation);
        }
    }
}
