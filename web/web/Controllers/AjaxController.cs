using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Services.Interface;
using Web.Services.Services;

namespace web.Controllers
{
    public class AjaxController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IDataService _dataService;
        public AjaxController(IImageService imageService, IDataService dataService)
        {
            _imageService = imageService;
            _dataService = dataService;
        }
        public JsonResult ConvertFileToString()
        {
            string result = string.Empty;
            var file= Request.Files[0];
            if(file!=null)
            {
                result = _imageService.ConvertToString(file);
                result = "data:image;base64," + result;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetOutsideCountry()
        {
            var result = await _dataService.GetOutsideCountryAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetProvince()
        {
            var result = await _dataService.GetActiveProvinceAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetDistrictByProvinceId(int? provinceId)
        {
            var result = await _dataService.GetDistrictByProvinceIdAsync(provinceId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetMunicipalityType()
        {
            var result = await _dataService.GetActiveMunicipalityTypeAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetGender()
        {
            var result = await _dataService.GetActiveGenderAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetMemberField()
        {
            var result = await _dataService.GetActiveMemberFieldAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetOccupation()
        {
            var result = await _dataService.GetActiveOccupationAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}