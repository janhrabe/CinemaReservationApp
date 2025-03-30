namespace CinemaService.API.Seat.GetSeatIdByHallId
{
    public class GetByHallIdRequest(Guid HallId)
    {
        public const string Route = "/Seats/GetByHallId/{Id}";
        public static string BuildRoute(Guid Id) => Route.Replace("{Id}", Id.ToString());
        public Guid Id { get; set; } = HallId;
    }
}