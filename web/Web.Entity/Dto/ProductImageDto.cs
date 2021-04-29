using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class ProductImageDto
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; }
        public string ImageLocation { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPrimary { get; set; }
    }
}
