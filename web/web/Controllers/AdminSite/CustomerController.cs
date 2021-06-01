using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Services.Services;
using Web.Services.Services.Customer;

namespace web.Controllers.AdminSite
{
    [Authorize]
    public class CustomerController : BaseController
    {
        InitialSetupService _initialService = new InitialSetupService();
        private readonly ICustomerQueryService _customerQueryService;
        public CustomerController(ICustomerQueryService customerQueryService)
        {
            _customerQueryService = customerQueryService;
        }

        [Route("CustomerQuery")]
        public async Task<ActionResult> CustomerQuery()
        {
            var menu = _initialService.GetMenuPermissionForLoginUser("CustomerQuery");
            if(!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");

            var obj =await _customerQueryService.GetAllQuery();
            return View(obj);
        }

        [Route("QueryDetails/{id}")]
        public async Task<ActionResult> QueryDetails(int id)
        {
            var menu = _initialService.GetMenuPermissionForLoginUser("CustomerQuery");
            if (!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");

            var obj = await _customerQueryService.GetQueryById(id);
            return View(obj);
        }
    }
}