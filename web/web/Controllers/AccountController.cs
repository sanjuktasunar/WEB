using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
using Web.Services.Services;

namespace web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUsersService _usersService;

        public AccountController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("~/Login")]
        public ActionResult Login()
        {
            EnsureLogout();
            Session["LangId"] = 2;
            var obj = new UsersDto();
            return View(obj);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UsersDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var data =await _usersService.GetLoginUser(dto);
            if (data == null)
                return Redirect("~/Login");
            if(data.UserTypeId==2)
                return RedirectToAction("Index", "Dashboard");
            return View(dto);
        }

        public ActionResult Logout()
        {
            EnsureLogout();
            return Redirect("~/Login");
        }

        public void EnsureLogout()
        {
            Session["UserId"] = null;
            Session["UserTypeId"] = null;
            Session.Abandon();
        }
    }
}