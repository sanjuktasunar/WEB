using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web.Services.Services
{
    public interface IImageService
    {
        byte[] ConvertToByte(HttpPostedFileBase file);
        string ConvertToString(HttpPostedFileBase file);
        byte[] ConvertToByteFromBaseString(string base64String);
    }
    public class ImageService:IImageService
    {
        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageData = null;
            if (file != null && file.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    imageData = binaryReader.ReadBytes(file.ContentLength);
                }
            }
            return imageData;
        }

        public string ConvertToString(HttpPostedFileBase file)
        {
            return Convert.ToBase64String(ConvertToByte(file));
        }

        public byte[] ConvertToByteFromBaseString(string base64String)
        {
            string imageString = base64String.Replace("data:image;base64,", " ").Trim();
            return System.Convert.FromBase64String(imageString);
        }
    }
}
