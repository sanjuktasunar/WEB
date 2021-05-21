using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto.UserSite;
using Web.Services.Services.Account;

namespace web.Controllers.PublicSite
{
    public class PublicProductController : Controller
    {
        private IProductService _productService;
        public PublicProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Route("~/Products",Name ="Products")]
        public async Task<ActionResult> Product(int?page,string query)
        {
            var obj = new ProductPageDto();
            query = query?.Trim();
            ViewBag.query = query;
            obj.GetAllProducts =await _productService.GetDisplayProductsForProductPage(page??1,8,query);
            return View(obj);
        }

        [Route("~/ProductDetails/{productId}")]
        public async Task<ActionResult> ProductDetails(int productId)
        {
            var obj = new ProductPageDto();
            obj.Product =await _productService.GetProductById(productId);
            obj.ProductImages = await _productService.GetProductImageByProductId(productId);
            obj.ProductPrice = await _productService.GetActiveProductPriceByProductId(productId);
            return View(obj);
        }
    }
}