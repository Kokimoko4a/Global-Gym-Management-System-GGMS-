using GGMS.Web.Infrastructure.Extensions;
using GGMS.Web.ViewModels.GymOwner;
using GGMSServices.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.UniversalAccessibility;
using System.Drawing.Imaging;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Security.Claims;
using static GGMS.Common.NotificationMessagesConstants;
//using htmlToImageAndPDFConvertier.Models;
/*using iText.Barcodes;
using iText.Barcodes.Qrcode;
using iText.Commons.Datastructures;*/
using PdfSharp.Drawing;
using SelectPdf;
//using SkiaSharp;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml.Linq;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace GGMS.Web.Controllers
{
    public class GymOwnerController : Controller
    {
        private readonly IGymOwnerService gymOwnerService;

        public GymOwnerController(IGymOwnerService gymOwnerService)
        {
            this.gymOwnerService = gymOwnerService;
        }

        [HttpGet]
        public async Task<IActionResult> BecomeGymOwner()
        {
            Guid userId = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));

            if (User.Identity.IsAuthenticated)
            {
                if (!await gymOwnerService.IsGymOwner(userId))
                {
                    await gymOwnerService.BecomeGymOwner(userId);
                    return RedirectToAction("Index", "Home");
                }

                TempData[WarningMessage] = "You are gym owner!";
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]

        public async Task<IActionResult> CreateGym()
        {
            if (User.Identity.IsAuthenticated)
            {
                Guid userId = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));

                if (await gymOwnerService.IsGymOwner(userId))
                {
                    return View();
                }

                TempData[WarningMessage] = "You are not gym owner! You canonot create gym/s!";
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");

        }


        [HttpPost]
        public async Task<IActionResult> CreateGym(GymFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "Error when creating!";
                return View(formModel);
            }

            if (User.Identity.IsAuthenticated)
            {
                Guid userId = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));

                await gymOwnerService.CreateGym(formModel,userId);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> GetAllGyms([FromQuery] GymQueryModel gymQueryModel) 
        {
           

                return View( await gymOwnerService.GetGymsWithQueryModel(gymQueryModel));
            

         
        }

        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        {
            return View(gymOwnerService.GetGymAsBigViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> BuyCard(Guid id)
        {
            string[] data = new string[5];

            var userId = User.GetClaimValue(ClaimTypes.NameIdentifier);

            data = await gymOwnerService.CreateGymCard(Guid.Parse(userId), id);

            string cardHolderFirstName = data[0];
            string cardHolderLastName = data[1];
            string gymName = data[2];
            string issueDate = data[3];
            string expirationDate = data[4];

            /*
        output[0] = applicationUser!.FirstName;
        output[1] = applicationUser.LastName;
        output[2] = gym.Name;
        output[3] = fitnessCard.IssueDate.ToString();
        output[4] = fitnessCard.ExpiringDate.ToString();*/


            string htmlContent = @"@""
<html lang=""""en"""">
<head>
    <meta charset=""""UTF-8"""">
    <meta name=""""viewport"""" content=""""width=device-width, initial-scale=1.0"""">
    <title>Fitness Card</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f0f0;
            margin: 0;
            padding: 0;
        }

        .card {
            width: 350px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            margin: 50px auto;
        }

        .card-header {
            background-color: #2ecc71;
            color: #fff;
            text-align: center;
            padding: 20px;
            border-top-left-radius: 10px;
            border-top-right-radius: 10px;
        }

        .card-body {
            padding: 20px;
        }

        h2 {
            margin: 0;
        }

        .info-section {
            margin-bottom: 20px;
        }

        .info-section h3 {
            margin: 0;
            font-size: 16px;
            color: #444;
        }

        .info-section p {
            margin: 5px 0 0;
            color: #666;
            font-size: 14px;
        }

        .qr-code {
            text-align: center;
            margin-top: 20px;
        }

        .qr-code img {
            width: 100px;
            height: 100px;
        }
    </style>
</head>
<body>
    <div class=""""card"""">
        <div class=""""card-header"""">
            <h2>Fitness Card</h2>
        </div>
        <div class=""""card-body"""">
            <div class=""""info-section"""">
                <h3>Card Holder:</h3>
                <p>First Name:* Last Name:#</p>
            </div>
            <div class=""""info-section"""">
                <h3>Fitness Center:</h3>
                <p>!</p>
            </div>
            <div class=""""info-section"""">
                <h3>Issue Date:</h3>
                <p>&</p>
            </div>
            <div class=""""info-section"""">
                <h3>Expiration Date:</h3>
                <p>^</p>
            </div>
            <div class=""""qr-code"""">
                <img src=""""data:image/png;base64,@ViewBag.QrCodeBase64"""" alt=""""QR Code"""">
            </div>
        </div>
    </div>
</body>
</html>
"";
";
            htmlContent = htmlContent.Replace("^", expirationDate);
            htmlContent = htmlContent.Replace("&", issueDate);
            htmlContent = htmlContent.Replace("!", gymName);
            htmlContent = htmlContent.Replace("*", cardHolderFirstName);
            htmlContent = htmlContent.Replace("#", cardHolderLastName);


         
      

         

            // Convert HTML to PDF as before...

            HtmlToPdf htmlToPdf = new HtmlToPdf();

            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(htmlContent);



            byte[] bytes = pdfDocument.Save();

            return File(bytes, "application/pdf", "Your_Fitness_Card.pdf");



        }


       


        public IActionResult GetPhoto(string[] data)
        {
            //tesing 

            string cardHolderFirstName = data[0];
            string cardHolderLastName = data[1];
            string gymName = data[2];
            string issueDate = data[3];
            string expirationDate = data[4];

                /*
            output[0] = applicationUser!.FirstName;
            output[1] = applicationUser.LastName;
            output[2] = gym.Name;
            output[3] = fitnessCard.IssueDate.ToString();
            output[4] = fitnessCard.ExpiringDate.ToString();*/


            string htmlContent = @"@""
<html lang=""""en"""">
<head>
    <meta charset=""""UTF-8"""">
    <meta name=""""viewport"""" content=""""width=device-width, initial-scale=1.0"""">
    <title>Fitness Card</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f0f0;
            margin: 0;
            padding: 0;
        }

        .card {
            width: 350px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            margin: 50px auto;
        }

        .card-header {
            background-color: #2ecc71;
            color: #fff;
            text-align: center;
            padding: 20px;
            border-top-left-radius: 10px;
            border-top-right-radius: 10px;
        }

        .card-body {
            padding: 20px;
        }

        h2 {
            margin: 0;
        }

        .info-section {
            margin-bottom: 20px;
        }

        .info-section h3 {
            margin: 0;
            font-size: 16px;
            color: #444;
        }

        .info-section p {
            margin: 5px 0 0;
            color: #666;
            font-size: 14px;
        }

        .qr-code {
            text-align: center;
            margin-top: 20px;
        }

        .qr-code img {
            width: 100px;
            height: 100px;
        }
    </style>
</head>
<body>
    <div class=""""card"""">
        <div class=""""card-header"""">
            <h2>Fitness Card</h2>
        </div>
        <div class=""""card-body"""">
            <div class=""""info-section"""">
                <h3>Card Holder:</h3>
                <p>First Name:* Last Name:#</p>
            </div>
            <div class=""""info-section"""">
                <h3>Fitness Center:</h3>
                <p>!</p>
            </div>
            <div class=""""info-section"""">
                <h3>Issue Date:</h3>
                <p>&</p>
            </div>
            <div class=""""info-section"""">
                <h3>Expiration Date:</h3>
                <p>^</p>
            </div>
            <div class=""""qr-code"""">
                <img src=""""data:image/png;base64,@ViewBag.QrCodeBase64"""" alt=""""QR Code"""">
            </div>
        </div>
    </div>
</body>
</html>
"";
";
            htmlContent = htmlContent.Replace("^",expirationDate);
            htmlContent = htmlContent.Replace("&",issueDate);
            htmlContent = htmlContent.Replace("!",gymName);
            htmlContent = htmlContent.Replace("*",cardHolderFirstName);
            htmlContent = htmlContent.Replace("#",cardHolderLastName);


            string qrCodeData = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTXDzCuZGQTUvOp2Sj7o4cHjvL-U-Zl062lfIY9DFUDK7yRLfir"; // Replace with your data
          

            // Convert QR Code PNG byte array to Base64
        

            // Insert QR Code Base64 into HTML content
     

            // Convert HTML to PDF as before...

            HtmlToPdf htmlToPdf = new HtmlToPdf();

            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(htmlContent);



            byte[] bytes = pdfDocument.Save();

            return File(bytes, "application/pdf", "Your_Fitness_Card.pdf");



            /* string htmlContent = "<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Fitness Card</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            background-color: #f0f0f0;\r\n            margin: 0;\r\n            padding: 0;\r\n        }\r\n\r\n        .card {\r\n            width: 300px;\r\n            background-color: #fff;\r\n            border-radius: 10px;\r\n            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);\r\n            margin: 50px auto;\r\n        }\r\n\r\n        .card-header {\r\n            background-color: #3498db;\r\n            color: #fff;\r\n            text-align: center;\r\n            padding: 20px;\r\n            border-top-left-radius: 10px;\r\n            border-top-right-radius: 10px;\r\n        }\r\n\r\n        .card-body {\r\n            padding: 20px;\r\n        }\r\n\r\n        h2 {\r\n            margin: 0;\r\n        }\r\n\r\n        h3 {\r\n            margin: 0;\r\n        }\r\n\r\n        .exercise, .duration, .calories, .validity {\r\n            margin-bottom: 20px;\r\n        }\r\n\r\n        .exercise p, .duration p, .calories p, .validity p {\r\n            margin: 0;\r\n            color: #666;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"card\">\r\n        <div class=\"card-header\">\r\n            <h2>Fitness Card</h2>\r\n        </div>\r\n        <div class=\"card-body\">\r\n            <div class=\"exercise\">\r\n                <h3>Exercise:</h3>\r\n                <p>Push-ups</p>\r\n            </div>\r\n            <div class=\"duration\">\r\n                <h3>Duration:</h3>\r\n                <p>30 minutes</p>\r\n            </div>\r\n            <div class=\"calories\">\r\n                <h3>Calories Burned:</h3>\r\n                <p>250 kcal</p>\r\n            </div>\r\n            <div class=\"validity\">\r\n                <h3>Validity:</h3>\r\n                <p>From 2024-05-01 to 2024-06-01</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n <h1>{username}</h1></html>";


             htmlContent = htmlContent.Replace("{username}", "Kaloyan");

             HtmlToPdf htmlToPdf = new HtmlToPdf();

             PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(htmlContent);



             byte[] bytes = pdfDocument.Save();





             pdfDocument.Close();

             return File(bytes, "application/pdf",
                     "Your_Fitness_Card.pdf");*/ //this works okay !!!

        }

    }

}

