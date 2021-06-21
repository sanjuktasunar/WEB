using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
using Web.Entity.Dto.UserSite;
using Web.Services.Services.Account;
using Web.Services.Services.Administration;
using Web.Services.Services.Customer;

namespace web.Controllers.PublicSite
{
    public class HomeController : PublicBaseController
    {
        public DisplayParamters _displayParameters = new DisplayParamters();
        private IProductService _productService;
        private ICustomerQueryService _customerQueryService;
        public HomeController(IProductService productService, ICustomerQueryService customerQueryService)
        {
            _productService = productService;
            _customerQueryService = customerQueryService;
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
            var jsonResult = Json(new { data=obj.Take(12),total=obj.Count()}, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [Route("~/ContactUs")]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public string InsertQuery(CustomerQueryDto dto)
        {
           return _customerQueryService.Insert(dto);
        }

        [Route("~/AboutUs")]
        public ActionResult AboutUs()
        {
            return View();
        }
    }
}