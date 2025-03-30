namespace CinemaService.API.Rows.GetById
{
    public class GetByIdResponse(int number, Guid rowId)
    {
        public int Number { get; set; } = number;
        public Guid HallId { get; set; } = rowId;
    }
}