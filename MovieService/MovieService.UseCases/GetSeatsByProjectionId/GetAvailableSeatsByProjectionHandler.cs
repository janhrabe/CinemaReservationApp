using MediatR;
using MovieService.CinemaClient;
using MovieService.Client;
using MovieService.Core.Interfaces;


namespace MovieService.UseCases.GetSeatsByProjectionId
{
    public class GetAvailableSeatsByProjectionHandler(IProjectionRepository projectionRepository, IReservationClient reservationServiceClient, ISeatsClient cinemaServiceClient) : IRequestHandler<GetAvailableSeatsByProjectionCommand, List<Guid>>
    {
        private readonly IProjectionRepository _projectionRepository = projectionRepository;
        private readonly IReservationClient _reservationServiceClient = reservationServiceClient;
        private readonly ISeatsClient _cinemaServiceClient = cinemaServiceClient;

        public async Task<List<Guid>> Handle(GetAvailableSeatsByProjectionCommand request, CancellationToken cancellationToken)
        {


            var projection = await _projectionRepository.GetByIdAsync(request.projectionId, cancellationToken) ?? throw new KeyNotFoundException("Projection not found");

            var seats = await _cinemaServiceClient.CinemaServiceAPISeatGetByHallIdGetByHallIdAsync(projection.HallId, cancellationToken);

            var occupiedSeats = await _reservationServiceClient.RegistrationServiceAPIReservationGetOccupiedSeatsProjectionIdGetOccupiedSeatsProjectionIdAsync(request.projectionId, cancellationToken);

            var availableSeats = seats.Where(seat => !occupiedSeats.Contains(seat)).ToList();

            return availableSeats;
        }
    }
}
