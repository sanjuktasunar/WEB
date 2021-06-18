

GO
ALTER TABLE Member
ADD IsActive bit null default(0)
GO

GO
CREATE OR ALTER VIEW [dbo].[MemberView]
AS
SELECT A.*,B.FirstName AS RefernceFirstName,B.MiddleName AS ReferenceMiddleName,B.LastName AS ReferenceLastName,
B.ReferalCode AS ReferenceReferalCode
FROM Member AS A
LEFT JOIN Member AS B ON A.ReferenceId=B.MemberId
GO


GO
CREATE OR ALTER VIEW [dbo].[AddressView]
AS
SELECT A.*,PP.ProvinceName AS PermanentProvinceName,PD.DistrictName AS PermanentDistrictName,
TP.ProvinceName as TemporaryProvinceName,TD.DistrictName AS TemporaryDistrictName,
PM.Name AS PermanentMunicipalityName,TM.Name AS TemporaryMunicipalityName,
PC.Name AS PermanentCountryName,TC.Name AS TemporaryCountryName
FROM Address AS A
LEFT JOIN Province AS PP ON PP.ProvinceId=A.PermanentProvinceId
LEFT JOIN District AS PD ON PD.DistrictId=A.PermanentDistrictId
LEFT JOIN Province AS TP ON TP.ProvinceId=A.TemporaryProvinceId
LEFT JOIN District AS TD ON TD.DistrictId=A.TemporaryDistrictId
LEFT JOIN MunicipalityType AS PM ON PM.Id=A.PermanentMunicipalityTypeId
LEFT JOIN MunicipalityType AS TM ON TM.Id=A.TemporaryMunicipalityTypeId
LEFT JOIN Country AS PC ON PC.Id=A.PermanentCountryId
LEFT JOIN Country AS TC ON TC.Id=A.TemporaryCountryId
GO


GO
CREATE OR ALTER VIEW [dbo].[MemberView]
AS
SELECT B.*,A.PermanentIsOutsideNepal,A.PermanentCountryId,A.PermanentProvinceId,
A.PermanentDistrictId,A.PermanentMunicipalityTypeId,A.PermanentMunicipality,PermanentWardNumber,A.PermanentToleName,
A.PermanentAddress,A.TemporaryIsOutsideNepal,A.TemporaryCountryId,A.TemporaryProvinceId,A.TemporaryDistrictId,A.TemporaryMunicipalityTypeId,
A.TemporaryMunicipality,A.TemporaryAddress,A.TemporaryWardNumber,A.TemporaryToleName,
PP.ProvinceName AS PermanentProvinceName,PD.DistrictName AS PermanentDistrictName,
TP.ProvinceName as TemporaryProvinceName,TD.DistrictName AS TemporaryDistrictName,
PM.Name AS PermanentMunicipalityTypeName,TM.Name AS TemporaryMunicipalityTypeName,
PC.Name AS PermanentCountryName,TC.Name AS TemporaryCountryName,

B1.FirstName AS RefernceFirstName,B1.MiddleName AS ReferenceMiddleName,B1.LastName AS ReferenceLastName,
B1.ReferalCode AS ReferenceReferalCode
FROM
Member AS B 
LEFT JOIN Address AS A ON A.MemberId=B.MemberId
LEFT JOIN Province AS PP ON PP.ProvinceId=A.PermanentProvinceId
LEFT JOIN District AS PD ON PD.DistrictId=A.PermanentDistrictId
LEFT JOIN Province AS TP ON TP.ProvinceId=A.TemporaryProvinceId
LEFT JOIN District AS TD ON TD.DistrictId=A.TemporaryDistrictId
LEFT JOIN MunicipalityType AS PM ON PM.Id=A.PermanentMunicipalityTypeId
LEFT JOIN MunicipalityType AS TM ON TM.Id=A.TemporaryMunicipalityTypeId
LEFT JOIN Country AS PC ON PC.Id=A.PermanentCountryId
LEFT JOIN Country AS TC ON TC.Id=A.TemporaryCountryId
LEFT JOIN Member AS B1 ON B.ReferenceId=B1.MemberId

GO


GO
ALTER TABLE MenuAccessPermission
ADD RoleId int null Constraint MenuAccessPermission_Role_RoleId_fk References Role(RoleId)
GO