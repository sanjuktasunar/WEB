using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
using Web.Services.Services;
using Web.Services.Services.Account;

namespace web.Controllers.AdminSite
{
    [Authorize]
    public class SupplierController : BaseController
    {
        InitialSetupService _initialService = new InitialSetupService();
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();

        ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
            menu = _initialService.GetMenuPermissionForLoginUser("Supplier");
            ViewBag.Menus = menu;
        }

        [Route("~/SupplierList")]
        public async Task<ActionResult> SupplierList()
        {
            if (!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");

            var obj = await _supplierService.GetAllSupplierAsync();
            return View(obj);
        }

        [Route("~/AddSupplier")]
        public ActionResult AddSupplier()
        {
            if (!menu.WriteAccess)
                return RedirectToAction("Logout", "Account");

            var obj = new SupplierDto();
            return View("AddModifySupplier", obj);
        }

        [Route("~/ModifySupplier/{id}")]
        public async Task<ActionResult> ModifySupplier(int id)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");
            var obj = (await _supplierService.GetSupplierByIdAsync(Convert.ToInt32(id)));
            return View("AddModifySupplier", obj);
        }

        [HttpPost]
        public string Post(SupplierDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            return _supplierService.Insert(dto);
        }
        [HttpPost]
        public async Task<string> Put(SupplierDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            return (await _supplierService.Update(dto));
        }

        public string Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            return (_supplierService.Delete(id));
        }
    }
}