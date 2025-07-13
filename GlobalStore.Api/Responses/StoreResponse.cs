namespace GlobalStore.Api.Responses
{
    public class StoreResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
    }
}
