
GO
CREATE TABLE OrganizationInfo
(
	OrganizationInfoId int not null Identity(1,1) Constraint OrganizationInfo_pk Primary Key,
	OrganizationName nvarchar(100) not null,
	AppName nvarchar(20) not null,
	Address nvarchar(200) not null,
	ContactNumber1 nvarchar(20) not null,
	ContactNumber2 nvarchar(20) null,
	TelephoneNumber nvarchar(20) null,
	EmailAddress nvarchar(200) null,
	FaxNumber nvarchar(100) null,
	POBoxNumber nvarchar(200) null,
	Logo image null
);
GO


GO
CREATE TABLE Language
(
	LanguageId int not null Identity(1,1) Constraint Language_pk Primary Key,
	LanguageName nvarchar(30) not null,
	Code nvarchar(10) not null,
	Status bit null default(1)
);
GO

GO
CREATE UNIQUE INDEX Language_LanguageName_ui ON
Language(LanguageName);
GO


GO
CREATE UNIQUE INDEX Language_Code_ui ON
Language(Code)
GO


GO
CREATE TABLE Menus
(
	MenuId int not null Identity(1,1) Constraint Menus_pk Primary Key,
	ParentMenuId int null Constraint Menus_fk References Menus(MenuId),
	MenuNameEnglish nvarchar(40) not null,
	MenuNameNepali nvarchar(100) not null,
	CheckMenuName nvarchar(50) not null,
	MenuLink nvarchar(200) not null,
	MenuOrder int not null,
	MenuIcon nvarchar(max) null,
	Status bit null default(1)
);
GO


GO
CREATE UNIQUE INDEX Menus_MenuNameEnglish ON
Menus(MenuNameEnglish,ParentMenuId)
GO

GO
CREATE UNIQUE INDEX Menus_CheckMenuName_ui ON
Menus(CheckMenuName)
GO


GO
CREATE UNIQUE INDEX Menus_MenuLink_ui ON
Menus(MenuLink)
GO



GO
CREATE TABLE UserType
(
	UserTypeId int not null Identity(1,1)  Constraint UserType_pk Primary Key,
	UserTypeTitle nvarchar(50) not null,
	Status bit null default(1),
	CreatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX UserType_UserTypeTitle_ui ON
UserType(UserTypeTitle)
GO


GO
CREATE TABLE Gender
(
	GenderId int not null Identity(1,1)  Constraint Gender_pk Primary Key,
	GenderName nvarchar(50) not null,
	Status bit null default(1),
	CreatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Gender_GenderName_ui ON
Gender(GenderName)
GO


GO
CREATE TABLE Role
(
	RoleId int not null Identity(1,1)  Constraint Role_pk Primary Key,
	RoleName nvarchar(50) not null,
	Status bit null default(1),
	CreatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Role_RoleName_ui ON
Role(RoleName)
GO


GO
CREATE TABLE Designation
(
	DesignationId int not null Identity(1,1)  Constraint Designation_pk Primary Key,
	DesignationName nvarchar(50) not null,
	Status bit null default(1),
	CreatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Designation_DesignationName_ui ON
Designation(DesignationName)
GO


GO
CREATE TABLE Department
(
	DepartmentId int not null Identity(1,1)  Constraint Department_pk Primary Key,
	DepartmentName nvarchar(50) not null,
	Status bit null default(1),
	CreatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Department_DepartmentName_ui ON
Department(DepartmentName)
GO


GO
CREATE TABLE PhotoStorages
(
	PhotoStorageId int not null Identity(1,1) Constraint PhotoStorages_pk Primary Key,
	Photo image null,
	PhotoLocation nvarchar(max) null
);
GO

GO
CREATE TABLE Users
(
	UserId int not null Identity constraint Users_pk Primary Key,
	UserTypeId int not null Constraint Users_UserType_UserTypeId_fk References UserType(UserTypeId),
	PhotoStorageId int not null Constraint Users_PhotoStorages_PhotoStorageId_fk References PhotoStorages(PhotoStorageId),
	UserName nvarchar(50) not null,
	Password nvarchar(max) not null,
	EmailAddress nvarchar(200) not null,
	ContactNumber nvarchar(20) not null,
	Status bit null default(1),
	CreatedBy int null Constraint Users_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null default GETDATE(),
	UpdatedBy int null Constraint Users_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Users_UserName_ui ON
Users(UserName)
GO
GO
CREATE UNIQUE INDEX Users_EmailAddress_ui ON
Users(EmailAddress)
GO
GO
CREATE UNIQUE INDEX Users_ContactNumber_ui ON
Users(ContactNumber)
GO

GO
CREATE TABLE Staffs
(
	StaffId int not null Identity(1,1) Constraint Staffs_pk Primary Key,
	RoleId int not null Constraint Staffs_Role_RoleId_fk References Role(RoleId),
	DesignationId int not null Constraint Staffs_Designation_DesignationId_fk References Designation(DesignationId),
	DepartmentId int null Constraint Staffs_Department_DepartmentId_fk References Department(DepartmentId),
	StaffName nvarchar(200) not null,
	GenderId int not null Constraint Staffs_Gender_GenderId_fk References Staffs(StaffId),
	TemporaryAddress nvarchar(200) not null,
	PermanentAddress nvarchar(200) not null,
	CitizenshipNumber nvarchar(150) null,
	PanNumber nvarchar(150) null,
	BasicSalary float not null,
	Status bit null default(1),
	CreatedBy int null Constraint Users_Staffs_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null  default GETDATE(),
	UpdatedBy int null Constraint Users_Staffs_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Staffs_CitizenshipNumber_ui ON
Staffs(CitizenshipNumber) WHERE CitizenshipNumber IS NOT NULL
GO

GO
CREATE UNIQUE INDEX Staffs_PanNumber_ui ON
Staffs(PanNumber) WHERE PanNumber IS NOT NULL
GO

GO
CREATE TABLE UserDocuments
(
	UserDocumentId int not null Identity(1,1) Constraint UserDocuments_pk Primary Key,
	StaffId int not null Constraint UserDocuments_Staffs_StaffId References Staffs(StaffId),
	CitizenshipFront nvarchar(500) null,
	CitizenshipBack nvarchar(500) null,
	PanCard nvarchar(500) null,
	EducationalDocument nvarchar(max) null
);
GO


GO
CREATE TABLE BankAccount
(
	BankAccountId int not null Identity(1,1) Constraint BankAccount_pk Primary Key,
	StaffId int not null Constraint BankAccount_Staffs_StaffId References Staffs(StaffId),
	AccountNumber nvarchar(200) null,
	BankName nvarchar(500) null,
	PanCard nvarchar(500) null,
	EducationalDocument nvarchar(max) null
);
GO

GO
CREATE TABLE Units
(
	UnitId int not null Identity(1,1) Constraint Units_pk Primary Key,
	UnitName nvarchar(200) not null,
	Status bit null default(1),
	CreatedBy int null Constraint Users_Units_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null,
	UpdatedBy int null Constraint Users_Units_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Units_UnitName_ui ON
Units(UnitName)
GO

GO
CREATE TABLE Product
(
	ProductId int not null Identity(1,1) Constraint Product_pk Primary Key,
	ParentProductId int null Constraint Product_ParentProductId_fk References Product(ProductId),
	ProductName nvarchar(200) not null,
	ProductCode nvarchar(50) not null,
	Status bit null default(1),
	CreatedBy int null Constraint Users_Product_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null,
	UpdatedBy int null Constraint Users_Product_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO
CREATE UNIQUE INDEX Product_ProductName_ui ON
Product(ProductName,ParentProductId)
GO
GO
CREATE UNIQUE INDEX Product_ProductCode_ui ON
Product(ProductCode)
GO

GO
CREATE TABLE ProductPrice
(
	ProductPriceId int not null Identity(1,1) Constraint ProductPrice_pk Primary Key,
	ProductId int not null Constraint ProductPrice_Product_ProductId_fk References Product(ProductId),
	UnitId int not null Constraint ProductPrice_Unit_UnitId_fk References Units(UnitId),
	SellingPrice float not null,
	Status bit null default(1),
	UpdatedDate datetime not null
);
GO


GO
CREATE TABLE MenuAccessPermission
(
	MenuAccessPermissionId int not null Identity(1,1) Constraint MenuAccessPermission_pk Primary Key,
	MenuId int not null Constraint MenuAccessPermission_Menus_MenuId_fk References Menus(MenuId),
	StaffId int not null Constraint MenuAccessPermission_Staffs_StaffId_fk References Staffs(StaffId),
	ReadAccess bit null Default(0),
	WriteAccess bit null Default(0),
	ModifyAccess bit null Default(0),
	DeleteAccess bit null Default(0),
	AdminAccess bit null Default(0),
);
GO
GO
CREATE UNIQUE INDEX MenuAccessPermission_MenuId_StaffId ON
MenuAccessPermission(MenuId,StaffId)
GO
