using Row = RegistrationService.Core.Entities.Row;

namespace RegistrationService.Infrastructure.Data.Config
{
    public class RowConfiguration : IEntityTypeConfiguration<Row>
    {
        public void Configure(EntityTypeBuilder<Row> builder)
        {
            builder.Property(x => x.Number).IsRequired();
        }
    }
}
