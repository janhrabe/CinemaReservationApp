using MovieService.Core.Entities;

namespace MovieService.Core.Specifications
{
    public class GetProjectionsByMovieIdSpec : Specification<ProjectionEntity>
    {
        private readonly Guid _movieId;
        public GetProjectionsByMovieIdSpec(Guid movieId)
        {
            _movieId = movieId;

        }

        public override bool IsSatisfiedBy(ProjectionEntity entity)
        {
            return entity.MovieId == _movieId;
        }
    }
}
