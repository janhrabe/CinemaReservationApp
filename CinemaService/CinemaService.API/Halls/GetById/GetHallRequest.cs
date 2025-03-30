namespace CinemaService.API.Halls.GetById
{
    public class GetHallRequest
    {
        public const string Route = "/Halls/GetById/{Id}";
        public static string BuildRoute(Guid Id) => Route.Replace("{Id}", Id.ToString());
        public Guid Id { get; set; }
    }
}