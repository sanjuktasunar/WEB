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
        public ActionResult Login(string message=null)
        {
            EnsureLogout();
            Session["LangId"] = 2;
            var obj = new UsersDto();
            obj.Message = message;
            return View(obj);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UsersDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var data =await _usersService.GetLoginUser(dto);
            if (data != null)
            {
                if (data.UserTypeId == 2 && data.UserStatusId == 1)
                    return RedirectToAction("Index", "Dashboard");
                else if (data.UserStatusId == 2)
                {
                    dto.Message = "Your Account is not active";
                }

                else if (data.UserStatusId == 3)
                {
                    dto.Message = "You are suspended,Please contact to admin for further information";
                }
            }
            else
                dto.Message = "User name or password is not correct";
            string url = string.Format(@"/Login?message={0}", dto.Message);
            return Redirect(url);
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