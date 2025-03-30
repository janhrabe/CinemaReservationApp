namespace MovieService.WebApi.GetSeatsByProjection
{
    public class GetAvailableSeatsByProjectionRequest
    {
        public Guid ProjectionId { get; set; }

        public GetAvailableSeatsByProjectionRequest(Guid projectionId)
        {
            ProjectionId = projectionId;
        }
    }
}
