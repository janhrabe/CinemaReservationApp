namespace CinemaService.API.Row.GetByHallId
{
    public class GetByIdRequest
    {
        public const string Route = "/Rows/GetByHallId/{Id}";
        public static string BuildRoute(Guid Id) => Route.Replace("{Id}", Id.ToString());
        public Guid Id { get; set; }
    }
}