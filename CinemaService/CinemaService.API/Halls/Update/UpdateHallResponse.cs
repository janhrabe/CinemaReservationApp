using RegistrationService.Core.Entities;

namespace CinemaService.API.Halls.Update
{
    public class UpdateHallResponse(Guid id, HallStatus hallStatus)
    {
        public Guid Id { get; set; } = id;
        public HallStatus Rs { get; set; } = hallStatus;
    }
}