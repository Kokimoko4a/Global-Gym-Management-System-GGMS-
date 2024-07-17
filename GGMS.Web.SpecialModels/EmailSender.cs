namespace GGMS.Web.SpecialModels
{
    using GGMS.Data.Models;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using SelectPdf;

    public class EmailSender
    {
        public async Task Execute(ApplicationUser trainer, ApplicationUser user, List<FitnessProgram> fitnessPrograms) //if you have this to work get new api key!!!!
        {
            

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("kaloyan.rusev.2007@abv.bg", trainer.FirstName + " " + trainer.LastName);
            var subject = $"New program from {trainer.UserName}";
            var to = new EmailAddress(user.Email, user.FirstName + " " + user.LastName);
            var plainTextContent = "Научих се как да пращам имейлите. От Баба Меца(Калоян)";
            var htmlContent = "<p>New program/s for you!</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            string htmlFormatForFitnessProgram = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Fitness Program</title>\r\n    <style>\r\n        body {\r\n            font-family: 'Arial', sans-serif;\r\n            line-height: 1.6;\r\n            margin: 0;\r\n            padding: 20px;\r\n            background-color: #f4f4f4;\r\n        }\r\n        .container {\r\n            max-width: 800px;\r\n            margin: 20px auto;\r\n            background: #fff;\r\n            padding: 30px;\r\n            border-radius: 10px;\r\n            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);\r\n        }\r\n        h1 {\r\n            color: #2c3e50;\r\n            text-align: center;\r\n            margin-bottom: 20px;\r\n        }\r\n        .description {\r\n            color: #34495e;\r\n        }\r\n        h2 {\r\n            color: #16a085;\r\n            margin-top: 20px;\r\n        }\r\n        p {\r\n            margin: 10px 0;\r\n            text-align: justify;\r\n        }\r\n        .highlight {\r\n            color: #e74c3c;\r\n            font-weight: bold;\r\n        }\r\n        ul {\r\n            list-style-type: disc;\r\n            padding-left: 20px;\r\n            margin: 10px 0;\r\n        }\r\n        li {\r\n            margin-bottom: 10px;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h1>Fitness Program</h1>\r\n        <div class=\"description\">\r\n           {{PROGRAM_DESCRIPTION}}\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>\r\n";


            foreach (var item in fitnessPrograms)
            {
                HtmlToPdf htmlToPdf = new HtmlToPdf();

                htmlContent = htmlContent.Replace("description",item.Description);

                PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(htmlContent);



                byte[] bytes = pdfDocument.Save();

                msg.AddAttachment($"{item.Title}.pdf", Convert.ToBase64String(bytes));
            }


            // Attach a file
          /* var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "Test.txt");
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileBase64 = Convert.ToBase64String(fileBytes);*/  

            var response =  await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
        }
    }
}
