namespace RegistrationService.Core.Entities
{
    public class Hall : BaseEntity
    {
        public string Name { get; set; }
        public HallStatus Status { get; set; }
        public Hall()
        {

        }
        public Hall(string name, HallStatus status)
        {
            Name = name;
            Status = status;
        }
        public void UpdateHall(string name, HallStatus status)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
            Status = (HallStatus)Guard.Against.OutOfRange((int)status, nameof(status), 0, 1);
        }
    }
}
