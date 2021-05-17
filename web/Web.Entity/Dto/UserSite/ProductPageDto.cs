using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto.UserSite
{
    public class ProductPageDto
    {
        public IEnumerable<ProductDto> GetAllProducts { get; set; }
    }
}
