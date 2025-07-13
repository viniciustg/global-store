using GlobalStore.Functions.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace GlobalStore.Functions.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SqlConnection")!;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT * FROM Products WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Product
                {
                    Id = reader.GetInt32(0),
                    CompanyId = reader.GetGuid(1),
                    StoreId = reader.GetGuid(2),
                    Name = reader.GetString(3),
                    Description = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Price = reader.GetDecimal(5),
                    CreatedAt = reader.GetDateTime(6),
                    UpdatedAt = reader.IsDBNull(7) ? null : reader.GetDateTime(7)
                };
            }

            return null;
        }

        public async Task<string> GetProductsJsonAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT dbo.GetProductsAsJson()", conn);

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();

            return result?.ToString() ?? "{}";
        }

        public async Task InsertUsingProcedureAsync(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("dbo.InsertProduct", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CompanyId", product.CompanyId);
            cmd.Parameters.AddWithValue("@StoreId", product.StoreId);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", product.Price);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task InsertAsync(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Products (CompanyId, StoreId, Name, Description, Price)
                    VALUES (@CompanyId, @StoreId, @Name, @Description, @Price)";
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@CompanyId", product.CompanyId);
            cmd.Parameters.AddWithValue("@StoreId", product.StoreId);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", product.Price);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            var sql = @"UPDATE Products
                    SET Name = @Name, Description = @Description, Price = @Price, UpdatedAt = GETDATE()
                    WHERE Id = @Id";
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", product.Id);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", product.Price);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Products WHERE Id = @Id";
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
