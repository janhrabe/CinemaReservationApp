namespace CinemaService.API.Seat.Create
{
    public class CreateSeatResponse(int number, Guid rowId)
    {
        public int Number { get; set; } = number;
        public Guid RowId { get; set; } = rowId;
    }
}