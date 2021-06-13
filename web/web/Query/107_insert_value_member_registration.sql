
GO
SET IDENTITY_INSERT MemberType ON
GO


GO
INSERT INTO MemberType(Id,Name,NepaliName,Status)
SELECT 1,N'Candidate',N'उम्मेदवार',1 
GO

GO
SET IDENTITY_INSERT MemberType OFF
GO

GO
INSERT INTO MunicipalityType(Name,NepaliName,Status)
SELECT N'Rular Municipality',N'गाउँपालिका',1 UNION ALL
SELECT N'Municipality',N'नगरपालिका',1 UNION ALL
SELECT N'Sub-metropolis',N'उपमहानगरपालिका',1 UNION ALL
SELECT N'Metropolitan',N'महानगरपालिका',1 
GO

GO
INSERT INTO UserType(UserTypeTitle,Status)
SELECT N'Member',1 
GO

GO
INSERT INTO Occupation(Name,NepaliName,Status)
SELECT N'Doctor',N'Doctor',1 UNION ALL
SELECT N'Engineer',N'Engineer',1 UNION ALL
SELECT N'Businessman',N'Businessman',1 UNION ALL
SELECT N'Other',N'Other',1
GO

GO
INSERT INTO MemberField(Name,NepaliName,Status)
SELECT N'Capital Increase',N'Capital Increase',1 UNION ALL
SELECT N'Marketing',N'Marketing',1
GO


GO
SET IDENTITY_INSERT Country ON
GO

GO
INSERT INTO Country(Id,Name,NepaliName,Status,IsOutsideNepal)
SELECT 1,N'Nepal',N'Nepal',1,0 UNION ALL
SELECT 2,N'India',N'India',1,1 UNION ALL
SELECT 3,N'China',N'China',1,1 UNION All
SELECT 4,N'USA',N'USA',1,1
GO

GO
SET IDENTITY_INSERT Country OFF
GO
