using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Utility;

namespace web.Controllers.PublicSite
{
    public class HomeController : Controller
    {
        public DisplayParamters _displayParameters = new DisplayParamters();
        public ActionResult Index()
        {
            var obj = _displayParameters.GetParameters();
            obj.MenuLink = _displayParameters.GetMenuLink();
            return View(obj);
        }
    }
}