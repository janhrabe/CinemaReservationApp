namespace CinemaService.API.Seat.GetById
{
    public class GetByIdResponse(int number, Guid rowId)
    {
        public int Number { get; set; } = number;
        public Guid RowId { get; set; } = rowId;
    }
}