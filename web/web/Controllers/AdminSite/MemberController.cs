using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
using Web.Services.Services;
using Web.Services.Services.Members;

namespace web.Controllers.AdminSite
{
    [Authorize]
    public class MemberController : BaseController
    {
        InitialSetupService _initialService = new InitialSetupService();
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            menu = _initialService.GetMenuPermissionForLoginUser("Member");
            ViewBag.Menus = menu;
            _memberService = memberService;
        }
        [Route("~/MemberList")]
        public async Task<ActionResult> MemberList()
        {
            if (!menu.ReadAccess)
                return null;
            var obj = await _memberService.GetMemberList();
            return View(obj);
        }
    }
}