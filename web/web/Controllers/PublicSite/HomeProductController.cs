using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers.PublicSite
{
    public class HomeProductController : Controller
    {
        [Route("~/Products")]
        public ActionResult Product()
        {
            return View();
        }
    }
}