CREATE OR ALTER FUNCTION GetProductsAsJson()
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @result NVARCHAR(MAX);

    SELECT @result = (
        SELECT Id, CompanyId, StoreId, Name, Description, Price, CreatedAt, UpdatedAt
        FROM Products
        FOR JSON AUTO
    );

    RETURN @result;
END;