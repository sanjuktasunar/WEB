using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
using Web.Entity.Model;
using Web.Services.Mapping;
using Web.Services.Services;

namespace web.Controllers.PublicSite
{
    public class MemberRegisterController : PublicBaseController
    {
        private readonly IAdministrationService _administrationService;
        public MemberRegisterController(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }
        [Route("~/MemberRegistration")]
        public ActionResult MemberRegistration()
        {
            var obj = new MemberDto();
            return View(obj);
        }

        [HttpPost]
        public JsonResult PostPersonalInfo(MemberPersonalInfoDto dto)
        {
            if (!ModelState.IsValid)
            {
                List<KeyValuePairDto> errors = ModelState.Select(a=>a.ToModelState()).ToList();
            }
            return Json(true,JsonRequestBehavior.AllowGet);
        }
    }
}