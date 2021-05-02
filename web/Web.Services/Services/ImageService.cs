﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web.Services.Services
{
    public interface IImageService
    {
        byte[] ConvertToByte(HttpPostedFileBase file);
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
    }
}
