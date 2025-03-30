
using System.Data;
using Marten;
using Moq;
using MovieService.Core.Entities;
using MovieService.Infrastructure.Repositories;

namespace MovieService.Infrastructure.Tests
{
    public class MovieRepositoryTests
    {
        private readonly Mock<IDocumentStore> _mockStore;
        private readonly Mock<IDocumentSession> _mockSession;
        private readonly Repository<MovieEntity> _repository;

        public MovieRepositoryTests()
        {
            _mockStore = new Mock<IDocumentStore>();
            _mockSession = new Mock<IDocumentSession>();
            _mockStore.Setup(store => store.LightweightSession(It.IsAny<IsolationLevel>())).Returns(_mockSession.Object);
            _repository = new Repository<MovieEntity>(_mockStore.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldStoreMovieEntity()
        {
            // Arrange
            var movie = new MovieEntity()
            {
                Id = Guid.NewGuid(),
                Title = "Inception",
                Description = "A mind-bending thriller",
                Director = "Christopher Nolan",
                Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt",
                DurationInMinutes = 148,
                IsPlaying = true,
            };
            var cancellationToken = CancellationToken.None;

            _mockSession.Setup(session => session.Store(It.IsAny<MovieEntity>())).Verifiable();
            _mockSession.Setup(session => session.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _repository.AddAsync(movie, cancellationToken);

            // Assert
            _mockSession.Verify(session => session.Store(movie), Times.Once);
            _mockSession.Verify(session => session.SaveChangesAsync(cancellationToken), Times.Once);
        }


        [Fact]
        public async Task DeleteAsync_ShouldDeleteMovieEntity()
        {
            // Arrange
            var movie = new MovieEntity
            {
                Id = Guid.NewGuid(),
                Title = "Inception",
                Description = "A mind-bending thriller",
                Director = "Christopher Nolan",
                Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt",
                DurationInMinutes = 148,
                IsPlaying = true,
            };
            var cancellationToken = CancellationToken.None;

            _mockSession.Setup(session => session.LoadAsync<MovieEntity>(movie.Id, cancellationToken)).ReturnsAsync(movie);
            _mockSession.Setup(session => session.Delete(It.IsAny<MovieEntity>())).Verifiable();
            _mockSession.Setup(session => session.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _repository.DeleteAsync(movie, cancellationToken);

            // Assert
            _mockSession.Verify(session => session.Delete(movie), Times.Once);
            _mockSession.Verify(session => session.SaveChangesAsync(cancellationToken), Times.Once);
        }


        [Fact]
        public async Task GetByIdAsync_ShouldReturnMovieEntity()
        {
            // Arrange
            var movieId = Guid.NewGuid();
            var movie = new MovieEntity
            {
                Id = movieId,
                Title = "Inception",
                Description = "A mind-bending thriller",
                Director = "Christopher Nolan",
                Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt",
                DurationInMinutes = 148,
                IsPlaying = true,
            };
            var cancellationToken = CancellationToken.None;

            _mockSession.Setup(session => session.LoadAsync<MovieEntity>(movieId, cancellationToken)).ReturnsAsync(movie);

            // Act
            var result = await _repository.GetByIdAsync(movieId, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movieId, result?.Id);
            Assert.Equal("Inception", result?.Title);
        }





        [Fact]
        public async Task UpdateAsync_ShouldStoreUpdatedMovieEntity()
        {
            // Arrange
            var movie = new MovieEntity
            {
                Id = Guid.NewGuid(),
                Title = "Inception",
                Description = "A mind-bending thriller",
                Director = "Christopher Nolan",
                Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt",
                DurationInMinutes = 148,
                IsPlaying = true,
            };
            var cancellationToken = CancellationToken.None;

            // Act
            await _repository.UpdateAsync(movie, cancellationToken);

            // Assert
            _mockSession.Verify(session => session.Store(movie), Times.Once);
            _mockSession.Verify(session => session.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenStoreFails()
        {
            // Arrange
            var movie = new MovieEntity
            {
                Id = Guid.NewGuid(),
                Title = "Inception",
                Description = "A mind-bending thriller",
                Director = "Christopher Nolan",
                Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt",
                DurationInMinutes = 148,
                IsPlaying = true,
            };
            var cancellationToken = CancellationToken.None;

            _mockSession.Setup(session => session.Store(It.IsAny<MovieEntity>())).Throws(new Exception("Store failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _repository.AddAsync(movie, cancellationToken));
        }

    }
}
