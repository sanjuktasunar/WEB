using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Database.BaseRepo;
using Web.Entity.Dto;
using Web.Repositories.Utitlities;
using Web.Services.Services;

namespace web.Controllers
{
    public class MenusController:BaseController
    {
        private IMenusService _menusService { get; set; }
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupService _initialService = new InitialSetupService();
      
        public MenusController(IMenusService menusService)
        {
            _menusService = menusService;
            menu = _initialService.GetMenuPermissionForLoginUser("Menus");
            ViewBag.Menus = menu;
        }

        [Route("~/MenuList")]
        public async Task<ActionResult> Menus()
        {
            if (menu.ReadAccess == true || menu.AdminAccess)
            {
                var data = await _menusService.GetMenusAsync();
                return View(data);
            }
            else
                return RedirectToAction("Logout", "Account");
        }

        [Route("~/AddModifyMenus")]
        [Route("~/AddModifyMenus/{id}")]
        public async Task<ActionResult> AddModifyMenus(int?id)
        {
            if (menu.WriteAccess == true || menu.AdminAccess)
            {
                var menu = new MenusDto();
                if (id > 0)
                    menu =await _menusService.GetMenusByIdAsync(Convert.ToInt32(id));
                menu.GetParentMenus = await _menusService.GetParentMenusAsync();
                return View(menu);
            }
            else
                return RedirectToAction("Logout", "Account");

        }

        [HttpPost]
        public string Insert(MenusDto dto)
        {
            if (menu.WriteAccess == true || menu.AdminAccess)
            {
                return (_menusService.Insert(dto));
            }
            else
                return null;
        }

        [HttpPost]
        public string Update(MenusDto dto)
        {
            if (menu.ModifyAccess == true || menu.AdminAccess)
            {
                return (_menusService.Update(dto));
            }
            else
                return null;
        }

        public string Delete(int id)
        {
            if (!menu.DeleteAccess == true || !menu.AdminAccess)
                return null;

            return (_menusService.Delete(id));
        }
    }
}