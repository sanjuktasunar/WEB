using System;
using System.Collections.Generic;
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
    public class UnitController : BaseController
    {
        InitialSetupService _initialService = new InitialSetupService();
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();

        private IUnitService _unitService;
        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
            menu = _initialService.GetMenuPermissionForLoginUser("Unit");
            ViewBag.Menus = menu;
        }

        [Route("~/UnitList")]
        public async Task<ActionResult> UnitList()
        {
            if (!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");

            var obj = await _unitService.GetAllUnitAsync();
            return View(obj);
        }

        [Route("~/AddUnit")]
        public ActionResult AddUnit()
        {
            if (!menu.WriteAccess)
                return RedirectToAction("Logout", "Account");

            var obj = new UnitDto();
            return View("AddModifyUnit", obj);
        }

        [Route("~/ModifyUnit/{id}")]
        public async Task<ActionResult> ModifyUnit(int id)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");
            var obj = (await _unitService.GetUnitByIdAsync(Convert.ToInt32(id)));
            return View("AddModifyUnit", obj);
        }

        [HttpPost]
        public string Insert(UnitDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            return _unitService.Insert(dto);
        }

        [HttpPost]
        public async Task<string> Update(UnitDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            return (await _unitService.Update(dto));
        }

        public string Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            return (_unitService.Delete(id));
        }
    }
}