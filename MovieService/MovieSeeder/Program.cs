using JasperFx;
using Marten;
using MovieService.Core.Entities;

var store = DocumentStore.For(opts =>
{
    opts.Connection("Server=localhost;Port=5432;Database=MovieServiceDb;Username=postgres;Password=root");
    opts.AutoCreateSchemaObjects = AutoCreate.All;
});

using (var cleanUpSession = store.LightweightSession())
{
    cleanUpSession.DeleteWhere<MovieEntity>(x => true);
    cleanUpSession.DeleteWhere<ProjectionEntity>(x => true);

    await cleanUpSession.SaveChangesAsync();
}

using var session = store.LightweightSession();

var existingCount = await session.Query<MovieEntity>().CountAsync();

var allMovies = await session.Query<MovieEntity>().ToListAsync();
var allProjections = await session.Query<ProjectionEntity>().ToListAsync();


await session.SaveChangesAsync();


if (existingCount > 0)
{
    Console.WriteLine($"Databáze již obsahuje {existingCount} filmů. Seeding se přeskočí.");
    return;
}

Console.WriteLine("Seeduji nové filmy...");


var shawshank = new MovieEntity
{
    Title = "The Shawshank Redemption",
    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
    Director = "Frank Darabont",
    Cast = "Tim Robbins, Morgan Freeman",
    DurationInMinutes = 142,
    IsPlaying = true,
};

var godfather = new MovieEntity
{
    Title = "The Godfather",
    Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
    Director = "Francis Ford Coppola",
    Cast = "Marlon Brando, Al Pacino",
    DurationInMinutes = 175,
    IsPlaying = true,
};

var darkKnight = new MovieEntity
{
    Title = "The Dark Knight",
    Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
    Director = "Christopher Nolan",
    Cast = "Christian Bale, Heath Ledger",
    DurationInMinutes = 152,
    IsPlaying = true,
};

var inception = new MovieEntity
{
    Title = "Inception",
    Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
    Director = "Christopher Nolan",
    Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt",
    DurationInMinutes = 148,
    IsPlaying = true,
};

session.Store(shawshank, godfather, darkKnight, inception);
await session.SaveChangesAsync();
Console.WriteLine("✅ Filmy uloženy.");

var additionalMovies = new List<MovieEntity>
{
    new MovieEntity { Title = "Madame Web", Description = "A paramedic discovers she has psychic abilities.", Director = "S.J. Clarkson", Cast = "Dakota Johnson, Sydney Sweeney", DurationInMinutes = 116, IsPlaying = false },
    new MovieEntity { Title = "Argylle", Description = "A spy novelist gets pulled into the real world of espionage.", Director = "Matthew Vaughn", Cast = "Henry Cavill, Bryce Dallas Howard", DurationInMinutes = 139, IsPlaying = false },
    new MovieEntity { Title = "Bob Marley: One Love", Description = "A biopic celebrating Bob Marley's life and music.", Director = "Reinaldo Marcus Green", Cast = "Kingsley Ben-Adir, Lashana Lynch", DurationInMinutes = 104, IsPlaying = false },
    new MovieEntity { Title = "Deadpool & Wolverine", Description = "Multiverse chaos starring Deadpool and Wolverine.", Director = "Shawn Levy", Cast = "Ryan Reynolds, Hugh Jackman", DurationInMinutes = 128, IsPlaying = false },
    new MovieEntity { Title = "Inside Out 2", Description = "Riley faces new emotions in her teenage years.", Director = "Kelsey Mann", Cast = "Amy Poehler, Maya Hawke", DurationInMinutes = 100, IsPlaying = false },
    new MovieEntity { Title = "Kingdom of the Planet of the Apes", Description = "Post-Caesar ape society faces new challenges.", Director = "Wes Ball", Cast = "Owen Teague, Freya Allan", DurationInMinutes = 140, IsPlaying = false },
    new MovieEntity { Title = "IF", Description = "A girl who sees imaginary friends embarks on an adventure.", Director = "John Krasinski", Cast = "Ryan Reynolds, Cailey Fleming", DurationInMinutes = 97, IsPlaying = false },
    new MovieEntity { Title = "Despicable Me 4", Description = "Gru takes on new villain with his minions.", Director = "Chris Renaud", Cast = "Steve Carell, Kristen Wiig", DurationInMinutes = 95, IsPlaying = false },
    new MovieEntity { Title = "Beetlejuice Beetlejuice", Description = "Sequel to the cult classic supernatural comedy.", Director = "Tim Burton", Cast = "Michael Keaton, Jenna Ortega", DurationInMinutes = 108, IsPlaying = false },
    new MovieEntity { Title = "Venom: The Last Dance", Description = "Venom’s final chapter.", Director = "Kelly Marcel", Cast = "Tom Hardy", DurationInMinutes = 110, IsPlaying = false },
    new MovieEntity { Title = "Kraven the Hunter", Description = "Marvel antihero origin story.", Director = "J.C. Chandor", Cast = "Aaron Taylor-Johnson", DurationInMinutes = 118, IsPlaying = false },
    new MovieEntity { Title = "Joker: Folie à Deux", Description = "Sequel with musical elements featuring Harley Quinn.", Director = "Todd Phillips", Cast = "Joaquin Phoenix, Lady Gaga", DurationInMinutes = 128, IsPlaying = false },
    new MovieEntity { Title = "A Quiet Place: Day One", Description = "Origins of the alien invasion in NYC.", Director = "Michael Sarnoski", Cast = "Lupita Nyong’o", DurationInMinutes = 98, IsPlaying = false },
    new MovieEntity { Title = "Mufasa: The Lion King", Description = "Prequel to The Lion King.", Director = "Barry Jenkins", Cast = "Aaron Pierre, Kelvin Harrison Jr.", DurationInMinutes = 102, IsPlaying = false },
    new MovieEntity { Title = "The Garfield Movie", Description = "Animated reboot of Garfield’s misadventures.", Director = "Mark Dindal", Cast = "Chris Pratt", DurationInMinutes = 93, IsPlaying = false },
    new MovieEntity { Title = "Paddington in Peru", Description = "Paddington returns to Peru for a family adventure.", Director = "Dougal Wilson", Cast = "Ben Whishaw", DurationInMinutes = 96, IsPlaying = false },
    new MovieEntity { Title = "Transformers One", Description = "Origin story of Optimus Prime and Megatron.", Director = "Josh Cooley", Cast = "Chris Hemsworth", DurationInMinutes = 105, IsPlaying = false },
    new MovieEntity { Title = "Twisters", Description = "Spiritual sequel to the 1996 disaster classic.", Director = "Lee Isaac Chung", Cast = "Daisy Edgar-Jones, Glen Powell", DurationInMinutes = 107, IsPlaying = false },
    new MovieEntity { Title = "The Marvels", Description = "Captain Marvel teams up with Monica Rambeau and Ms. Marvel.", Director = "Nia DaCosta", Cast = "Brie Larson, Iman Vellani", DurationInMinutes = 110, IsPlaying = false },
    new MovieEntity { Title = "The Hunger Games: The Ballad of Songbirds & Snakes", Description = "Prequel to Hunger Games series.", Director = "Francis Lawrence", Cast = "Rachel Zegler, Tom Blyth", DurationInMinutes = 157, IsPlaying = false },
    new MovieEntity { Title = "Wonka", Description = "Origin story of Willy Wonka.", Director = "Paul King", Cast = "Timothée Chalamet", DurationInMinutes = 112, IsPlaying = false },
    new MovieEntity { Title = "Oppenheimer", Description = "Biography of physicist J. Robert Oppenheimer.", Director = "Christopher Nolan", Cast = "Cillian Murphy, Emily Blunt", DurationInMinutes = 180, IsPlaying = false },
    new MovieEntity { Title = "Barbie", Description = "Barbie explores the real world.", Director = "Greta Gerwig", Cast = "Margot Robbie, Ryan Gosling", DurationInMinutes = 114, IsPlaying = false },
    new MovieEntity { Title = "Mission: Impossible – Dead Reckoning Part One", Description = "Ethan Hunt faces new threats.", Director = "Christopher McQuarrie", Cast = "Tom Cruise", DurationInMinutes = 163, IsPlaying = false },
    new MovieEntity { Title = "Napoleon", Description = "Epic saga of Napoleon Bonaparte.", Director = "Ridley Scott", Cast = "Joaquin Phoenix, Vanessa Kirby", DurationInMinutes = 157, IsPlaying = false },
    new MovieEntity { Title = "The Marvels", Description = "Marvel team-up with cosmic consequences.", Director = "Nia DaCosta", Cast = "Brie Larson, Iman Vellani", DurationInMinutes = 105, IsPlaying = false },
    new MovieEntity { Title = "Wish", Description = "Disney's 100th anniversary film about wishes coming true.", Director = "Chris Buck", Cast = "Ariana DeBose", DurationInMinutes = 92, IsPlaying = false },
    new MovieEntity { Title = "Elemental", Description = "A city where fire, water, land and air residents coexist.", Director = "Peter Sohn", Cast = "Leah Lewis, Mamoudou Athie", DurationInMinutes = 101, IsPlaying = false },
    new MovieEntity { Title = "The Creator", Description = "Sci-fi war between humans and AI.", Director = "Gareth Edwards", Cast = "John David Washington", DurationInMinutes = 133, IsPlaying = false },
    new MovieEntity { Title = "The Holdovers", Description = "A grumpy teacher bonds with a student during the holidays.", Director = "Alexander Payne", Cast = "Paul Giamatti", DurationInMinutes = 133, IsPlaying = false },
    new MovieEntity { Title = "Next Goal Wins", Description = "A sports comedy based on the true story of American Samoa's football team.", Director = "Taika Waititi", Cast = "Michael Fassbender", DurationInMinutes = 104, IsPlaying = false },
    new MovieEntity { Title = "Aquaman and the Lost Kingdom", Description = "Arthur must forge uneasy alliances to protect Atlantis.", Director = "James Wan", Cast = "Jason Momoa, Patrick Wilson", DurationInMinutes = 124, IsPlaying = false },
    new MovieEntity { Title = "Migration", Description = "A duck family heads south for the winter.", Director = "Benjamin Renner", Cast = "Kumail Nanjiani, Elizabeth Banks", DurationInMinutes = 90, IsPlaying = false },
    new MovieEntity { Title = "The Iron Claw", Description = "Story of the Von Erich wrestling family.", Director = "Sean Durkin", Cast = "Zac Efron, Jeremy Allen White", DurationInMinutes = 132, IsPlaying = false },
    new MovieEntity { Title = "Poor Things", Description = "A woman brought back to life explores freedom.", Director = "Yorgos Lanthimos", Cast = "Emma Stone, Mark Ruffalo", DurationInMinutes = 141, IsPlaying = false },
    new MovieEntity { Title = "The Color Purple", Description = "A bold reimagining of the classic tale.", Director = "Blitz Bazawule", Cast = "Fantasia Barrino, Taraji P. Henson", DurationInMinutes = 141, IsPlaying = false }
};

// Ulož všech 36 najednou
session.Store(additionalMovies.ToArray());
await session.SaveChangesAsync();

Console.WriteLine("🎞 36 dalších filmů bylo uloženo.");


var movieIds = new[] { shawshank.Id, godfather.Id, darkKnight.Id, inception.Id };

var halls = new Dictionary<string, Guid>
{
    { "Sál 1", Guid.Parse("B0116D00-E611-43F7-EB3B-08DD69DD2B2A") },
    { "Sál 2", Guid.Parse("7B985A51-25F5-4333-EB3C-08DD69DD2B2A") },
    { "Sál 3", Guid.Parse("2E5380A6-95F5-4903-EB3D-08DD69DD2B2A") }
};

Console.WriteLine("🕒 Generuji projekce...");

Console.WriteLine("🕒 Generuji projekce...");

var random = new Random();
var days = Enumerable.Range(1, 6)
                     .Select(offset => DateTime.Today.AddDays(offset))
                     .ToList();

foreach (var movieId in movieIds)
{
    foreach (var hall in halls)
    {
        foreach (var day in days)
        {
            int projectionCount = random.Next(2, 5); // 2 až 4 promítání denně

            var usedTimes = new HashSet<TimeSpan>();

            for (int i = 0; i < projectionCount; i++)
            {
                TimeSpan time;
                do
                {
                    var hour = random.Next(13, 22); // od 13:00 do 21:00
                    var minute = random.Next(0, 4) * 15; // 0, 15, 30, 45
                    time = new TimeSpan(hour, minute, 0);
                }
                while (!usedTimes.Add(time));

                var dateTime = day.Add(time);

                var projection = new ProjectionEntity(dateTime, movieId, hall.Value);
                session.Store(projection);
            }
        }
    }
}

await session.SaveChangesAsync();
Console.WriteLine("🎬 Projekce vytvořeny.");

await session.SaveChangesAsync();
