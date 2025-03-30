using Seat = RegistrationService.Core.Entities.Seat;

namespace RegistrationService.Infrastructure.Data.Config
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.Property(x => x.Number).IsRequired();
        }
    }
}
