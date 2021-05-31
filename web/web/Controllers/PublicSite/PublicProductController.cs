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
    public class PublicProductController : PublicBaseController
    {
        private IProductService _productService;
        public PublicProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Route("~/Products",Name ="Products")]
        public async Task<ActionResult> Product(int?page,string query, int? parentProductId)
        {
            var obj = new ProductPageDto();
            obj.ParentProductId = parentProductId;
            obj.query = query?.Trim();
            obj.page = page ?? 1;
            ViewBag.parentProductId = parentProductId;
            obj.GetAllProducts =await _productService.GetDisplayProductsForProductPage(obj.page,8,obj.query,parentProductId);
            obj.GetParentProducts = await _productService.GetParentProductsWithChildProduct();
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