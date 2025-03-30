namespace RegistrationService.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Hall> Halls => Set<Hall>();
        public DbSet<Row> Rows => Set<Row>();
        public DbSet<Seat> Seats => Set<Seat>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.LogTo(result => System.Diagnostics.Trace.WriteLine(result), Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();




            optionsBuilder.UseSeeding((context, _) =>
            {
                var hall1 = new Hall() { Name = "Sál 1", Status = 0 };

                context.Set<Hall>().Add(hall1);
                context.SaveChanges();

                for (int i = 1; i <= 10; i++)
                {
                    var row = new Row() { HallId = hall1.Id, Number = i };

                    context.Set<Row>().Add(row);
                    context.SaveChanges();
                }

                var rows = context.Set<Row>().ToList();

                foreach (var row in rows)
                {
                    for (int i = 1; i <= 13; i++)
                    {
                        var seat = new Seat() { Number = i, RowId = row.Id };

                        context.Set<Seat>().Add(seat);
                        context.SaveChanges();
                    }
                }

                var hall2 = new Hall() { Name = "Sál 2", Status = 0 };

                context.Set<Hall>().Add(hall2);
                context.SaveChanges();

                for (int i = 1; i <= 12; i++)
                {
                    var row = new Row() { HallId = hall2.Id, Number = i };

                    context.Set<Row>().Add(row);
                    context.SaveChanges();
                }

                var rows2 = context.Set<Row>().ToList();

                foreach (var row in rows2)
                {
                    for (int i = 1; i <= 17; i++)
                    {
                        var seat = new Seat() { Number = i, RowId = row.Id };

                        context.Set<Seat>().Add(seat);
                        context.SaveChanges();
                    }
                }

                var hall3 = new Hall() { Name = "Sál 3", Status = 0 };

                context.Set<Hall>().Add(hall3);
                context.SaveChanges();

                for (int i = 1; i <= 9; i++)
                {
                    var row = new Row() { HallId = hall3.Id, Number = i };

                    context.Set<Row>().Add(row);
                    context.SaveChanges();
                }

                var rows3 = context.Set<Row>().ToList();

                foreach (var row in rows3)
                {
                    for (int i = 1; i <= 14; i++)
                    {
                        var seat = new Seat() { Number = i, RowId = row.Id };

                        context.Set<Seat>().Add(seat);
                        context.SaveChanges();
                    }
                }

            });
        }
    }
}
