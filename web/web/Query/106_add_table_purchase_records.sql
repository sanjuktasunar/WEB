
GO
CREATE TABLE Supplier
(
	SupplierId int not null Identity(1,1) Constraint Supplier_pk Primary Key,
	SupplierName nvarchar(500) not null,
	Address nvarchar(300),
	ContactNumber1 nvarchar(20) not null,
	ContactNumber2 nvarchar(20) null,
	EmailAddress nvarchar(200) null,
	Website nvarchar(500) null,
	PanNumber nvarchar(50) null,
	Status bit null default(1),
	CreatedBy int null Constraint Supplier_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null default GETDATE(),
	UpdatedBy int null Constraint Supplier_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO

GO
CREATE UNIQUE INDEX Supplier_SupplierName_ui ON
Supplier(SupplierName)
GO

GO
CREATE UNIQUE INDEX Supplier_ContactNumber1_ui ON
Supplier(ContactNumber1)
GO

GO
CREATE UNIQUE INDEX Supplier_EmailAddress_ui ON
Supplier(EmailAddress) WHERE EmailAddress IS NOT NULL
GO

GO
CREATE UNIQUE INDEX Supplier_Website_ui ON
Supplier(EmailAddress) WHERE Website IS NOT NULL
GO


GO
CREATE UNIQUE INDEX Supplier_PanNumber_ui ON
Supplier(PanNumber) WHERE PanNumber IS NOT NULL
GO






GO
CREATE TABLE PurchaseRecord
(
	PurchaseRecordId int not null Identity(1,1) Constraint PurchaseRecord_pk Primary Key,
	SupplierId int not null Constraint PurchaseRecord_Supplier_SupplierId_fk References Supplier(SupplierId),
	PurchaseDateAD datetime not null,
	PurchaseDateBS nvarchar(10) not null
)