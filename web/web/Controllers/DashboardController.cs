using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Entity;

namespace web.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            Menus menu = new Menus();
            return View();
        }
    }
}