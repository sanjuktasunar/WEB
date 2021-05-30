
GO
ALTER TABLE OrganizationInfo
ADD Favicon NVARCHAR(MAX) NULL 
GO

GO
ALTER TABLE OrganizationInfo
DROP COLUMN Logo 
GO

GO
ALTER TABLE OrganizationInfo
ADD Logo NVARCHAR(MAX) NULL
GO


GO
ALTER TABLE OrganizationInfo
ADD NormalizedName NVARCHAR(100) NULL
GO


GO
UPDATE OrganizationInfo SET NormalizedName=N'Store'
GO


GO
CREATE TABLE CustomerQuery
(
	Id int not null Identity(1,1) Constraint CustomerQuery_pk Primary Key,
	Name nvarchar(100) not null,
	PhoneNumber nvarchar(20) not null,
	EmailAddress nvarchar(200) null,
	Address nvarchar(300) not null,
	Subject nvarchar(200) null default(''),
	Message nvarchar(1000) not null
);
GO

