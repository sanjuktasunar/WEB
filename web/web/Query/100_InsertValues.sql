
GO
INSERT INTO OrganizationInfo(OrganizationName,AppName,Address,ContactNumber1,ContactNumber2,
							TelephoneNumber,EmailAddress,FaxNumber,POBoxNumber,Logo)
VALUES('Bishwokarma Trading and Promotors Pvt. ltd.','BKP APP','Butwal-11,Buddhanagar','9857039526',
	'','','bishwokarmatrading@gmail.com','','','')
GO


GO
Insert INTO Language(LanguageName,Code,Status)
SELECT 'English','E',1 UNION ALL
SELECT 'Nepali','N',1 
GO


GO
SET IDENTITY_INSERT [UserType] ON;
GO
GO
INSERT INTO UserType(UserTypeId,UserTypeTitle)
SELECT 1,'Customer' UNION ALL
SELECT 2,'Staff'
GO
GO
SET IDENTITY_INSERT [UserType] OFF;
GO


GO
SET IDENTITY_INSERT [UserStatus] ON;
GO
GO
INSERT INTO UserStatus(StatusId,StatusName,UserTypeId)
SELECT 1,'Active',NULL UNION ALL
SELECT 2,'InActive',NULL UNION ALL
SELECT 3,'Suspend',2
SELECT 4,'Left',2
GO
GO
SET IDENTITY_INSERT [UserStatus] OFF;
GO



GO
INSERT INTO Gender(GenderName)
SELECT 'Male' UNION ALL
SELECT 'Female' UNION ALL
SELECT 'Others'
GO


GO
SET IDENTITY_INSERT [Menus] ON;
GO

GO
INSERT INTO Menus(MenuId,ParentMenuId,MenuNameEnglish,MenuNameNepali,CheckMenuName,MenuLink,MenuOrder,MenuIcon)
SELECT 1,NULL,N'Administration',N'प्रशासन ',N'Administration',N'#',1,'fas fa-fw fa-cog' UNION ALL
SELECT 2,1,N'Menus',N'मेनु ',N'Menus','/MenuList',1,'' UNION ALL
SELECT 3,1,N'Staffs Info',N'कर्मचारी',N'Staffs','/StaffList',2,''
GO

GO
SET IDENTITY_INSERT [Menus] OFF;
GO


GO
SET IDENTITY_INSERT [Role] ON;
GO
GO
INSERT INTO Role(RoleId,RoleName)
VALUES(1,N'Administration')
GO
GO
SET IDENTITY_INSERT [Role] OFF;
GO


GO
SET IDENTITY_INSERT [Designation] ON;
GO
GO
INSERT INTO Designation(DesignationId,DesignationName)
VALUES(1,N'Admin')
GO
GO
SET IDENTITY_INSERT [Designation] OFF;
GO


GO
SET IDENTITY_INSERT [Department] ON;
GO
GO
INSERT INTO Department(DepartmentId,DepartmentName)
VALUES(1,N'Administration')
GO
GO
SET IDENTITY_INSERT [Department] OFF;
GO


GO
SET IDENTITY_INSERT [PhotoStorages] ON;
GO
GO
INSERT INTO PhotoStorages(PhotoStorageId,Photo)
VALUES(1,NULL)
GO
GO
SET IDENTITY_INSERT [PhotoStorages] OFF;
GO


GO
SET IDENTITY_INSERT [Users] ON;
GO
GO
INSERT INTO Users(UserId,UserTypeId,PhotoStorageId,UserName,Password,EmailAddress,ContactNumber,CreatedDate,UserStatusId)
VALUES(1,2,1,N'12345','45E5F464304A2F961CB6A585C42DA3EC','abc@gmail.com','1234567890',GETDATE(),1)
GO
GO
SET IDENTITY_INSERT [Users] OFF;
GO


GO
SET IDENTITY_INSERT [Staffs] ON;
GO
GO
INSERT INTO Staffs(StaffId,UserId,RoleId,DesignationId,DepartmentId,StaffName,GenderId,TemporaryAddress,PermanentAddress,CitizenshipNumber,PanNumber,BasicSalary)
VALUES(1,1,1,1,1,'Admin Name',1,'Temp add','Permanent Add','1234/56','4567000',10000)
GO
GO
SET IDENTITY_INSERT [Staffs] OFF;
GO

GO
INSERT INTO MenuAccessPermission(MenuId,StaffId,ReadAccess,WriteAccess,ModifyAccess,DeleteAccess,AdminAccess)
SELECT 2,1,1,1,1,1,1 UNION ALL
SELECT 3,1,1,1,1,1,1
GO

GO
SET IDENTITY_INSERT [Province] ON;
GO
GO
INSERT INTO Province(ProvinceId,ProvinceName,ProvinceNameNepali,Status,CreatedDate)
SELECT 1,N'Province No. 1',N'1',1,GETDATE() UNION ALL
SELECT 2,N'Province No. 2',N'2',1,GETDATE() UNION ALL
SELECT 3,N'Province No. 3',N'3',1,GETDATE() UNION ALL
SELECT 4,N'Province No. 4',N'4',1,GETDATE() UNION ALL
SELECT 5,N'Province No. 5',N'5',1,GETDATE() UNION ALL
SELECT 6,N'Province No. 6',N'6',1,GETDATE() UNION ALL
SELECT 7,N'Province No. 7',N'7',1,GETDATE()
GO
GO
SET IDENTITY_INSERT [Province] OFF;
GO

