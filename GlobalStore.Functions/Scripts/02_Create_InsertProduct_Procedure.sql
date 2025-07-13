CREATE OR ALTER PROCEDURE InsertProduct
    @CompanyId UNIQUEIDENTIFIER,
    @StoreId UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @Description NVARCHAR(500),
    @Price DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Products (CompanyId, StoreId, Name, Description, Price)
    VALUES (@CompanyId, @StoreId, @Name, @Description, @Price);
END;