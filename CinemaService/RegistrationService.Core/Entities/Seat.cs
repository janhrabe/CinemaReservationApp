using Ardalis.GuardClauses;

namespace RegistrationService.Core.Entities
{
    public class Seat : BaseEntity
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid RowId { get; set; }
        public Seat()
        {

        }
        public Seat(int number, Guid rowId)
        {
            Number = number;
            RowId = rowId;
        }
        public void UpdateSeat(int number, Guid rowId)
        {
            Number = Guard.Against.NegativeOrZero(number, nameof(number));
            RowId = Guard.Against.NullOrEmpty(rowId, nameof(rowId));
        }
    }
}
