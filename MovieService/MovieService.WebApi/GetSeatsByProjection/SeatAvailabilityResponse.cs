namespace MovieService.WebApi.GetSeatsByProjection
{
    public class SeatAvailabilityResponse
    {
        public Guid SeatId { get; set; }
        public int SeatNumber { get; set; }

        public int RowNumber { get; set; }

        public bool IsOccupied { get; set; }
    }
}