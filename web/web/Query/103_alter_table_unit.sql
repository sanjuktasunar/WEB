
GO
ALTER TABLE Unit
ADD UnitSymbol nvarchar(20) not null
GO

GO
CREATE UNIQUE INDEX Unit_UnitSymbol_ui ON
Unit(UnitSymbol)
GO


GO
ALTER TABLE Unit
ADD UnitSymbolNepali nvarchar(100) not null
GO


GO
CREATE UNIQUE INDEX Unit_UnitSymbolNepali_ui ON
Unit(UnitSymbolNepali)
GO


GO
ALTER TABLE Unit
ADD UnitNameNepali nvarchar(400) not null
GO


GO
CREATE UNIQUE INDEX Unit_UnitNameNepali_ui ON
Unit(UnitNameNepali)
GO
