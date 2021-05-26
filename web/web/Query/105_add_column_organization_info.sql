
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

