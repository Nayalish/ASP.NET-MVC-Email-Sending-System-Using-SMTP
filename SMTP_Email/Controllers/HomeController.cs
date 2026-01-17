using Microsoft.AspNetCore.Mvc;
using SMTP_Email.Models;
using System.Diagnostics;
using System.Net.Mail;

namespace SMTP_Email.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        //Global Declarations
        string mailbody = @"
    <html>
    <head>
        <title>Your Email Title</title>
    </head>
    <body>
        <h1>Hello!</h1>
        <p>This is an example email content in HTML format.</p>
        <p>You can customize this with your own message, formatting, and styles.</p>
        <p>Feel free to add images, links, and more HTML elements as needed.</p>
    </body>
    </html>
";
        string FromEmail = "enter your email address here";
        string mailtitle = "Attachment Demo";
        string mailsubject = "Email with Attachment";
        string mailpassword = "enter your 2 step App password here";       //app password(click account managage account then security search app password)

        public IActionResult Email()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Email(string ToEmail, IFormFile fileToAttach)
        {
            //Mail Message
            MailMessage message = new MailMessage(new MailAddress(FromEmail, mailtitle), new MailAddress(ToEmail));
            //Mail Content
            message.Subject = mailsubject;
            message.Body = mailbody;
            message.IsBodyHtml = true;
            //Attachment
            message.Attachments.Add(new Attachment(fileToAttach.OpenReadStream(), fileToAttach.FileName));
            //Server Details
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //Credentials
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
            credential.UserName = FromEmail;
            credential.Password = mailpassword;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credential;
            //SendEmail
            smtp.Send(message);
            ViewBag.emailsentmessage = "Email Sent Successfully";
            return View();
        }
    

    public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
