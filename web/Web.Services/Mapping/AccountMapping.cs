using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Services.Mapping
{
    public static class AccountMapping
    {
        public static Unit ToEntity(this UnitDto dto)
        {
            if (dto == null)
                return null;

            return new Unit
            {
                UnitId = dto.UnitId,
                UnitName = dto.UnitName,
                Status = dto.Status,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
                UnitNameNepali=dto.UnitNameNepali,
                UnitSymbol=dto.UnitSymbol,
                UnitSymbolNepali=dto.UnitSymbolNepali
            };
        }
    }
}
