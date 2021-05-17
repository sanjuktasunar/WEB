﻿using System;
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
        [Route("~/Products")]
        public async Task<ActionResult> Product()
        {
            var obj = new ProductPageDto();
            obj.GetAllProducts =await _productService.GetDisplayProductsForProductPage();
            return View(obj);
        }

        [Route("~/ProductDetails/{productId}")]
        public async Task<ActionResult> ProductDetails(int productId)
        {
            var obj = new ProductPageDto();
            obj.GetAllProducts = await _productService.GetDisplayProductsForProductPage();
            return View();
        }
    }
}