using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto.UserSite
{
    public class ProductPageDto
    {
        public IEnumerable<ProductImageDto> ProductImages { get; set; }
        public IEnumerable<ProductPriceDto> ProductPrice { get; set; }
        public ProductDto Product { get; set; }
        public IPagedList<ProductDto> GetAllProducts { get; set; }
    }
}
