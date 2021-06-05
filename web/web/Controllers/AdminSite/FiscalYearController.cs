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
    public class FiscalYearController : BaseController
    {
        private readonly IFiscalYearService _fiscalYearService;
        private readonly InitialSetupService _initialService = new InitialSetupService();
        private readonly MenuAccessPermissionDto menu = new MenuAccessPermissionDto();

        public FiscalYearController(IFiscalYearService fiscalYearService)
        {
            _fiscalYearService = fiscalYearService;
            menu = _initialService.GetMenuPermissionForLoginUser("FiscalYear");
            ViewBag.Menus = menu;
        }

        [Route("~/FiscalYearList")]
        public async Task<ActionResult> FiscalYearList()
        {
            if (!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");

            var obj = await _fiscalYearService.GetAllFiscalYearAsync();
            return View(obj);
        }

        [Route("~/AddFiscalYear")]
        public ActionResult AddFiscalYear()
        {
            if (!menu.WriteAccess)
                return RedirectToAction("Logout", "Account");

            var obj = new FiscalYearDto();
            return View("AddModifyFiscalYear", obj);
        }

        [Route("~/ModifyFiscalYear/{id}")]
        public async Task<ActionResult> ModifyFiscalYear(int id)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");

            var obj = (await _fiscalYearService.GetFiscalYearByIdAsync(Convert.ToInt32(id)));
            return View("AddModifyFiscalYear", obj);
        }

        [HttpPost]
        public async Task<string> Post(FiscalYearDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            return (await _fiscalYearService.InsertAsync(dto));
        }

        [HttpPost]
        public async Task<string> Put(FiscalYearDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            return await _fiscalYearService.UpdateAsync(dto);
        }

        [HttpPost]
        public async Task<string> Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            return await _fiscalYearService.DeleteAsync(id);
        }
    }
}