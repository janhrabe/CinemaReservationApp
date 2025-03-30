using Ardalis.GuardClauses;

namespace RegistrationService.Core.Entities
{
    public class Row : BaseEntity
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid HallId { get; set; }
        public Row()
        {

        }
        public Row(int number, Guid hallId)
        {
            Number = number;
            HallId = hallId;
        }
        public void UpdateRow(int number, Guid hallId)
        {
            Number = Guard.Against.NegativeOrZero(number, nameof(number));
            HallId = Guard.Against.NullOrEmpty(hallId, nameof(hallId));
        }
    }
}
