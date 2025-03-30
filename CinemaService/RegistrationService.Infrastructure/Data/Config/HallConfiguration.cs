using Hall = RegistrationService.Core.Entities.Hall;

namespace RegistrationService.Infrastructure.Data.Config
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(DataConstants.DEFAULT_NAME_LENGTH);
        }
    }
}
