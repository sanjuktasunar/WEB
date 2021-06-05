using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
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
        public async Task<ActionResult> MemberRegistration()
        {
            var obj = new MemberDto();
            ViewBag.Gender =await _administrationService.GetActiveGenderAsync();
            return View(obj);
        }
    }
}