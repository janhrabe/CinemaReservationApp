namespace MovieService.Core.Entities
{
    public class ProjectionEntity : BaseEntity
    {

        public DateTime StartTime { get; set; }

        public Guid MovieId { get; set; }

        public Guid HallId { get; set; }


        public ProjectionEntity()
        { }

        public ProjectionEntity(DateTime startTime, Guid movieId, Guid hallId)
        {
            StartTime = startTime;
            MovieId = movieId;
            HallId = hallId;
        }

        public ProjectionEntity(Guid id, DateTime startTime, Guid movieId, Guid hallId) : this(startTime, movieId, hallId)
        {
            Id = id;
        }


    }

}
