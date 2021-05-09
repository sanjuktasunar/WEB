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
    public class DesignationController : BaseController
    {
        InitialSetupService _initialService = new InitialSetupService();
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        private IDesignationService _designationService;
        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
            menu = _initialService.GetMenuPermissionForLoginUser("Designation");
            ViewBag.Menus = menu;
        }

        [HttpGet]
        [Route("~/DesignationList")]
        public async Task<ActionResult> Designation()
        {
            if (!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");

            var obj = await _designationService.GetAllDesignation();
            return View(obj);
        }

        [Route("~/AddDesignation")]
        public ActionResult AddDesignation()
        {
            if (!menu.WriteAccess)
                return RedirectToAction("Logout", "Account");

            var obj = new DesignationDto();
            return View("AddModifyDesignation", obj);
        }

        [Route("~/ModifyDesignation/{id}")]
        public async Task<ActionResult> ModifyDesignation(int id)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");
            var obj = (await _designationService.GetDesignationById(Convert.ToInt32(id)));
            return View("AddModifyDesignation", obj);
        }

        [HttpPost]
        public string Insert(DesignationDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            return _designationService.Insert(dto);
        }

        [HttpPost]
        public string Update(DesignationDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            return _designationService.Update(dto);
        }

        public string Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            return (_designationService.Delete(id));
        }
    }
}