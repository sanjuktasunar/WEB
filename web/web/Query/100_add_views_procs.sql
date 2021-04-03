
GO
CREATE OR ALTER VIEW [dbo].[MenuView]
AS
	SELECT A.*,B.MenuNameEnglish AS ParentMenuNameEnglish,B.MenuNameNepali AS ParentMenuNameNepali 
	FROM Menus AS A
	LEFT JOIN Menus AS B ON B.MenuId=A.ParentMenuId
GO
