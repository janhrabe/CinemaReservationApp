using MovieService.WebApi.Clients;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient http = new HttpClient();
            CinemaHttpClient client = new CinemaHttpClient(http);

            var result = await client.GetAllSeats(Guid.Parse("B0116D00-E611-43F7-EB3B-08DD69DD2B2A"));

            foreach (var seat in result)
            {
                Console.WriteLine(seat.Item1 + " " + seat.Item2);
            }
            Console.WriteLine("Konec");
        }
    }
}
