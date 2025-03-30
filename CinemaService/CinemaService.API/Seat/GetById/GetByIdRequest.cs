namespace CinemaService.API.Seat.GetById
{
    public class GetByIdRequest
    {
        public const string Route = "/Seats/GetById/{Id}";
        public static string BuildRoute(Guid Id) => Route.Replace("{Id}", Id.ToString());
        public Guid Id { get; set; }

    }
}