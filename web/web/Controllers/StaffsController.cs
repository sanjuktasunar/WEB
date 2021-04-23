using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
using Web.Services.Services;

namespace web.Controllers
{
    [Authorize]
    public class StaffsController : BaseController
    {
        InitialSetupService _initialService = new InitialSetupService();
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        
        private IStaffsService _staffsService;
        public StaffsController(IStaffsService staffsService)
        {
            _staffsService = staffsService;
            menu = _initialService.GetMenuPermissionForLoginUser("Staffs");
            ViewBag.Menus = menu;
        }
        [Route("~/StaffList")]
        public async Task<ActionResult> Staffs()
        {
            if (!menu.ReadAccess)
                return null;
            var obj =await _staffsService.GetStaffListAsync();
            return View(obj);
        }

        [Route("~/AddStaff")]
        public async Task<ActionResult> AddStaff()
        {
            var obj = new StaffsDto();
            obj =await _staffsService.DropDownMethods(obj);

            if (!menu.WriteAccess)
                return null;
            
            return View("AddModifyStaff",obj);
        }

        [Route("~/ModifyStaff/{id}")]
        public async Task<ActionResult> ModifyStaff(int id)
        {
            if (!menu.ModifyAccess)
                return null;
            var obj = (await _staffsService.GetStaffsByIdAsync(Convert.ToInt32(id)));
            obj = await _staffsService.DropDownMethods(obj);
            return View("AddModifyStaff", obj);
        }

        [HttpPost]
        public string Insert(StaffsDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            return _staffsService.Insert(dto);
        }

        [HttpPost]
        public async Task<string> Update(StaffsDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            return (await _staffsService.Update(dto));
        }
    }
}