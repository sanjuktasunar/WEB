using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers.PublicSite
{
    public class MemberRegisterController : PublicBaseController
    {
        [Route("~/MemberRegistration")]
        public ActionResult MemberRegistration()
        {
            var obj = new MemberDto();
            return View();
        }
    }
}