namespace GlobalStore.Domain.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Store> Stores { get; set; } = [];
    }
}
