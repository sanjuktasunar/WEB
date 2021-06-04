
GO
INSERT INTO MemberType(Name,NepaliName,Status)
SELECT N'Candidate',N'उम्मेदवार',1 
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
