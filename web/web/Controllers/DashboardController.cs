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
    [Authorize]
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}