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
    public class DepartmentController : BaseController
    {
        InitialSetupService _initialService = new InitialSetupService();
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        private IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
            menu = _initialService.GetMenuPermissionForLoginUser("Department");
            ViewBag.Menus = menu;
        }

        [HttpGet]
        [Route("~/DepartmentList")]
        public async Task<ActionResult> Department()
        {
            if (!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");

            var obj = await _departmentService.GetAllDepartment();
            return View(obj);
        }

        [Route("~/AddDepartment")]
        public ActionResult AddDepartment()
        {
            if (!menu.WriteAccess)
                return RedirectToAction("Logout", "Account");

            var obj = new DepartmentDto();
            return View("AddModifyDepartment", obj);
        }

        [Route("~/ModifyDepartment/{id}")]
        public async Task<ActionResult> ModifyDepartment(int id)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");
            var obj = (await _departmentService.GetDepartmentById(Convert.ToInt32(id)));
            return View("AddModifyDepartment", obj);
        }

        [HttpPost]
        public string Insert(DepartmentDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            return _departmentService.Insert(dto);
        }

        [HttpPost]
        public string Update(DepartmentDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            return _departmentService.Update(dto);
        }

        public string Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            return (_departmentService.Delete(id));
        }
    }
}