namespace CinemaService.API.Halls.Create
{
    public class CreateHallResponse(string email)
    {
        public string Name { get; set; } = email;
    }
}