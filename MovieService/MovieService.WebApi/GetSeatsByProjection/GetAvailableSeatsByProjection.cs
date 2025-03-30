using FastEndpoints;
using MovieService.CinemaClient;
using MovieService.Client;
using MovieService.Core.Interfaces;
using MovieService.WebApi.Clients;

namespace MovieService.WebApi.GetSeatsByProjection
{
    public class GetAvailableSeatsByProjectionEndpoint : Endpoint<GetAvailableSeatsByProjectionRequest, List<SeatAvailabilityResponse>>
    {
        private readonly IProjectionRepository _projectionRepository;
        private readonly IReservationClient _reservationServiceClient;
        private readonly ISeatsClient _cinemaServiceClient;
        private readonly IRowsClient _rowsClient;
        private readonly CinemaHttpClient cinemaHttpClient;

        public GetAvailableSeatsByProjectionEndpoint(
            IProjectionRepository projectionRepository,
            IReservationClient reservationServiceClient,
            ISeatsClient cinemaServiceClient, IRowsClient rowsClient, CinemaHttpClient cinemaHttpClient)
        {
            _projectionRepository = projectionRepository;
            _reservationServiceClient = reservationServiceClient;
            _cinemaServiceClient = cinemaServiceClient;
            _rowsClient = rowsClient;
            this.cinemaHttpClient = cinemaHttpClient;
        }

        public override void Configure()
        {
            Get("/projections/seats/available-seats/{projectionId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAvailableSeatsByProjectionRequest req, CancellationToken ct)
        {
            var projection = await _projectionRepository.GetByIdAsync(req.ProjectionId, ct)
            ?? throw new KeyNotFoundException("Projection not found");

            if (projection.HallId == Guid.Empty)
                throw new InvalidOperationException("Projection.HallId is not set.");


            var allSeatsOfTheHall = await cinemaHttpClient.GetAllSeats(projection.HallId);

            var occupiedSeats = await _reservationServiceClient.RegistrationServiceAPIReservationGetOccupiedSeatsProjectionIdGetOccupiedSeatsProjectionIdAsync(req.ProjectionId, ct);

            var seatResponses = new List<SeatAvailabilityResponse>();
            foreach (var seatId in allSeatsOfTheHall)
            {
                seatResponses.Add(new SeatAvailabilityResponse
                {
                    SeatId = seatId.Item3,
                    SeatNumber = seatId.Item1,
                    RowNumber = seatId.Item2,
                    IsOccupied = occupiedSeats.Contains(seatId.Item3)
                });
            }

            await SendAsync(seatResponses);
        }
    }

}
