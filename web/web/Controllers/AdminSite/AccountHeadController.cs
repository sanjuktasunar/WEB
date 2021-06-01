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
    public class AccountHeadController : BaseController
    {
        private IAccountHeadService _accountHeadService;
        InitialSetupService _initialService = new InitialSetupService();
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        public AccountHeadController(IAccountHeadService accountHeadService)
        {
            _accountHeadService = accountHeadService;
            menu = _initialService.GetMenuPermissionForLoginUser("AccountHead");
            ViewBag.Menus = menu;
        }
        [Route("~/AccountHeadList")]
        public async Task<ActionResult> AccountHeadList()
        {
            if (!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");
            var obj = await _accountHeadService.GetAllAccountHead();
            return View(obj);
        }

        [Route("~/AddAccountHead")]
        public ActionResult AddAccountHead()
        {
            if (!menu.WriteAccess)
                return RedirectToAction("Logout", "Account");

            var obj = new AccountHeadDto();
            return View("AddModifyAccountHead", obj);
        }

        [Route("~/ModifyAccountHead/{id}")]
        public async Task<ActionResult> ModifyAccountHead(int id)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");

            var obj = (await _accountHeadService.GetAccountHeadById(Convert.ToInt32(id)));
            return View("AddModifyAccountHead", obj);
        }

        [HttpPost]
        public string Insert(AccountHeadDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            return _accountHeadService.Insert(dto);
        }

        [HttpPost]
        public async Task<string> Update(AccountHeadDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            return await _accountHeadService.Update(dto);
        }

        public string Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            return (_accountHeadService.Delete(id));
        }
    }
}