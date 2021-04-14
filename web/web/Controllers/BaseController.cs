using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace web.Controllers
{
    public class BaseController : Controller
    {
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
            }
            else
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.Result = new RedirectResult(Url.Action("Logout","Account"));
            }
        }
    }
}