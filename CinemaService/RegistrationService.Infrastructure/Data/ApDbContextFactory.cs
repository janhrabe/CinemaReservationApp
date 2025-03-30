namespace RegistrationService.Infrastructure.Data
{
    public class ApDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>, IDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            return CreateDbContext();
        }

        public AppDbContext CreateDbContext()
        {
            //var configurationBuilder = new ConfigurationBuilder()
            //    .AddUserSecrets<ApDbContextFactory>()
            //    .Build();
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Cinema.CinemaService;Integrated Security=True;");

            return new AppDbContext(optionBuilder.Options);
        }
    }
}
