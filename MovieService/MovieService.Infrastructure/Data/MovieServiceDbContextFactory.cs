using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MovieService.Infrastructure.Data
{
    public class MovieServiceDbContextFactory : IDesignTimeDbContextFactory<MovieServiceDbContext>, IDbContextFactory<MovieServiceDbContext>
    {
        public MovieServiceDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MovieServiceDbContext>();

            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=MovieServiceDb;Username=postgres;Password=root");

            return new MovieServiceDbContext(optionsBuilder.Options);

        }

        public MovieServiceDbContext CreateDbContext(string[] args)
        {
            return CreateDbContext();
        }
    }
}
