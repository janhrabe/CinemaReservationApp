namespace CinemaService.API.Rows.GetById
{
    public class GetByIdRequest
    {
        public const string Route = "/Rows/GetById";
        public Guid Id { get; set; }

    }
}