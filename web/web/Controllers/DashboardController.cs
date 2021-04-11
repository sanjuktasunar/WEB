using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Entity;
using Web.Services;
using Web.Services.Services;

namespace web.Controllers
{
    public class DashboardController : Controller
    {
        public MenusService _menusService=new MenusService();

      
        public async Task<ActionResult> Index()
        {
            var menu =await _menusService.GetMenusAsync();
            return RedirectToAction("Index");
        }

        
    }
}