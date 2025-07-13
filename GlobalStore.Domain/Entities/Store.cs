namespace GlobalStore.Domain.Entities
{
    public class Store
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
