using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        public Image ConvertToImage(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);

                System.Drawing.Image sourceimage =
                    System.Drawing.Image.FromStream(file.InputStream);
                return sourceimage;
            }
            return null;
        }

        public static byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public string ConvertToString(HttpPostedFileBase file)
        {
            var image = ConvertToImage(file);
            if (image != null)
            {
                var resizeImage=ResizeImage(image, 200, 200, true);
                //var Imgstring = Convert.ToBase64String(ConvertToByte(file));
                var Imgstring = Convert.ToBase64String(imageToByteArray(resizeImage));
                return Imgstring;
            }
            return null;
        }

        public byte[] ConvertToByteFromBaseString(string base64String)
        {
            string imageString = base64String.Replace("data:image;base64,", " ").Trim();
            return System.Convert.FromBase64String(imageString);
        }

        public Image ResizeImage(Image image, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height * newWidth / image.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead  
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var res = new Bitmap(newWidth, newHeight);

            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return res;
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }
}
