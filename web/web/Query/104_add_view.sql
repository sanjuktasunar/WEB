
GO
CREATE VIEW DisplayProductView
AS
SELECT A.*,
B.ProductName AS ParentProductName,
B.ProductNameNepali AS ParentProductNameNepali,
C.Photo
FROM Product AS A
JOIN Product AS B ON B.ProductId=A.ParentProductId
JOIN ProductImage AS C ON C.ProductId=A.ProductId AND C.IsPrimary=1
GO

GO
CREATE OR ALTER PROC [dbo].[Sp_SearchProductForDisplay]
(
	@query nvarchar(50)
)
AS
BEGIN
	SELECT * FROM DisplayProductView
	WHERE 
	(
		(ISNULL(@query,'')='') OR ((UPPER(TRIM(ProductName)) LIKE '%'+UPPER(@query)+'%')) OR
		((UPPER(TRIM(ProductNameNepali)) LIKE '%'+UPPER(@query)+'%')) OR
		((UPPER(TRIM(ParentProductName)) LIKE '%'+UPPER(@query)+'%')) OR
		((UPPER(TRIM(ParentProductNameNepali)) LIKE '%'+UPPER(@query)+'%')) OR
		((UPPER(TRIM(ProductCode)) LIKE '%'+UPPER(@query)+'%'))
	)
END
GO

