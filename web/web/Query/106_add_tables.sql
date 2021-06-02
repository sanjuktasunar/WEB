
GO
CREATE TABLE AccountHead
(
	AccountHeadId int not null Identity(1,1) Constraint AccountHead_pk Primary Key,
	AccountHeadName nvarchar(350) not null,
	AccountHolderName nvarchar(150) not null,
	AccountNumber nvarchar(100) not null,
	Address nvarchar(150) not null,
	Status bit null default(1)
);
GO
GO
CREATE UNIQUE INDEX AccountHead_AccountNumber_ui ON
AccountHead(AccountNumber)
GO

GO
CREATE TABLE MunicipalityType
(
	Id int not null Identity(1,1) Constraint MunicipalityType_pk Primary Key,
	Name nvarchar(100) not null,
	NepaliName nvarchar(200) not null,
	Status bit null default(1)
);
GO
GO
CREATE UNIQUE INDEX MunicipalityType_Name_ui ON
MunicipalityType(Name)
GO

GO
CREATE UNIQUE INDEX MunicipalityType_NepaliName_ui ON
MunicipalityType(NepaliName)
GO


GO
CREATE TABLE MemberType
(
	Id int not null Identity(1,1) Constraint MemberType_pk Primary Key,
	Name nvarchar(100) not null,
	NepaliName nvarchar(200) not null,
	Status bit null default(1)
);
GO
GO
CREATE UNIQUE INDEX MemberType_Name_ui ON
MemberType(Name)
GO

GO
CREATE UNIQUE INDEX MemberType_NepaliName_ui ON
MemberType(NepaliName)
GO



GO
CREATE TABLE Member
(
	MemberId int not null Identity(1,1) Constraint Members_pk Primary Key,
	PhotoStorageId int not null Constraint Member_PhotoStorage_PhotoStorageId_fk References PhotoStorages(PhotoStorageId),
	MemberCode nvarchar(50) not null,
	FirstName nvarchar(50) not null,
	MiddleName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	MobileNumber nvarchar(20) not null,
	Email nvarchar(200) not null,
	DateOfBirthBS nvarchar(10) not null,
	DateOfBirthAD date not null,
	GenderId int not null Constraint Member_Gender_GenderId_fk References Gender(GenderId),
	CitizenshipNumber nvarchar(100) null,
	ProvinceId int null Constraint Member_Province_ProvinceId_fk References Province(ProvinceId),
	DistrictId int null Constraint Member_District_DistrictId_fk References District(DistrictId),
	MunicipalityTypeId int null Constraint Member_MunicipalityType_MunicipalityTypeId_fk References MunicipalityType(Id),
	Municipality nvarchar(150) null,
	WardNumber int null,
	ToleName nvarchar(150) not null,
);
GO

GO
ALTER TABLE UserDocuments
ADD MemberId int null Constraint UserDocument_Member_MemberId_fk References Member(MemberId)
GO

GO
CREATE UNIQUE INDEX UserDocument_MemberId_ui ON
UserDocuments(MemberId) WHERE MemberId is not null
GO


GO
CREATE TABLE MemberDetails
(
	Id int not null Identity(1,1) Constraint MemberDetails_pk Primary Key,
	MemberId int not null Constraint MemberDetails_Members_MemberId References Member(MemberId),
	MemberTypeId int not null Constraint MemberDetails_Member_MemberId_fk References MemberType(Id),
	IsPrimary bit not null,
	CreatedDate datetime not null,
	CreatedBy int null Constraint MemberDetails_Users_CreatedBy_fk References Users(UserId),
);
GO



