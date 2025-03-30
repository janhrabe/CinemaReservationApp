using System.Data;
using Marten;
using Moq;
using MovieService.CinemaClient;
using MovieService.Client;
using MovieService.Core.Entities;
using MovieService.Core.Interfaces;
using MovieService.Infrastructure.Repositories;
using MovieService.UseCases.GetSeatsByProjectionId;
using MovieService.UseCases.Projection.Create;

namespace MovieService.Infrastructure.Tests
{
    public class ProjectionRepositoryTests
    {
        private readonly Mock<IDocumentStore> _mockStore;
        private readonly Mock<IDocumentSession> _mockSession;
        private readonly Repository<ProjectionEntity> _repository;
        private readonly Mock<ISeatsClient> _cinemaServiceClient;
        private readonly Mock<IHallsClient> _hallsClient;
        private readonly Mock<IReservationClient> _reservationServiceClient;
        private readonly GetAvailableSeatsByProjectionHandler handler;
        private readonly Mock<IProjectionRepository> _projectionRepository;

        public ProjectionRepositoryTests()
        {
            _mockStore = new Mock<IDocumentStore>();
            _mockSession = new Mock<IDocumentSession>();
            _mockStore.Setup(store => store.LightweightSession(It.IsAny<IsolationLevel>())).Returns(_mockSession.Object);
            _repository = new Repository<ProjectionEntity>(_mockStore.Object);

            _cinemaServiceClient = new Mock<ISeatsClient>();
            _hallsClient = new Mock<IHallsClient>();
            _reservationServiceClient = new Mock<IReservationClient>(MockBehavior.Strict);
            _projectionRepository = new Mock<IProjectionRepository>();

            handler = new GetAvailableSeatsByProjectionHandler(
                _projectionRepository.Object,
                _reservationServiceClient.Object,
                _cinemaServiceClient.Object
                );
        }

        [Fact]
        public async Task AddAsync_ShouldStoreProjectionEntity()
        {
            // Arrange
            var projection = new ProjectionEntity(
                startTime: DateTime.Now.AddHours(2),
                hallId: Guid.NewGuid(),
                movieId: Guid.NewGuid()
            );
            var cancellationToken = CancellationToken.None;

            // Act
            await _repository.AddAsync(projection, cancellationToken);

            // Assert
            _mockSession.Verify(session => session.Store(projection), Times.Once);
            _mockSession.Verify(session => session.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteProjectionEntity()
        {
            // Arrange
            var projectionId = Guid.NewGuid();
            var projection = new ProjectionEntity(
                startTime: DateTime.Now.AddHours(2),
                hallId: Guid.NewGuid(),
                movieId: Guid.NewGuid()
            );

            var cancellationToken = CancellationToken.None;

            _mockSession.Setup(session => session.LoadAsync<ProjectionEntity>(projectionId, cancellationToken)).ReturnsAsync(projection);

            // Act
            await _repository.DeleteAsync(projection, cancellationToken);

            // Assert
            _mockSession.Verify(session => session.Delete(projection), Times.Once);
            _mockSession.Verify(session => session.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProjectionEntity()
        {
            // Arrange
            var projectionId = Guid.NewGuid();
            var projection = new ProjectionEntity(
                startTime: DateTime.Now.AddHours(2),
                hallId: Guid.NewGuid(),
                movieId: Guid.NewGuid()
            );

            projection.Id = projectionId;

            var cancellationToken = CancellationToken.None;


            _mockSession.Setup(session => session.LoadAsync<ProjectionEntity>(projectionId, cancellationToken))
                        .ReturnsAsync(projection);

            // Act
            var result = await _repository.GetByIdAsync(projectionId, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(projectionId, result?.Id);
        }

        [Fact]
        public async Task Handle_ShouldReturnProjectionId_WhenProjectionIsCreated()
        {
            // Arrange
            var hallId = Guid.NewGuid();
            var movieId = Guid.NewGuid();
            var startTime = DateTime.Now.AddDays(2);

            var handler = new CreateProjectionHandler(_mockSession.Object, _hallsClient.Object);
            var command = new CreateProjectionCommand(startTime, movieId, hallId);


            _hallsClient.Setup(client => client.CinemaServiceAPIHallsGetHallByIdGetHallByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEqual(Guid.Empty, result.Value);
            _mockSession.Verify(session => session.Store(It.IsAny<ProjectionEntity>()), Times.Once);
            _mockSession.Verify(session => session.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}


