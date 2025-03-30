namespace CinemaService.API.Halls.GetHallById
{
    public class GetHallByIdRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
