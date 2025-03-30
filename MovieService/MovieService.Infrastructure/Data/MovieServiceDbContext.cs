using Microsoft.EntityFrameworkCore;
using MovieService.Core.Entities;

namespace MovieService.Infrastructure.Data
{
    public class MovieServiceDbContext : DbContext
    {
        public MovieServiceDbContext(DbContextOptions<MovieServiceDbContext> options) : base(options)
        { }

        public DbSet<MovieEntity> Movies => Set<MovieEntity>();
        public DbSet<ProjectionEntity> Projections => Set<ProjectionEntity>();
    }
}