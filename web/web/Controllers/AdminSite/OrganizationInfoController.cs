using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
using Web.Services.Services;
using Web.Services.Services.Administration;

namespace web.Controllers.AdminSite
{
    [Authorize]
    public class OrganizationInfoController : BaseController
    {
        private readonly InitialSetupService _initialService = new InitialSetupService();
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        private IOrganizationInfoService _organizationInfoService;
        public OrganizationInfoController(IOrganizationInfoService organizationInfoService)
        {
            _organizationInfoService = organizationInfoService;
            menu = _initialService.GetMenuPermissionForLoginUser("OrganizationInfo");
            ViewBag.Menus = menu;
        }
        [Route("~/OrganizationInfo")]
        public async Task<ActionResult> OrganizationInfo()
        {
            if (!menu.AdminAccess)
                return RedirectToAction("Logout", "Account");

            var obj =await _organizationInfoService.GetOrganizationInfo();
            return View(obj);
        }

        [HttpPost]
        public JsonResult Update(OrganizationInfoDto dto)
        {
            if (!menu.AdminAccess)
                return null;
            string message = _organizationInfoService.Update(dto);
            return Json(message,JsonRequestBehavior.AllowGet);
        }

       
    }
}