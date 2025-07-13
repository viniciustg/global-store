﻿namespace GlobalStore.Functions.Models
{
    public class Product
    {
        public int Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
