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
    public class RoleController : BaseController
    {
        InitialSetupService _initialService = new InitialSetupService();
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();

        private IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
            menu = _initialService.GetMenuPermissionForLoginUser("Role");
            ViewBag.Menus = menu;
        }

        [HttpGet]
        [Route("~/RoleList")]
        public async Task<ActionResult> Roles()
        {
            if (!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");

            var obj = await _roleService.GetAllRoles();
            return View(obj);
        }

        [Route("~/AddRole")]
        public ActionResult AddRole()
        {
            if (!menu.WriteAccess)
                return RedirectToAction("Logout", "Account");

            var obj = new RoleDto();
            return View("AddModifyRole", obj);
        }

        [Route("~/ModifyRole/{id}")]
        public async Task<ActionResult> ModifyRole(int id)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");
            var obj = (await _roleService.GetRoleById(Convert.ToInt32(id)));
            return View("AddModifyRole", obj);
        }

        [HttpPost]
        public string Insert(RoleDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            return _roleService.Insert(dto);
        }

        [HttpPost]
        public string Update(RoleDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            return _roleService.Update(dto);
        }

        public string Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            return (_roleService.Delete(id));
        }

        [Route("~/MenuAccessPermission/{roleId}")]
        public async Task<ActionResult> MenuAccessPermission(int roleId)
        {
            if (!menu.AdminAccess)
                return RedirectToAction("Logout", "Account");
            var obj = (await _roleService.MenuAccessPermissionAsync(roleId));
            if (obj.Status == true)
                return View(obj);
            else
                return Redirect("/RoleList");
        }

        [HttpPost]
        public async Task<JsonResult> MenuAccessPermission(int id, IEnumerable<MenuAccessPermissionDto> dto)
        {
            if (!menu.AdminAccess)
                return null;
            string message = "";
            var obj = (await _roleService.GetRoleById(id));
            if (obj.Status!=true)
                message = "Menu assign only active roles" + "+" + -1;
            message = await _roleService.AddMenuAccess(dto);
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}