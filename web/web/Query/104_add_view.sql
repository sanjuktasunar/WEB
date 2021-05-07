
GO
CREATE OR ALTER VIEW [dbo].[DisplayProductView]
AS
SELECT A.*,
B.ProductName AS ParentProductName,
B.ProductNameNepali AS ParentProductNameNepali,
C.Photo,D.SellingPrice
FROM Product AS A
JOIN Product AS B ON B.ProductId=A.ParentProductId
JOIN ProductImage AS C ON C.ProductId=A.ProductId AND C.IsPrimary=1
JOIN ProductPrice AS D ON D.ProductId=A.ProductId AND D.Status=1
GO
