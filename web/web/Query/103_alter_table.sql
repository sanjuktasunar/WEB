
GO
ALTER TABLE Unit
ADD UnitSymbol nvarchar(20) not null
GO

GO
CREATE UNIQUE INDEX Unit_UnitSymbol_ui ON
Unit(UnitSymbol)
GO


GO
ALTER TABLE Unit
ADD UnitSymbolNepali nvarchar(100) not null
GO


GO
CREATE UNIQUE INDEX Unit_UnitSymbolNepali_ui ON
Unit(UnitSymbolNepali)
GO


GO
ALTER TABLE Unit
ADD UnitNameNepali nvarchar(400) not null
GO


GO
CREATE UNIQUE INDEX Unit_UnitNameNepali_ui ON
Unit(UnitNameNepali)
GO

GO
ALTER TABLE ProductPrice
ADD CreatedBy int null Constraint ProductPrice_Users_CreatedBy_fk References Users(UserId)
GO

GO
ALTER TABLE ProductPrice
ADD UpdatedBy int null Constraint ProductPrice_Users_UpdatedBy_fk References Users(UserId)
GO


GO
CREATE UNIQUE INDEX ProductPrice_Product_Unit_Price_ui ON
ProductPrice(ProductId,UnitId,SellingPrice)
GO

--GO
--CREATE TABLE ProductCategory
--(
--	CategoryId int not null Identity(1,1) Constraint ProductCategory_pk Primary Key,
--	CategoryName nvarchar(200) not null,
--	CategoryNameNepali nvarchar(200) not null,
--	Status bit null default(1),
--	CreatedBy int null Constraint Users_ProductCategory_CreatedBy_fk References Users(UserId),
--	CreatedDate datetime not null,
--	UpdatedBy int null Constraint Users_ProductCategory_UpdatedBy_fk References Users(UserId),
--	UpdatedDate datetime null
--);
--GO
--GO
--CREATE UNIQUE INDEX ProductCategory_CategoryName_ui ON
--ProductCategory(CategoryName)
--GO
--GO
--CREATE UNIQUE INDEX ProductCategory_CategoryNameNepali_ui ON
--ProductCategory(CategoryNameNepali)
--GO

GO
ALTER TABLE Product
ADD ProductNameNepali nvarchar(400) not null
GO


GO
CREATE UNIQUE INDEX Product_ProductNameNepali_ui ON
Product(ProductName,ParentProductId)
GO


GO
CREATE VIEW [dbo].[ProductView]
AS
SELECT A.*,
B.ProductName AS ParentProductName,
B.ProductNameNepali AS ParentProductNameNepali
FROM Product AS A
LEFT JOIN Product AS B ON B.ProductId=A.ParentProductId
GO



