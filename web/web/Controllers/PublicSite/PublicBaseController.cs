using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Services.Services;

namespace web.Controllers.PublicSite
{
    public class PublicBaseController : Controller
    {
        protected InitialSetupService _initialSetupService = new InitialSetupService();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.OrganizationInfo = _initialSetupService.GetOrganizationInfo();
        }
    }
}