using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Services.Services;

namespace web.Controllers
{
    public class AjaxController : Controller
    {
        private readonly IImageService _imageService;
        public AjaxController(IImageService imageService)
        {
            _imageService = imageService;
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
    }
}