
GO
DROP VIEW [dbo].[MemberView]
GO

GO
DROP TABLE  [dbo].[BankDeposit]
GO


GO
DROP TABLE [dbo].[Address]
GO

GO
DROP TABLE MemberDetails
GO


GO
DROP TABLE UserDocuments
GO


GO
CREATE TABLE UserDocuments
(
	UserDocumentId int not null Identity(1,1) Constraint UserDocuments_pk Primary Key,
	StaffId int null Constraint UserDocuments_Staffs_StaffId References Staffs(StaffId),
	CitizenshipFront nvarchar(max) null,
	CitizenshipBack nvarchar(max) null,
	PanCard nvarchar(max) null,
	EducationalDocument nvarchar(max) null
);
GO
GO
CREATE UNIQUE INDEX UserDocument_StaffId_ui ON
UserDocuments(StaffId) WHERE StaffId is not null
GO

GO
DROP TABLE Member
GO

GO
DROP TABLE MemberType
GO

GO
DELETE FROM UserType WHERE UserTypeTitle=N'Member'
GO

GO
DROP TABLE MunicipalityType
GO


GO
DROP TABLE AccountHead
GO


GO
DROP TABLE MemberField
GO

GO
DROP TABLE Occupation
GO

GO
DROP TABLE Country
GO




