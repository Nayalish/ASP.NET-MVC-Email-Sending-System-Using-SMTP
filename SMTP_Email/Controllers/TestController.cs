using Microsoft.AspNetCore.Mvc;

namespace SMTP_Email.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
