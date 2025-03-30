using System.Text.Json;

namespace MovieService.WebApi.Clients
{
    public class CinemaHttpClient
    {
        private readonly HttpClient _httpClient;

        public CinemaHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public CinemaHttpClient()
        {

        }

        private const string baseUrl = "https://localhost:7254/";
        private const string getAllHallSSeats = "Seats/by-hall/";

        public async Task<List<Tuple<int, int, Guid>>> GetAllSeats(Guid Id)
        {
            string path = baseUrl + getAllHallSSeats + Id.ToString();
            var response = await _httpClient.GetAsync(path);
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = JsonSerializer.Deserialize<List<Tuple<int, int, Guid>>>(content, options);

            return result;
        }

    }
    public class SeatAvailability
    {
        public int Seat { get; set; }
        public int Row { get; set; }
        public Guid SeatId { get; set; }
    }
}
