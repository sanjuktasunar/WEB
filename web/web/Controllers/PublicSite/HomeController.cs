using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
using Web.Entity.Dto.UserSite;
using Web.Services.Services.Account;

namespace web.Controllers.PublicSite
{
    public class HomeController : Controller
    {
        public DisplayParamters _displayParameters = new DisplayParamters();
        private IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<ActionResult> Index()
        {
            Session["LangId"] = 2;
            var obj = new UserIndexDto();
            obj.ParameterClass = _displayParameters.GetParameters();
            obj.ParameterClass.MenuLink = _displayParameters.GetMenuLink();
            obj.ParentProductDto =await _productService.GetParentProductsWithChildProduct();
            obj.ChildProductDto =await _productService.GetDisplayProducts();
            return View(obj);
        }

        public async Task<ActionResult> GetChildProductByParentProductId(int id)
        {
            var obj =(await _productService.GetChildProductByParentProductId(id));
            var jsonResult = Json(new { data=obj.Take(8),total=obj.Count()}, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}