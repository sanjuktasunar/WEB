using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
using Web.Services.Services;
using Web.Services.Services.AddressService;

namespace web.Controllers.AdminSite
{
    [Authorize]
    public class CountryController : BaseController
    {
        private ICountryService _countryService;
        InitialSetupService _initialService = new InitialSetupService();
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
            menu = _initialService.GetMenuPermissionForLoginUser("Country");
            ViewBag.Menus = menu;
        }

        [Route("~/CountryList")]
        public async Task<ActionResult> CountryList()
        {
            if (!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");

            var obj = await _countryService.GetCountryListAsync();
            return View(obj);
        }
        [Route("~/AddCountry")]
        public ActionResult AddCountry()
        {
            if (!menu.WriteAccess)
                return RedirectToAction("Logout", "Account");

            var obj = new CountryDto();
            return View("AddModifyCountry", obj);
        }

        [Route("~/ModifyCountry/{id}")]
        public async Task<ActionResult> ModifyCountry(int id)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");

            var obj = (await _countryService.GetCountryByIdAsyc(Convert.ToInt32(id)));
            return View("AddModifyCountry", obj);
        }

        [HttpPost]
        public string Insert(CountryDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            return _countryService.Insert(dto);
        }

        [HttpPost]
        public async Task<string> Update(CountryDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            return await _countryService.Update(dto);
        }

        public async Task<string>  Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            return (await _countryService.Delete(id));
        }
    }
}