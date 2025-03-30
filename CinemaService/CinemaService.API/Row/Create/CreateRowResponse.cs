namespace CinemaService.API.Row.Create
{
    public class CreateRowResponse(int number, Guid hallId)
    {
        public int Number { get; set; } = number;
        public Guid HallId { get; set; } = hallId;

    }
}