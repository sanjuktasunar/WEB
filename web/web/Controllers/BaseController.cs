using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Services.Services;

namespace web.Controllers
{
    public class BaseController : Controller
    {
        protected InitialSetupService _initialSetupService=new InitialSetupService();
      
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Convert.ToInt32(HttpContext.Session["UserId"]) > 0)
            {
                Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.ClearHeaders();
                Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");

                ViewBag.Menus = (_initialSetupService.GetMenuAccessPermissionForLoginUser());
                ViewBag.Version = _initialSetupService.GetCurrentVersionInfo();
            }
            else
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.Result = new RedirectResult(Url.Action("Logout","Account"));
            }
        }
    }
}