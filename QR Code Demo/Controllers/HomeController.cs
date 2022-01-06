using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QR_Code_Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult getQR(string username, string password)
        {
            //match data with sql table 
            if(username == "admin" && password == "admin")
            {

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                int id = 1245; //get id from db
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(id.ToString(), QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                return File(BitmapToBytes(qrCodeImage), "image/jpeg");
            }
            //else show error on index page
            return RedirectToAction("Index");
        }
        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}