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
                UnitName = dto.UnitName?.Trim(),
                Status = dto.Status,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
                UnitNameNepali=dto.UnitNameNepali?.Trim(),
                UnitSymbol=dto.UnitSymbol?.Trim(),
                UnitSymbolNepali=dto.UnitSymbolNepali?.Trim()
            };
        }

        public static Product ToEntity(this ProductDto dto)
        {
            if (dto == null)
                return null;

            return new Product
            {
                ProductId = dto.ProductId,
                ParentProductId = dto.ParentProductId,
                ProductName = dto.ProductName?.Trim(),
                ProductNameNepali = dto.ProductNameNepali?.Trim(),
                ProductCode = dto.ProductCode?.Trim(),
                CreatedBy=dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
                Status = dto.Status,
            };
        }

        public static ProductPrice ToEntity(this ProductPriceDto dto)
        {
            if (dto == null)
                return null;

            return new ProductPrice
            {
                ProductPriceId = dto.ProductPriceId,
                ProductId = dto.ProductId,
                UnitId = dto.UnitId,
                SellingPrice = dto.SellingPrice,
                CreatedBy = dto.CreatedBy,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
                Status = dto.Status,
            };
        }

        public static ProductImage ToEntity(this ProductImageDto dto)
        {
            if (dto == null)
                return null;

            return new ProductImage
            {
                ImageId=dto.ImageId,
                ProductId=dto.ProductId,
                Photo=dto.Photo,
                IsActive=dto.IsActive,
                IsPrimary=dto.IsPrimary,
            };
        }
    }
}
