using Marten;
using MediatR;
using MovieService.CinemaClient;
using MovieService.Core;
using MovieService.Core.Entities;

namespace MovieService.UseCases.Projection.Create
{
    public class CreateProjectionHandler
        : IRequestHandler<CreateProjectionCommand, Result<Guid>>
    {
        private readonly IDocumentSession _session;
        private readonly IHallsClient _hallsClient;

        public CreateProjectionHandler(IDocumentSession session, IHallsClient hallsClient)
        {
            _session = session;
            _hallsClient = hallsClient;
        }

        public async Task<Result<Guid>> Handle(CreateProjectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var hallId = request.HallId.ToString();

                var hallExists = await CheckHallExists(hallId, cancellationToken);
                if (!hallExists)
                {
                    return Result<Guid>.Failure("Hall not Found");
                }

                var projection = new ProjectionEntity(request.StartTime, request.MovieId, request.HallId);

                _session.Store(projection);
                await _session.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(projection.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure($"An error occurred: {ex.Message}");
            }
        }

        private async Task<bool> CheckHallExists(string hallId, CancellationToken cancellationToken)
        {
            try
            {

                await _hallsClient.CinemaServiceAPIHallsGetHallByIdGetHallByIdAsync(hallId, cancellationToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}