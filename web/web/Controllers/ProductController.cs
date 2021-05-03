using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
using Web.Services.Services;
using Web.Services.Services.Account;

namespace web.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        InitialSetupService _initialService = new InitialSetupService();
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();

        private IProductService _productService;
        private IUnitService _unitService;
        public ProductController(IProductService productService,
            IUnitService unitService)
        {
            _productService = productService;
            _unitService = unitService;
            menu = _initialService.GetMenuPermissionForLoginUser("Unit");
            ViewBag.Menus = menu;
        }
        [Route("~/ProductList")]
        public async Task<ActionResult> ProductList()
        {
            if (!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");

            var obj = await _productService.GetAllProduct();
            return View(obj);
        }

        [Route("~/AddProduct")]
        public async Task<ActionResult> AddProduct()
        {
            if (!menu.WriteAccess)
                return RedirectToAction("Logout", "Account");

            var obj = new ProductDto();
            obj = await _productService.DropDownList(obj);
            return View("AddModifyProduct", obj);
        }

        [Route("~/ModifyProduct/{id}")]
        public async Task<ActionResult> ModifyProduct(int id)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");
            var obj = (await _productService.GetProductById(Convert.ToInt32(id)));
            obj = await _productService.DropDownList(obj);
            return View("AddModifyProduct", obj);
        }

        [HttpPost]
        public string Insert(ProductDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            return _productService.Insert(dto);
        }

        [HttpPost]
        public async Task<string> Update(ProductDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            return (await _productService.Update(dto));
        }

        [HttpGet]
        [Route("~/ProductPrice/{productId}")]
        public async Task<ActionResult> ProductPrice(int productId)
        {
            if (!menu.AdminAccess)
                return RedirectToAction("Logout", "Account");
            var obj = (await _productService.GetProductById(productId));
             obj.GetProductPrice = (await _productService.GetProductPriceByProductId(Convert.ToInt32(productId)));
            obj.Units =await _unitService.GetActiveUnitAsync();
            return View(obj);
        }

        [HttpPost]
        public async Task<string> InsertPrice(ProductPriceDto dto)
        {
            if (!menu.AdminAccess)
                return null;

            return (await _productService.InsertProductPrice(dto));
        }
        public string Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            return (_productService.Delete(id));
        }

        public string DeletePrice(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            return (_productService.DeletePrice(id));
        }
        public async Task<string> UpdatePrice(int productPriceId)
        {
            if (!menu.ModifyAccess)
                return null;

            return (await _productService.UpdatePrice(productPriceId));
        }

        [HttpGet]
        [Route("~/ProductImage/{productId}")]
        public async Task<ActionResult> ProductImage(int productId)
        {
            if (!menu.AdminAccess)
                return RedirectToAction("Logout", "Account");

            var obj = (await _productService.GetProductById(productId));
            obj.ProductImages = (await _productService.GetProductImageByProductId(Convert.ToInt32(productId)));
            return View(obj);
        }
        [HttpPost]
        public async Task<string> InsertImage(ProductImageDto dto)
        {
            if (!menu.AdminAccess)
                return null;
            dto.Image = Request.Files[0];
            string message = await _productService.InsertImage(dto);
            return message;
        }

        public async Task<string> DeleteImage(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            return (await _productService.DeleteImage(id));
        }

        [HttpPost]
        public async Task<string> UpdateImage(int ImageId)
        {
            if (!menu.AdminAccess)
                return null;
           
            string message = await _productService.UpdateImage(ImageId);
            return message;
        }
    }
}